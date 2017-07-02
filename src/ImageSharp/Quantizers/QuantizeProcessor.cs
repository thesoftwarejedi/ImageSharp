// <copyright file="Quantize.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Threading.Tasks;

    using ImageSharp.PixelFormats;
    using ImageSharp.Processing;
    using ImageSharp.Quantizers;
    using SixLabors.Primitives;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    internal sealed class QuantizeProcessor : ImageProcessor
    {
        private readonly IQuantizer quantizer;
        private readonly int maxColors;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuantizeProcessor"/> class.
        /// </summary>
        /// <param name="quantizer">The quantizer.</param>
        /// <param name="maxColors">The maxColors.</param>
        public QuantizeProcessor(IQuantizer quantizer, int maxColors)
        {
            this.quantizer = quantizer;
            this.maxColors = maxColors;
        }

        /// <inheritdoc/>
        protected override void BeforeImageApply<TPixel>(Image<TPixel> source, Rectangle sourceRectangle)
        {
            // TODO : move helper logic into the processor
            QuantizedImage<TPixel> quantized = this.quantizer.Quantize(source, this.maxColors);
            int palleteCount = quantized.Palette.Length - 1;

            using (PixelAccessor<TPixel> pixels = new PixelAccessor<TPixel>(quantized.Width, quantized.Height))
            {
                Parallel.For(
                    0,
                    pixels.Height,
                    source.Configuration.ParallelOptions,
                    y =>
                    {
                        for (int x = 0; x < pixels.Width; x++)
                        {
                            int i = x + (y * pixels.Width);
                            TPixel color = quantized.Palette[Math.Min(palleteCount, quantized.Pixels[i])];
                            pixels[x, y] = color;
                        }
                    });

                source.SwapPixelsBuffers(pixels);
            }
        }

        /// <inheritdoc/>
        protected override void OnApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
        {
            // noop image only operation
        }
    }
}