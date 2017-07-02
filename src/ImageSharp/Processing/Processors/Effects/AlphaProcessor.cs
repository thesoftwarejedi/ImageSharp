// <copyright file="AlphaProcessor.cs" company="James Jackson-South">
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
    /// An <see cref="IImageProcessor"/> to change the alpha component of an <see cref="Image{TPixel}"/>.
    /// </summary>
    internal class AlphaProcessor : ImageProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlphaProcessor"/> class.
        /// </summary>
        /// <param name="percent">The percentage to adjust the opacity of the image. Must be between 0 and 1.</param>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="percent"/> is less than 0 or is greater than 1.
        /// </exception>
        public AlphaProcessor(float percent)
        {
            Guard.MustBeBetweenOrEqualTo(percent, 0, 1, nameof(percent));
            this.Value = percent;
        }

        /// <summary>
        /// Gets the alpha value.
        /// </summary>
        public float Value { get; }

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

            var alphaVector = new Vector4(1, 1, 1, this.Value);

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
                        pixel.PackFromVector4(pixel.ToVector4() * alphaVector);
                    }
                });
        }
    }
}
