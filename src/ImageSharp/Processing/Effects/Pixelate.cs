// <copyright file="Pixelate.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp
{
    using System;
    using Processing.Processors;
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.Primitives;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Pixelates an image with the given pixel size.
        /// </summary>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        /// <param name="source">The image this method extends.</param>
        /// <param name="size">The size of the pixels.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static Image<TPixel> Pixelate<TPixel>(this Image<TPixel> source, int size = 4)
            where TPixel : struct, IPixel<TPixel>
        {
            return Pixelate(source, size, source.Bounds);
        }

        /// <summary>
        /// Pixelates an image with the given pixel size.
        /// </summary>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        /// <param name="source">The image this method extends.</param>
        /// <param name="size">The size of the pixels.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static Image<TPixel> Pixelate<TPixel>(this Image<TPixel> source, int size, Rectangle rectangle)
            where TPixel : struct, IPixel<TPixel>
        {
            if (size <= 0 || size > source.Height || size > source.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            source.ApplyProcessor(new PixelateProcessor<TPixel>(size), rectangle);
            return source;
        }
    }
}