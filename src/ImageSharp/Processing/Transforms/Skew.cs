// <copyright file="Skew.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;

    using ImageSharp.PixelFormats;

    using Processing.Processors;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Skews an image by the given angles in degrees, expanding the image to fit the skewed result.
        /// </summary>
        /// <param name="source">The image to skew.</param>
        /// <param name="degreesX">The angle in degrees to perform the rotation along the x-axis.</param>
        /// <param name="degreesY">The angle in degrees to perform the rotation along the y-axis.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        public static IImageOperations Skew(this IImageOperations source, float degreesX, float degreesY)
        {
            return Skew(source, degreesX, degreesY, true);
        }

        /// <summary>
        /// Skews an image by the given angles in degrees.
        /// </summary>
        /// <param name="source">The image to skew.</param>
        /// <param name="degreesX">The angle in degrees to perform the rotation along the x-axis.</param>
        /// <param name="degreesY">The angle in degrees to perform the rotation along the y-axis.</param>
        /// <param name="expand">Whether to expand the image to fit the skewed result.</param>
        /// <returns>The <see cref="Image{TPixel}"/></returns>
        public static IImageOperations Skew(this IImageOperations source, float degreesX, float degreesY, bool expand)
        => source.ApplyProcessor(new SkewProcessor { AngleX = degreesX, AngleY = degreesY, Expand = expand });
    }
}
