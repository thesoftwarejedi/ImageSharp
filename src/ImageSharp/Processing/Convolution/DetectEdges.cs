// <copyright file="DetectEdges.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;

    using ImageSharp.PixelFormats;

    using ImageSharp.Processing;
    using Processing.Processors;
    using SixLabors.Primitives;

    /// <summary>
    /// Extension methods for the <see cref="Image{TPixel}"/> type.
    /// </summary>
    public static partial class ImageExtensions
    {
        /// <summary>
        /// Detects any edges within the image. Uses the <see cref="SobelProcessor"/> filter
        /// operating in Grayscale mode.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations DetectEdges(this IImageOperations source)
        {
            return DetectEdges(source, new SobelProcessor { Grayscale = true });
        }

        /// <summary>
        /// Detects any edges within the image. Uses the <see cref="SobelProcessor"/> filter
        /// operating in Grayscale mode.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations DetectEdges(this IImageOperations source, Rectangle rectangle)
        {
            return DetectEdges(source, rectangle, new SobelProcessor { Grayscale = true });
        }

        /// <summary>
        /// Detects any edges within the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="filter">The filter for detecting edges.</param>
        /// <param name="grayscale">Whether to convert the image to Grayscale first. Defaults to true.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations DetectEdges(this IImageOperations source, EdgeDetection filter, bool grayscale = true)
            => DetectEdges(source, GetProcessor(filter, grayscale));

        /// <summary>
        /// Detects any edges within the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="filter">The filter for detecting edges.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <param name="grayscale">Whether to convert the image to Grayscale first. Defaults to true.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations DetectEdges(this IImageOperations source, EdgeDetection filter, Rectangle rectangle, bool grayscale = true)
            => DetectEdges(source, rectangle, GetProcessor(filter, grayscale));

        /// <summary>
        /// Detects any edges within the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="filter">The filter for detecting edges.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations DetectEdges(this IImageOperations source, IEdgeDetectorProcessor filter)
        {
            return source.ApplyProcessor(filter);
        }

        /// <summary>
        /// Detects any edges within the image.
        /// </summary>
        /// <param name="source">The image this method extends.</param>
        /// <param name="rectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
        /// </param>
        /// <param name="filter">The filter for detecting edges.</param>
        /// <returns>The <see cref="Image{TPixel}"/>.</returns>
        public static IImageOperations DetectEdges(this IImageOperations source, Rectangle rectangle, IEdgeDetectorProcessor filter)
        {
            source.ApplyProcessor(filter, rectangle);
            return source;
        }

        private static IEdgeDetectorProcessor GetProcessor(EdgeDetection filter, bool grayscale)
        {
            IEdgeDetectorProcessor processor;

            switch (filter)
            {
                case EdgeDetection.Kayyali:
                    processor = new KayyaliProcessor { Grayscale = grayscale };
                    break;

                case EdgeDetection.Kirsch:
                    processor = new KirschProcessor { Grayscale = grayscale };
                    break;

                case EdgeDetection.Lapacian3X3:
                    processor = new Laplacian3X3Processor { Grayscale = grayscale };
                    break;

                case EdgeDetection.Lapacian5X5:
                    processor = new Laplacian5X5Processor { Grayscale = grayscale };
                    break;

                case EdgeDetection.LaplacianOfGaussian:
                    processor = new LaplacianOfGaussianProcessor { Grayscale = grayscale };
                    break;

                case EdgeDetection.Prewitt:
                    processor = new PrewittProcessor { Grayscale = grayscale };
                    break;

                case EdgeDetection.RobertsCross:
                    processor = new RobertsCrossProcessor { Grayscale = grayscale };
                    break;

                case EdgeDetection.Robinson:
                    processor = new RobinsonProcessor { Grayscale = grayscale };
                    break;

                case EdgeDetection.Scharr:
                    processor = new ScharrProcessor { Grayscale = grayscale };
                    break;

                default:
                    processor = new SobelProcessor { Grayscale = grayscale };
                    break;
            }

            return processor;
        }
    }
}