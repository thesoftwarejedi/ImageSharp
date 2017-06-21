// <copyright file="BmpFormat.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Formats
{
    using System.Collections.Generic;

    /// <summary>
    /// Encapsulates the means to encode and decode bitmap images.
    /// </summary>
    public class BmpFormat : IImageFormat
    {
        /// <inheritdoc/>
        public string MimeType => "image/bmp";

        /// <inheritdoc/>
        public string Extension => "bmp";

        /// <inheritdoc/>
        public IEnumerable<string> SupportedExtensions => new string[] { "bmp", "dip" };

        /// <inheritdoc/>
        public IImageDecoder Decoder => new BmpDecoder();

        /// <inheritdoc/>
        public IImageEncoder Encoder => new BmpEncoder();

        /// <inheritdoc/>
        public int HeaderSize => 2;

        /// <inheritdoc/>
        public bool IsSupportedFileFormat(byte[] header)
        {
            return header.Length >= this.HeaderSize &&
                   header[0] == 0x42 && // B
                   header[1] == 0x4D;   // M
        }
    }
}
