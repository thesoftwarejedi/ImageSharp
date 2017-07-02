// <copyright file="EntropyCropProcessor.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Processing.Processors
{
    using System;

    using ImageSharp.PixelFormats;
    using SixLabors.Primitives;

    /// <summary>
    /// Provides methods to allow the cropping of an image to preserve areas of highest
    /// entropy.
    /// </summary>
    internal class EntropyCropProcessor : ImageProcessor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntropyCropProcessor"/> class.
        /// </summary>
        /// <param name="threshold">The threshold to split the image. Must be between 0 and 1.</param>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="threshold"/> is less than 0 or is greater than 1.
        /// </exception>
        public EntropyCropProcessor(float threshold)
        {
            Guard.MustBeBetweenOrEqualTo(threshold, 0, 1, nameof(threshold));
            this.Value = threshold;
        }

        /// <summary>
        /// Gets the threshold value.
        /// </summary>
        public float Value { get; }

        /// <inheritdoc/>
        protected override void OnApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
        {
            using (ImageBase<TPixel> temp = new Image<TPixel>(source))
            {
                // Detect the edges.
                new SobelProcessor().Apply(temp, sourceRectangle);

                // Apply threshold binarization filter.
                new BinaryThresholdProcessor(this.Value).Apply(temp, sourceRectangle);

                // Search for the first white pixels
                Rectangle rectangle = ImageMaths.GetFilteredBoundingRectangle(temp, 0);

                if (rectangle == sourceRectangle)
                {
                    return;
                }

                new CropProcessor(rectangle).Apply(source, sourceRectangle);
            }
        }
    }
}