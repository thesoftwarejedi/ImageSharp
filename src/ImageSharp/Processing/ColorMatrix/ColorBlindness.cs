// <copyright file="ColorBlindness.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;

    using ImageSharp.PixelFormats;

    using ImageSharp.Processing;
    using Processing.Processors;
    using SixLabors.Primitives;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Applies the given colorblindness simulator to the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="colorBlindness">The type of color blindness simulator to apply.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations ColorBlindness(this IImageOperations source, ColorBlindness colorBlindness)
        {
            source.ApplyProcessor(GetProcessor(colorBlindness));
            return source;
        }

        /// <summary>
        /// Applies the given colorblindness simulator to the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="colorBlindness">The type of color blindness simulator to apply.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations ColorBlindness(this IImageOperations source, ColorBlindness colorBlindness, Rectangle rectangle)
        {
            source.ApplyProcessor(GetProcessor(colorBlindness), rectangle);
            return source;
        }

        private static IImageProcessor GetProcessor(ColorBlindness colorBlindness)
        {
            switch (colorBlindness)
            {
                case ImageSharp.Processing.ColorBlindness.Achromatomaly:
                    return new AchromatomalyProcessor();
                case ImageSharp.Processing.ColorBlindness.Achromatopsia:
                    return new AchromatopsiaProcessor();
                case ImageSharp.Processing.ColorBlindness.Deuteranomaly:
                    return new DeuteranomalyProcessor();
                case ImageSharp.Processing.ColorBlindness.Deuteranopia:
                    return new DeuteranopiaProcessor();
                case ImageSharp.Processing.ColorBlindness.Protanomaly:
                    return new ProtanomalyProcessor();
                case ImageSharp.Processing.ColorBlindness.Protanopia:
                    return new ProtanopiaProcessor();
                case ImageSharp.Processing.ColorBlindness.Tritanomaly:
                    return new TritanomalyProcessor();
                default:
                    return new TritanopiaProcessor();
            }
        }
    }
}
