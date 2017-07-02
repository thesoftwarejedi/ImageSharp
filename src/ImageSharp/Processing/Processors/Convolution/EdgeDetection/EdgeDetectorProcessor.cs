// <copyright file="EdgeDetectorProcessor.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Processing.Processors
{
    using System;

    using ImageSharp.Memory;
    using ImageSharp.PixelFormats;
    using SixLabors.Primitives;

    /// <summary>
    /// Defines a sampler that detects edges within an image using a single two dimensional matrix.
    /// </summary>
    internal abstract class EdgeDetectorProcessor : ImageProcessor, IEdgeDetectorProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeDetectorProcessor"/> class.
        /// </summary>
        /// <param name="kernelXY">The 2d gradient operator.</param>
        protected EdgeDetectorProcessor(Fast2DArray<float> kernelXY)
        {
            this.KernelXY = kernelXY;
        }

        /// <inheritdoc/>
        public bool Grayscale { get; set; }

        /// <summary>
        /// Gets the 2d gradient operator.
        /// </summary>
        public Fast2DArray<float> KernelXY { get; }

        /// <inheritdoc/>
        protected override void BeforeApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
        {
            if (this.Grayscale)
            {
                new GrayscaleBt709Processor().Apply(source, sourceRectangle);
            }
        }

        /// <inheritdoc/>
        protected override void OnApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
        {
            new ConvolutionProcessor(this.KernelXY).Apply(source, sourceRectangle);
        }
    }
}