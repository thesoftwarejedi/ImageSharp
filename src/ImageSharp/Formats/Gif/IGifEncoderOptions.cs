// <copyright file="IGifEncoderOptions.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Formats
{
    using System.Text;

    using Quantizers;

    /// <summary>
    /// Encapsulates the options for the <see cref="GifEncoder"/>.
    /// </summary>
    public interface IGifEncoderOptions : IEncoderOptions
    {
        /// <summary>
        /// Gets the encoding that should be used when writing comments.
        /// </summary>
        Encoding TextEncoding { get; }

        /// <summary>
        /// Gets the quality of output for images.
        /// </summary>
        /// <remarks>For gifs the value ranges from 1 to 256.</remarks>
        int Quality { get; }

        /// <summary>
        /// Gets the transparency threshold.
        /// </summary>
        byte Threshold { get; }

        /// <summary>
        /// Gets the quantizer for reducing the color count.
        /// </summary>
        IQuantizer Quantizer { get; }
    }
}
