// <copyright file="IImage.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp
{
    using Formats;

    /// <summary>
    /// Encapsulates the basic properties and methods required to manipulate images.
    /// </summary>
    internal interface IImage : IImageBase
    {
        /// <summary>
        /// Gets the currently loaded image format.
        /// </summary>
        IImageFormat CurrentImageFormat { get; }

        /// <summary>
        /// Gets the meta data of the image.
        /// </summary>
        ImageMetaData MetaData { get; }
    }
}