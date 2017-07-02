// <copyright file="Glow.cs" company="James Jackson-South">
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
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Glow(this IImageOperations source)
        {
            return Glow(source, GraphicsOptions.Default);
        }

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the glow.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Glow(this IImageOperations source, Color color)
        {
            return Glow(source, color, GraphicsOptions.Default);
        }

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="radius">The the radius.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Glow(this IImageOperations source, float radius)
        {
            return Glow(source, radius, GraphicsOptions.Default);
        }

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Glow(this IImageOperations source, Rectangle rectangle)
        => source.Glow(rectangle, GraphicsOptions.Default);

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the glow.</param>
        /// <param name="radius">The the radius.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Glow(this IImageOperations source, Color color, float radius, Rectangle rectangle)
        => source.Glow(color, ValueSize.Absolute(radius), rectangle, GraphicsOptions.Default);

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="options">The options effecting things like blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Glow(this IImageOperations source, GraphicsOptions options)
        => source.Glow(Color.Black, ValueSize.PercentageOfWidth(0.5f), options);

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the glow.</param>
        /// <param name="options">The options effecting things like blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Glow(this IImageOperations source, Color color, GraphicsOptions options)
        => source.Glow(color, ValueSize.PercentageOfWidth(0.5f), options);

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="radius">The the radius.</param>
        /// <param name="options">The options effecting things like blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Glow(this IImageOperations source, float radius, GraphicsOptions options)
        => source.Glow(Color.Black, ValueSize.Absolute(radius), options);

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <param name="options">The options effecting things like blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Glow(this IImageOperations source, Rectangle rectangle, GraphicsOptions options)
        => source.Glow(Color.Black, ValueSize.PercentageOfWidth(0.5f), rectangle, options);

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the glow.</param>
        /// <param name="radius">The the radius.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <param name="options">The options effecting things like blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Glow(this IImageOperations source, Color color, float radius, Rectangle rectangle, GraphicsOptions options)
        => source.Glow(color, ValueSize.Absolute(radius), rectangle, options);

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the glow.</param>
        /// <param name="radius">The the radius.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <param name="options">The options effecting things like blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        private static IImageOperations Glow(this IImageOperations source, Color color, ValueSize radius, Rectangle rectangle, GraphicsOptions options)
        => source.ApplyProcessor(new GlowProcessor(color, radius, options), rectangle);

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the glow.</param>
        /// <param name="radius">The the radius.</param>
        /// <param name="options">The options effecting things like blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        private static IImageOperations Glow(this IImageOperations source, Color color, ValueSize radius, GraphicsOptions options)
        => source.ApplyProcessor(new GlowProcessor(color, radius, options));
    }
}
