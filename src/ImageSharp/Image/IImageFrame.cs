// <copyright file="IImageFrame.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

using ImageSharp.PixelFormats;

namespace ImageSharp
{
    /// <summary>
    /// Encapsulates the basic properties and methods required to manipulate images.
    /// </summary>
    public interface IImageFrame : IImageBase
    {
        /// <summary>
        /// Gets the meta data of the image.
        /// </summary>
        ImageFrameMetaData MetaData { get; }

        /// <summary>
        /// Returns this <see cref="IImage"/> as an <see cref="Image{TPixel}"/>
        /// </summary>
        /// <typeparam name="TPixel">The pixel format you want the image data in.</typeparam>
        /// <returns>Returns self if alread in correct pixel format otherwise it converts it to the correctpixel format and returns that instead.</returns>
        ImageFrame<TPixel> As<TPixel>()
            where TPixel : struct, IPixel<TPixel>;
    }
}