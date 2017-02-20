// <copyright file="ColorConverter{TColor}.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Buffers;
    using System.Globalization;

    /// <summary>
    /// A set of named colors mapped to the provided Color space.
    /// </summary>
    /// <typeparam name="TColor">The type of the color.</typeparam>
    public static class ColorConverter<TColor>
        where TColor : struct, IPackedPixel, IEquatable<TColor>
    {
        /// <summary>
        /// returns the hexidecimal version of this color in format that can be read by <see cref="FromHex(string)"/>.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The hex representation of this color.</returns>
        public static string ToHex(TColor color)
        {
            byte[] buffer = ArrayPool<byte>.Shared.Rent(4);
            try
            {
                color.ToXyzwBytes(buffer, 0);
                return ToHexFromXyzw(buffer);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// Creates a new <typeparamref name="TColor"/> representation from the string representing a color.
        /// </summary>
        /// <param name="hex">
        /// The hexadecimal representation of the combined color components arranged
        /// in rgb, rgba, rrggbb, or rrggbbaa format to match web syntax.
        /// </param>
        /// <returns>Returns a <typeparamref name="TColor"/> that represents the color defined by the provided RGBA heax string.</returns>
        public static TColor FromHex(string hex)
        {
            Guard.NotNullOrEmpty(hex, nameof(hex));

            hex = ToRgbaHex(hex);
            uint packedValue;
            if (hex == null || !uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out packedValue))
            {
                throw new ArgumentException("Hexadecimal string is not in the correct format.", nameof(hex));
            }

            TColor result = default(TColor);

            result.PackFromBytes(
                (byte)(packedValue >> 24),
                (byte)(packedValue >> 16),
                (byte)(packedValue >> 8),
                (byte)(packedValue >> 0));
            return result;
        }

        /// <summary>
        /// returns the string reperesentation of this color as usable with web application.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        /// The web representation of this color.
        /// </returns>
        public static string ToWebExact(TColor color, WebStringFormat format)
        {
            if (format == WebStringFormat.Name)
            {
                // do name seperately as it doesn't require extracting the bytes
                return NamedColors<TColor>.FindWebSafeName(color);
            }

            byte[] buffer = ArrayPool<byte>.Shared.Rent(4);
            try
            {
                color.ToXyzwBytes(buffer, 0);
                return ToWebExactFromXyzw(buffer, format);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// returns the string reperesentation of this color as usable with we application.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The web representation of this color.</returns>
        public static string ToWeb(TColor color)
        {
            string webColor = NamedColors<TColor>.FindWebSafeName(color);

            if (webColor != null)
            {
                return webColor;
            }

            byte[] buffer = ArrayPool<byte>.Shared.Rent(4);
            try
            {
                color.ToXyzwBytes(buffer, 0);

                var webString = ToWebExactFromXyzw(buffer, WebStringFormat.Hex); // try as a hash second
                webString = webString ?? ToWebExactFromXyzw(buffer, WebStringFormat.RgbaFunctionBytes); // else fallback to rgba

                return webString;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        /// <summary>
        /// Creates a new <typeparamref name="TColor"/> representation from the string representing a color.
        /// </summary>
        /// <param name="webColor">
        /// The representation of the color, eather as Wellknown name, #RRGGBB color hash, or rgba(rrr,ggg,bbb,a) string.
        /// </param>
        /// <returns>Returns a <typeparamref name="TColor"/> that represents the color defined by the provided RGBA heax string.</returns>
        public static TColor FromWeb(string webColor)
        {
            Guard.NotNullOrEmpty(webColor, nameof(FromWeb));

            if (webColor[0] == '#')
            {
                // treat it as a hext string
                return FromHex(webColor);
            }
            else if ((webColor.StartsWith("rgba(", StringComparison.OrdinalIgnoreCase) || webColor.StartsWith("rgb(", StringComparison.OrdinalIgnoreCase)) && webColor.EndsWith(")"))
            {
                // {rgb,rgba}(x,x,x,x) function
                int argCount = 3;

                int idx = webColor.IndexOf('(');
                if (webColor.Substring(0, idx).Equals("rgba", StringComparison.OrdinalIgnoreCase))
                {
                    argCount = 4;
                }

                string args = webColor.Substring(idx + 1, webColor.Length - idx - 2);
                string[] argsArray = args.Split(',');
                if (argsArray.Length != argCount)
                {
                    throw new FormatException($"{(argCount == 3 ? "rgb" : "rgba")} must have exactly {argCount} paramaters but only found {argsArray.Length}");
                }

                byte[] buffer = new byte[4];
                bool isPercentage = false;
                for (var i = 0; i < 3; i++)
                {
                    string trimmed = argsArray[i].Trim();
                    if (i == 0)
                    {
                        // we get the format now
                        if (trimmed[trimmed.Length - 1] == '%')
                        {
                            isPercentage = true;
                        }
                    }
                    else
                    {
                        // we validate the format
                        if (trimmed[trimmed.Length - 1] == '%')
                        {
                            if (!isPercentage)
                            {
                                throw new FormatException("can't mix percentage and byte color components, all components must be formatted corrected");
                            }
                        }
                        else
                        {
                            if (isPercentage)
                            {
                                throw new FormatException("missing '%', all components must be formatted corrected");
                            }
                        }
                    }

                    if (isPercentage)
                    {
                        trimmed = trimmed.TrimEnd('%');
                        float percentage = float.Parse(trimmed).Clamp(0, 100);
                        buffer[i] = (byte)((percentage / 100f) * 255f);
                    }
                    else
                    {
                        buffer[i] = (byte)int.Parse(trimmed).Clamp(0, 255);
                    }
                }

                if (argCount == 4)
                {
                    buffer[3] = (byte)(float.Parse(argsArray[3]).Clamp(0, 1) * 255f);
                }
                else
                {
                    buffer[3] = 255;
                }

                return FromRGBA(buffer[0], buffer[1], buffer[2], buffer[3]);
            }

            TColor result;
            if (NamedColors<TColor>.TryFindColor(webColor, out result))
            {
                return result;
            }

            return default(TColor);
        }

        /// <summary>
        /// Creates a new <typeparamref name="TColor"/> representation from standard RGB bytes with 100% opacity.
        /// </summary>
        /// <param name="red">The red intensity.</param>
        /// <param name="green">The green intensity.</param>
        /// <param name="blue">The blue intensity.</param>
        /// <returns>Returns a <typeparamref name="TColor"/> that represents the color defined by the provided RGB values with 100% opacity.</returns>
        public static TColor FromRGB(byte red, byte green, byte blue)
        {
            TColor color = default(TColor);
            color.PackFromBytes(red, green, blue, 255);
            return color;
        }

        /// <summary>
        /// Creates a new <typeparamref name="TColor"/> representation from standard RGBA bytes.
        /// </summary>
        /// <param name="red">The red intensity.</param>
        /// <param name="green">The green intensity.</param>
        /// <param name="blue">The blue intensity.</param>
        /// <param name="alpha">The alpha intensity.</param>
        /// <returns>Returns a <typeparamref name="TColor"/> that represents the color defined by the provided RGBA values.</returns>
        public static TColor FromRGBA(byte red, byte green, byte blue, byte alpha)
        {
            TColor color = default(TColor);
            color.PackFromBytes(red, green, blue, alpha);
            return color;
        }

        private static string ToHexFromXyzw(byte[] buffer)
        {
            uint hexOrder = (uint)(buffer[0] << 24 |
                buffer[1] << 16 |
                buffer[2] << 8 |
                buffer[3] << 0);
            return hexOrder.ToString("X8");
        }

        /// <summary>
        /// Converts the specified hex value to an rrggbbaa hex value.
        /// </summary>
        /// <param name="hex">The hex value to convert.</param>
        /// <returns>
        /// A rrggbbaa hex value.
        /// </returns>
        private static string ToRgbaHex(string hex)
        {
            hex = hex.StartsWith("#") ? hex.Substring(1) : hex;

            if (hex.Length == 8)
            {
                return hex;
            }

            if (hex.Length == 6)
            {
                return hex + "FF";
            }

            if (hex.Length < 3 || hex.Length > 4)
            {
                return null;
            }

            string red = char.ToString(hex[0]);
            string green = char.ToString(hex[1]);
            string blue = char.ToString(hex[2]);
            string alpha = hex.Length == 3 ? "F" : char.ToString(hex[3]);

            return string.Concat(red, red, green, green, blue, blue, alpha, alpha);
        }

        private static string ToWebExactFromXyzw(byte[] buffer, WebStringFormat format)
        {
            if (buffer[3] == 255)
            {
                // ones that do not support an alpha channel
                switch (format)
                {
                    case WebStringFormat.HexFull:
                        return "#" + ToHexFromXyzw(buffer).Substring(0, 6);
                    case WebStringFormat.RgbFunctionBytes:
                        return $"rgb({buffer[0]}, {buffer[1]}, {buffer[2]})";
                    case WebStringFormat.Hex:
                        var hex = ToHexFromXyzw(buffer);
                        if (hex[0] == hex[1] && hex[2] == hex[3] && hex[4] == hex[5])
                        {
                            return string.Concat('#', hex[0], hex[2], hex[4]);
                        }

                        return "#" + hex.Substring(0, 6);
                    case WebStringFormat.RgbFunctionPercentages:
                        float r = buffer[0] * 100f / 255f;
                        float g = buffer[1] * 100f / 255f;
                        float b = buffer[2] * 100f / 255f;
                        return $"rgb({r:0.#}%, {g:0.#}%, {b:0.#}%)";
                }
            }

            // ones that DO support an alpha channel
            switch (format)
            {
                case WebStringFormat.RgbaFunctionBytes:
                    float a1 = buffer[3] / 255f;
                    return $"rgba({buffer[0]}, {buffer[1]}, {buffer[2]}, {a1:0.##})";
                case WebStringFormat.RgbaFunctionPercentages:
                    float r = buffer[0] * 100f / 255f;
                    float g = buffer[1] * 100f / 255f;
                    float b = buffer[2] * 100f / 255f;
                    float a = buffer[3] / 255f;
                    return $"rgba({r:0.#}%, {g:0.#}%, {b:0.#}%, {a:0.##})";
            }

            return null;
        }
    }
}