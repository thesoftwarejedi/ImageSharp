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
        /// returns the string reperesentation of this color as usable with we application.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The web representation of this color.</returns>
        public static string ToWeb(TColor color)
        {
            string webColor = NamedColors<TColor>.FindName(color);

            if (webColor != null)
            {
                return webColor;
            }

            byte[] buffer = ArrayPool<byte>.Shared.Rent(4);
            try
            {
                color.ToXyzwBytes(buffer, 0);

                if (buffer[3] == 255)
                {
                    return "#" + ToHexFromXyzw(buffer).Substring(0, 6);
                }
                else
                {
                    // has an alpha channel use rgba
                    return $"rgba({buffer[0]}, {buffer[1]}, {buffer[2]}, {buffer[3] / 255f})";
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
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
    }
}