// <copyright file="BackgroundColor.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;

    using ImageSharp.PixelFormats;

    using Processing.Processors;
    using SixLabors.Primitives;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Replaces the background color of image with the given one.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the background.</param>
        /// <param name="options">The options effecting pixel blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations BackgroundColor(this IImageOperations source, Color color, GraphicsOptions options)
        => source.ApplyProcessor(new BackgroundColorProcessor(color, options));

        /// <summary>
        /// Replaces the background color of image with the given one.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the background.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <param name="options">The options effecting pixel blending.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations BackgroundColor(this IImageOperations source, Color color, Rectangle rectangle, GraphicsOptions options)
        => source.ApplyProcessor(new BackgroundColorProcessor(color, options), rectangle);

        /// <summary>
        /// Replaces the background color of image with the given one.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the background.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations BackgroundColor(this IImageOperations source, Color color)
        {
            return BackgroundColor(source, color, GraphicsOptions.Default);
        }

        /// <summary>
        /// Replaces the background color of image with the given one.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="color">The color to set as the background.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations BackgroundColor(this IImageOperations source, Color color, Rectangle rectangle)
        {
            return BackgroundColor(source, color, rectangle, GraphicsOptions.Default);
        }
    }
}
