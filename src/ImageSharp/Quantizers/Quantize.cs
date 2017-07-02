// <copyright file="Quantize.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Threading.Tasks;

    using ImageSharp.PixelFormats;
    using ImageSharp.Quantizers;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Applies quantization to the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="mode">The quantization mode to apply to perform the operation.</param>
        /// <param name="maxColors">The maximum number of colors to return. Defaults to 256.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Quantize(this IImageOperations source, Quantization mode = Quantization.Octree, int maxColors = 256)
        {
            IQuantizer quantizer;
            switch (mode)
            {
                case Quantization.Wu:
                    quantizer = new WuQuantizer();
                    break;

                case Quantization.Palette:
                    quantizer = new PaletteQuantizer();
                    break;

                default:
                    quantizer = new OctreeQuantizer();
                    break;
            }

            return Quantize(source, quantizer, maxColors);
        }

        /// <summary>
        /// Applies quantization to the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="quantizer">The quantizer to apply to perform the operation.</param>
        /// <param name="maxColors">The maximum number of colors to return.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations Quantize(this IImageOperations source, IQuantizer quantizer, int maxColors)
        {
            return source.ApplyProcessor(new QuantizeProcessor(quantizer, maxColors));
        }
    }
}