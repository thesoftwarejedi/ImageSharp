// <copyright file="ImageProcessor.cs" company="James Jackson-South">
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
    internal abstract class ImageProcessor : IImageProcessor
    {
        /// <inheritdoc/>
        public virtual ParallelOptions ParallelOptions { get; set; }

        /// <inheritdoc/>
        public virtual bool Compand { get; set; } = false;

        /// <inheritdoc/>
        public void Apply<TPixel>(Image<TPixel> source, Rectangle sourceRectangle)
            where TPixel : struct, IPixel<TPixel>
        {
            if (this.ParallelOptions == null)
            {
                this.ParallelOptions = source.Configuration.ParallelOptions;
            }

            try
            {
                this.BeforeImageApply(source, sourceRectangle);

                this.BeforeApply(source, sourceRectangle);
                this.OnApply(source, sourceRectangle);
                this.AfterApply(source, sourceRectangle);

                foreach (ImageFrame<TPixel> sourceFrame in source.Frames)
                {
                    this.BeforeApply(source, sourceRectangle);

                    this.OnApply(source, sourceRectangle);
                    this.AfterApply(source, sourceRectangle);
                }

                this.AfterImageApply(source, sourceRectangle);
            }
#if DEBUG
            catch (Exception)
            {
                throw;
#else
            catch (Exception ex)
            {
                throw new ImageProcessingException($"An error occured when processing the image using {this.GetType().Name}. See the inner exception for more detail.", ex);
#endif
            }
        }

        /// <summary>
        /// Applies the processor to just a single ImageBase
        /// </summary>
        /// <param name="source">the source image</param>
        /// <param name="sourceRectangle">the target</param>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        public void Apply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
            where TPixel : struct, IPixel<TPixel>
        {
            if (this.ParallelOptions == null)
            {
                this.ParallelOptions = source.Configuration.ParallelOptions;
            }

            try
            {
                this.BeforeApply(source, sourceRectangle);
                this.OnApply(source, sourceRectangle);
                this.AfterApply(source, sourceRectangle);
            }
#if DEBUG
            catch (Exception)
            {
                throw;
#else
            catch (Exception ex)
            {
                throw new ImageProcessingException($"An error occured when processing the image using {this.GetType().Name}. See the inner exception for more detail.", ex);
#endif
            }
        }

        /// <summary>
        /// This method is called before the process is applied to prepare the processor.
        /// </summary>
        /// <param name="source">The source image. Cannot be null.</param>
        /// <param name="sourceRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to draw.
        /// </param>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        protected virtual void BeforeImageApply<TPixel>(Image<TPixel> source, Rectangle sourceRectangle)
            where TPixel : struct, IPixel<TPixel>
        {
        }

        /// <summary>
        /// This method is called before the process is applied to prepare the processor.
        /// </summary>
        /// <param name="source">The source image. Cannot be null.</param>
        /// <param name="sourceRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to draw.
        /// </param>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        protected virtual void BeforeApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
            where TPixel : struct, IPixel<TPixel>
        {
        }

        /// <summary>
        /// Applies the process to the specified portion of the specified <see cref="ImageBase{TPixel}"/> at the specified location
        /// and with the specified size.
        /// </summary>
        /// <param name="source">The source image. Cannot be null.</param>
        /// <param name="sourceRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to draw.
        /// </param>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        protected abstract void OnApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
            where TPixel : struct, IPixel<TPixel>;

        /// <summary>
        /// This method is called after the process is applied to prepare the processor.
        /// </summary>
        /// <param name="source">The source image. Cannot be null.</param>
        /// <param name="sourceRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to draw.
        /// </param>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        protected virtual void AfterApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
            where TPixel : struct, IPixel<TPixel>
        {
        }

        /// <summary>
        /// This method is called after the process is applied to prepare the processor.
        /// </summary>
        /// <param name="source">The source image. Cannot be null.</param>
        /// <param name="sourceRectangle">
        /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to draw.
        /// </param>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        protected virtual void AfterImageApply<TPixel>(Image<TPixel> source, Rectangle sourceRectangle)
            where TPixel : struct, IPixel<TPixel>
        {
        }
    }
}