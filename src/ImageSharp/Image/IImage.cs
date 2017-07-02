// <copyright file="IImage.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Formats;
    using ImageSharp.PixelFormats;
    using ImageSharp.Processing;
    using SixLabors.Primitives;

    /// <summary>
    /// Encapsulates the basic properties and methods required to manipulate images.
    /// </summary>
    public interface IImage : IImageBase, IDisposable
    {
        /// <summary>
        /// Gets the current Pixel Format of the underlying image data
        /// </summary>
        Type PixelFormat { get; }

        /// <summary>
        /// Gets the meta data of the image.
        /// </summary>
        ImageMetaData MetaData { get; }

        /// <summary>
        /// Gets the other frames for the animation.
        /// </summary>
        /// <value>The list of frame images.</value>
        IList<IImageFrame> Frames { get; }

        /// <summary>
        /// Saves the image to the given stream using the currently loaded image format.
        /// </summary>
        /// <param name="stream">The stream to save the image to.</param>
        /// <param name="format">The format to save the image to.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the stream is null.</exception>
        void Save(Stream stream, IImageFormat format);

        /// <summary>
        /// Applies the processor to the image.
        /// </summary>
        /// <param name="processor">The processor to apply to the image.</param>
        /// <param name="rectangle">The <see cref="Rectangle" /> structure that specifies the portion of the image object to draw.</param>
        void ApplyProcessor(IImageProcessor processor, Rectangle rectangle);

        /// <summary>
        /// Saves the image to the given stream using the given image encoder.
        /// </summary>
        /// <param name="stream">The stream to save the image to.</param>
        /// <param name="encoder">The encoder to save the image with.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the stream or encoder is null.</exception>
        void Save(Stream stream, IImageEncoder encoder);

#if !NETSTANDARD1_1
        /// <summary>
        /// Saves the image to the given stream using the currently loaded image format.
        /// </summary>
        /// <param name="filePath">The file path to save the image to.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the stream is null.</exception>
        void Save(string filePath);

        /// <summary>
        /// Saves the image to the given stream using the currently loaded image format.
        /// </summary>
        /// <param name="filePath">The file path to save the image to.</param>
        /// <param name="encoder">The encoder to save the image with.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the encoder is null.</exception>
        void Save(string filePath, IImageEncoder encoder);
#endif

        /// <summary>
        /// Returns this <see cref="IImage"/> as an <see cref="Image{TPixel}"/>
        /// </summary>
        /// <typeparam name="TPixel">The pixel format you want the image data in.</typeparam>
        /// <returns>Returns self if alread in correct pixel format otherwise it converts it to the correctpixel format and returns that instead.</returns>
        Image<TPixel> As<TPixel>()
            where TPixel : struct, IPixel<TPixel>;
    }
}