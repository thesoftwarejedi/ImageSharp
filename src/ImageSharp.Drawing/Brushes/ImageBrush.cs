// <copyright file="ImageBrush.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Drawing.Brushes
{
    using System;

    using ImageSharp.Memory;
    using ImageSharp.PixelFormats;
    using Processors;
    using SixLabors.Primitives;

    /// <summary>
    /// Provides an implementation of an image brush for painting images within areas.
    /// </summary>
    public class ImageBrush : Brush
    {
        /// <summary>
        /// The image to paint.
        /// </summary>
        private readonly IImage image;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageBrush"/> class.
        /// </summary>
        /// <param name="image">The image.</param>
        public ImageBrush(IImage image)
        {
            this.image = image;
        }

        /// <inheritdoc />
        public override BrushApplicator<TPixel> CreateApplicator<TPixel>(ImageBase<TPixel> source, RectangleF region, GraphicsOptions options)
        {
            return new ImageBrushApplicator<TPixel>(source, this.image.As<TPixel>(), region, options);
        }

        /// <summary>
        /// The image brush applicator.
        /// </summary>
        /// <typeparam name="TPixel">The Pixel format</typeparam>
        private class ImageBrushApplicator<TPixel> : BrushApplicator<TPixel>
            where TPixel : struct, IPixel<TPixel>
        {
            /// <summary>
            /// The source image.
            /// </summary>
            private readonly ImageBase<TPixel> source;

            /// <summary>
            /// The y-length.
            /// </summary>
            private readonly int yLength;

            /// <summary>
            /// The x-length.
            /// </summary>
            private readonly int xLength;

            /// <summary>
            /// The Y offset.
            /// </summary>
            private readonly int offsetY;

            /// <summary>
            /// The X offset.
            /// </summary>
            private readonly int offsetX;

            public ImageBrushApplicator(ImageBase<TPixel> target, Image<TPixel> image, RectangleF region, GraphicsOptions options)
                : base(target, options)
            {
                this.source = image;
                this.xLength = image.Width;
                this.yLength = image.Height;
                this.offsetY = (int)MathF.Max(MathF.Floor(region.Top), 0);
                this.offsetX = (int)MathF.Max(MathF.Floor(region.Left), 0);
            }

            /// <summary>
            /// Gets the color for a single pixel.
            /// </summary>
            /// <param name="x">The x.</param>
            /// <param name="y">The y.</param>
            /// <returns>
            /// The color
            /// </returns>
            internal override TPixel this[int x, int y]
            {
                get
                {
                    int srcX = (x - this.offsetX) % this.xLength;
                    int srcY = (y - this.offsetY) % this.yLength;
                    return this.source[srcX, srcY];
                }
            }

            /// <inheritdoc />
            public override void Dispose()
            {
                this.source.Dispose();
            }

            /// <inheritdoc />
            internal override void Apply(Span<float> scanline, int x, int y)
            {
                // Create a span for colors
                using (var amountBuffer = new Buffer<float>(scanline.Length))
                using (var overlay = new Buffer<TPixel>(scanline.Length))
                {
                    int sourceY = (y - this.offsetY) % this.yLength;
                    int offsetX = x - this.offsetX;
                    Span<TPixel> sourceRow = this.source.GetRowSpan(sourceY);

                    for (int i = 0; i < scanline.Length; i++)
                    {
                        amountBuffer[i] = scanline[i] * this.Options.BlendPercentage;

                        int sourceX = (i + offsetX) % this.xLength;
                        TPixel pixel = sourceRow[sourceX];
                        overlay[i] = pixel;
                    }

                    Span<TPixel> destinationRow = this.Target.GetRowSpan(x, y).Slice(0, scanline.Length);
                    this.Blender.Blend(destinationRow, destinationRow, overlay, amountBuffer);
                }
            }
        }
    }
}