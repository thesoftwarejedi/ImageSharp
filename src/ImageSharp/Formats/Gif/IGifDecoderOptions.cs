// <copyright file="IGifDecoderOptions.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Formats
{
    using System.Text;

    /// <summary>
    /// Encapsulates the options for the <see cref="GifDecoder"/>.
    /// </summary>
    public interface IGifDecoderOptions : IDecoderOptions
    {
        /// <summary>
        /// Gets the encoding that should be used when reading comments.
        /// </summary>
        Encoding TextEncoding { get; }
    }
}
