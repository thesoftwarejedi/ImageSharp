// <copyright file="Vignette.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using ImageSharp.PixelFormats;

    using Processing.Processors;
    using SixLabors.Primitives;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Vignette(this IImageOperations source)
        {
            return Vignette(source, GraphicsOptions.Default);
        }

        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the vignette.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Vignette(this IImageOperations source, Color color)
        {
            return Vignette(source, color, GraphicsOptions.Default);
        }

        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="radiusX">The the x-radius.</param>
        /// <param name="radiusY">The the y-radius.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Vignette(this IImageOperations source, float radiusX, float radiusY)
        {
            return Vignette(source, radiusX, radiusY, GraphicsOptions.Default);
        }

        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Vignette(this IImageOperations source, Rectangle rectangle)
        {
            return Vignette(source, rectangle, GraphicsOptions.Default);
        }

        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the vignette.</param>
        /// <param name="radiusX">The the x-radius.</param>
        /// <param name="radiusY">The the y-radius.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Vignette(this IImageOperations source, Color color, float radiusX, float radiusY, Rectangle rectangle)
         => source.Vignette(color, radiusX, radiusY, rectangle, GraphicsOptions.Default);

        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="options">The options effecting pixel blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Vignette(this IImageOperations source, GraphicsOptions options)
         => source.Vignette(Color.Black, ValueSize.PercentageOfWidth(.5f), ValueSize.PercentageOfHeight(.5f), options);

        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the vignette.</param>
        /// <param name="options">The options effecting pixel blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Vignette(this IImageOperations source, Color color, GraphicsOptions options)
         => source.Vignette(color, 0, 0, options);

        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="radiusX">The the x-radius.</param>
        /// <param name="radiusY">The the y-radius.</param>
        /// <param name="options">The options effecting pixel blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Vignette(this IImageOperations source, float radiusX, float radiusY, GraphicsOptions options)
         => source.Vignette(Color.Black, radiusX, radiusY, options);

        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <param name="options">The options effecting pixel blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Vignette(this IImageOperations source, Rectangle rectangle, GraphicsOptions options)
         => source.Vignette(Color.Black, 0, 0, rectangle, options);

        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the vignette.</param>
        /// <param name="radiusX">The the x-radius.</param>
        /// <param name="radiusY">The the y-radius.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <param name="options">The options effecting pixel blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Vignette(this IImageOperations source, Color color, float radiusX, float radiusY, Rectangle rectangle, GraphicsOptions options)
         => source.Vignette(color, radiusX, radiusY, rectangle, options);

        private static IImageOperations Vignette(this IImageOperations source, Color color, ValueSize radiusX, ValueSize radiusY, Rectangle rectangle, GraphicsOptions options)
            => source.ApplyProcessor(new VignetteProcessor(color, radiusX, radiusY, options), rectangle);

        private static IImageOperations Vignette(this IImageOperations source, Color color, ValueSize radiusX, ValueSize radiusY, GraphicsOptions options)
            => source.ApplyProcessor(new VignetteProcessor(color, radiusX, radiusY, options));
    }
}