// <copyright file="DelegateImageProcessor.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Processing
{
    using System;
    using System.Threading.Tasks;

    using ImageSharp.PixelFormats;
    using SixLabors.Primitives;

    /// <summary>
    /// Allows the application of processors to images.
    /// </summary>
    internal class DelegateImageProcessor : ImageProcessor
    {
        private readonly Action<IImage> action;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateImageProcessor"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        public DelegateImageProcessor(Action<IImage> action)
        {
            this.action = action;
        }

        /// <inheritdoc/>
        protected override void BeforeImageApply<TPixel>(Image<TPixel> source, Rectangle sourceRectangle)
        {
            this.action?.Invoke((Image<TPixel>)source);
        }

        /// <inheritdoc/>
        protected override void OnApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
        {
            // no op, we did all we wanted to do inside BeforeImageApply
        }
    }
}