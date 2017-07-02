// <copyright file="ColorMatrixFilter.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Processing.Processors
{
    using System;
    using System.Numerics;
    using System.Threading.Tasks;

    using ImageSharp.PixelFormats;
    using SixLabors.Primitives;

    /// <summary>
    /// The color matrix filter. Inherit from this class to perform operation involving color matrices.
    /// </summary>
    internal abstract class ColorMatrixProcessor : ImageProcessor, IColorMatrixFilter
    {
        /// <inheritdoc/>
        public abstract Matrix4x4 Matrix { get; }

        /// <inheritdoc/>
        public override bool Compand { get; set; } = true;

        /// <inheritdoc/>
        protected override void OnApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
        {
            int startY = sourceRectangle.Y;
            int endY = sourceRectangle.Bottom;
            int startX = sourceRectangle.X;
            int endX = sourceRectangle.Right;

            // Align start/end positions.
            int minX = Math.Max(0, startX);
            int maxX = Math.Min(source.Width, endX);
            int minY = Math.Max(0, startY);
            int maxY = Math.Min(source.Height, endY);

            // Reset offset if necessary.
            if (minX > 0)
            {
                startX = 0;
            }

            if (minY > 0)
            {
                startY = 0;
            }

            Matrix4x4 matrix = this.Matrix;
            bool compand = this.Compand;

            using (PixelAccessor<TPixel> sourcePixels = source.Lock())
            {
                Parallel.For(
                    minY,
                    maxY,
                    this.ParallelOptions,
                    y =>
                        {
                            Span<TPixel> row = source.GetRowSpan(y - startY);

                            for (int x = minX; x < maxX; x++)
                            {
                                ref TPixel pixel = ref row[x - startX];
                                var vector = pixel.ToVector4();

                                if (compand)
                                {
                                    vector = vector.Expand();
                                }

                                vector = Vector4.Transform(vector, matrix);
                                pixel.PackFromVector4(compand ? vector.Compress() : vector);
                            }
                        });
            }
        }
    }
}