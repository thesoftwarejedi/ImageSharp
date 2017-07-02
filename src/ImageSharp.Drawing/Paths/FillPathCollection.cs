// <copyright file="FillPaths.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using Drawing;
    using Drawing.Brushes;
    using ImageSharp.PixelFormats;
    using SixLabors.Shapes;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Flood fills the image in the shape of the provided polygon with the specified brush..
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="paths">The shapes.</param>
        /// <param name="options">The graphics options.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Fill(this IImageOperations source, Brush brush, IPathCollection paths, GraphicsOptions options)
        {
            foreach (IPath s in paths)
            {
                source.Fill(brush, s, options);
            }

            return source;
        }

        /// <summary>
        /// Flood fills the image in the shape of the provided polygon with the specified brush.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="paths">The paths.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Fill(this IImageOperations source, Brush brush, IPathCollection paths)
        {
            return source.Fill(brush, paths, GraphicsOptions.Default);
        }
    }
}
