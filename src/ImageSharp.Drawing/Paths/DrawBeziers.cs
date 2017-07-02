// <copyright file="DrawBeziers.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System.Numerics;
    using Drawing;
    using Drawing.Brushes;
    using Drawing.Pens;
    using ImageSharp.PixelFormats;
    using SixLabors.Primitives;
    using SixLabors.Shapes;

    /// <summary>
    /// Extension methods for the <see cref="Image{Color}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Draws the provided Points as an open Bezier path at the provided thickness with the supplied brush
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="points">The points.</param>
        /// <param name="options">The options.</param>
        /// <returns>The <see cref="Image{Color}"/>.</returns>
        public static IImageOperations DrawBeziers(this IImageOperations source, Brush brush, float thickness, PointF[] points, GraphicsOptions options)
        {
            return source.Draw(new Pen(brush, thickness), new Path(new CubicBezierLineSegment(points)), options);
        }

        /// <summary>
        /// Draws the provided Points as an open Bezier path at the provided thickness with the supplied brush
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="thickness">The thickness.</param>
        /// <param name="points">The points.</param>
        /// <returns>The <see cref="Image{Color}"/>.</returns>
        public static IImageOperations DrawBeziers(this IImageOperations source, Brush brush, float thickness, PointF[] points)
        {
            return source.Draw(new Pen(brush, thickness), new Path(new CubicBezierLineSegment(points)));
        }

        /// <summary>
        /// Draws the provided Points as an open Bezier path with the supplied pen
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        /// <param name="options">The options.</param>
        /// <returns>The <see cref="Image{Color}"/>.</returns>
        public static IImageOperations DrawBeziers(this IImageOperations source, Pen pen, PointF[] points, GraphicsOptions options)
        {
            return source.Draw(pen, new Path(new CubicBezierLineSegment(points)), options);
        }

        /// <summary>
        /// Draws the provided Points as an open Bezier path with the supplied pen
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="pen">The pen.</param>
        /// <param name="points">The points.</param>
        /// <returns>The <see cref="Image{Color}"/>.</returns>
        public static IImageOperations DrawBeziers(this IImageOperations source, Pen pen, PointF[] points)
        {
            return source.Draw(pen, new Path(new CubicBezierLineSegment(points)));
        }
    }
}
