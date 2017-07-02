// <copyright file="Rgba32.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Globalization;
    using System.Numerics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    using ImageSharp.PixelFormats;

    /// <summary>
    /// Packed pixel type containing four 8-bit unsigned normalized values ranging from 0 to 255.
    /// The color components are stored in red, green, blue, and alpha order.
    /// <para>
    /// Ranges from &lt;0, 0, 0, 0&gt; to &lt;1, 1, 1, 1&gt; in vector form.
    /// </para>
    /// </summary>
    /// <remarks>
    /// This struct is fully mutable. This is done (against the guidelines) for the sake of performance,
    /// as it avoids the need to create new values for modification operations.
    /// </remarks>
    public partial struct Color
    {
        private Rgba32 backing;

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color(byte r, byte g, byte b)
            : this(r, g, b, 255)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> struct.
        /// </summary>
        /// <param name="r">The red component.</param>
        /// <param name="g">The green component.</param>
        /// <param name="b">The blue component.</param>
        /// <param name="a">The alpha component.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color(byte r, byte g, byte b, byte a)
        {
            this.backing = new Rgba32(r, g, b, a);
        }

        /// <summary>
        /// Gets or sets the red component.
        /// </summary>
        public byte R { get => this.backing.R; set => this.backing.R = value; }

        /// <summary>
        /// Gets or sets the green component.
        /// </summary>
        public byte G { get => this.backing.G; set => this.backing.G = value; }

        /// <summary>
        /// Gets or sets the blue component.
        /// </summary>
        public byte B { get => this.backing.B; set => this.backing.B = value; }

        /// <summary>
        /// Gets or sets the alpha component.
        /// </summary>
        public byte A { get => this.backing.A; set => this.backing.A = value; }

        /// <summary>
        /// Compares two <see cref="Rgba32"/> objects for equality.
        /// </summary>
        /// <param name="left">
        /// The <see cref="Rgba32"/> on the left side of the operand.
        /// </param>
        /// <param name="right">
        /// The <see cref="Rgba32"/> on the right side of the operand.
        /// </param>
        /// <returns>
        /// True if the <paramref name="left"/> parameter is equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Color left, Color right)
        {
            return left.backing == right.backing;
        }

        /// <summary>
        /// Compares two <see cref="Rgba32"/> objects for equality.
        /// </summary>
        /// <param name="left">The <see cref="Rgba32"/> on the left side of the operand.</param>
        /// <param name="right">The <see cref="Rgba32"/> on the right side of the operand.</param>
        /// <returns>
        /// True if the <paramref name="left"/> parameter is not equal to the <paramref name="right"/> parameter; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Color left, Color right)
        {
            return left.backing != right.backing;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Rgba32"/> struct.
        /// </summary>
        /// <param name="hex">
        /// The hexadecimal representation of the combined color components arranged
        /// in rgb, rgba, rrggbb, or rrggbbaa format to match web syntax.
        /// </param>
        /// <returns>
        /// The <see cref="Rgba32"/>.
        /// </returns>
        public static Color FromHex(string hex)
        {
            Guard.NotNullOrEmpty(hex, nameof(hex));

            hex = ToRgbaHex(hex);
            uint packedValue;
            if (hex == null || !uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out packedValue))
            {
                throw new ArgumentException("Hexadecimal string is not in the correct format.", nameof(hex));
            }

            return new Color(
                (byte)(packedValue >> 24),
                (byte)(packedValue >> 16),
                (byte)(packedValue >> 8),
                (byte)(packedValue >> 0));
        }

        /// <summary>
        /// Generates a new Color with the opactiy alterd
        /// </summary>
        /// <param name="opacity">The value of the alpha channel to set on the new color</param>
        /// <returns>A new color with the opacity set.</returns>
        public Color WithOpacity(byte opacity)
        {
            return new Color(this.backing.R, this.backing.G, this.backing.B, opacity);
        }

        /// <summary>
        /// Converts the value of this instance to a hexadecimal string.
        /// </summary>
        /// <returns>A hexadecimal string representation of the value.</returns>
        public string ToHex()
        {
            return this.backing.ToHex();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return (obj is Color) && this.Equals((Color)obj);
        }

        /// <summary>
        /// Determins of the to colors are identical.
        /// </summary>
        /// <param name="other">The other color</param>
        /// <returns>true if the 2 colors represent the same shade</returns>
        public bool Equals(Color other)
        {
            return this.backing == other.backing;
        }

        /// <summary>
        /// Gets a string representation of the packed vector.
        /// </summary>
        /// <returns>A string representation of the packed vector.</returns>
        public override string ToString()
        {
            return this.ToHex();
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.backing.GetHashCode();
        }

        /// <summary>
        /// Returns this color struct as a valuing <typeparamref name="TPixel"/> format
        /// </summary>
        /// <typeparam name="TPixel">The format to convert this color to</typeparam>
        /// <returns>The TPixel </returns>
        public TPixel As<TPixel>()
            where TPixel : struct, IPixel<TPixel>
        {
            var pixel = default(TPixel);
            pixel.PackFromRgba32(this.backing);
            return pixel;
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