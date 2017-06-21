// <copyright file="IImageBase{TPixel}.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp
{
    using System;
    using SixLabors.ImageSharp.PixelFormats;

    /// <summary>
    /// Encapsulates the basic properties and methods required to manipulate images in varying formats.
    /// </summary>
    /// <typeparam name="TPixel">The pixel format.</typeparam>
    public interface IImageBase<TPixel> : IImageBase, IDisposable
        where TPixel : struct, IPixel<TPixel>
    {
        /// <summary>
        /// Gets the representation of the pixels as an area of contiguous memory in the given pixel format.
        /// </summary>
        Span<TPixel> Pixels { get; }
    }
}