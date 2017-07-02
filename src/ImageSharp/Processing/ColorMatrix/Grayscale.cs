// <copyright file="Grayscale.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
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
        /// Applies <see cref="GrayscaleMode.Bt709"/> Grayscale toning to the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Grayscale(this IImageOperations source)
        {
            return Grayscale(source, GrayscaleMode.Bt709);
        }

        /// <summary>
        /// Applies Grayscale toning to the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="mode">The formula to apply to perform the operation.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Grayscale(this IImageOperations source, GrayscaleMode mode)
        {
            IImageProcessor processor = mode == GrayscaleMode.Bt709
               ? (IImageProcessor)new GrayscaleBt709Processor()
               : new GrayscaleBt601Processor();

            source.ApplyProcessor(processor);
            return source;
        }

        /// <summary>
        /// Applies <see cref="GrayscaleMode.Bt709"/> Grayscale toning to the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Grayscale(this IImageOperations source, Rectangle rectangle)
        {
            return Grayscale(source, GrayscaleMode.Bt709, rectangle);
        }

        /// <summary>
        /// Applies Grayscale toning to the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="mode">The formula to apply to perform the operation.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Grayscale(this IImageOperations source, GrayscaleMode mode, Rectangle rectangle)
        {
            IImageProcessor processor = mode == GrayscaleMode.Bt709
                ? (IImageProcessor)new GrayscaleBt709Processor()
                : new GrayscaleBt601Processor();

            source.ApplyProcessor(processor, rectangle);
            return source;
        }
    }
}
