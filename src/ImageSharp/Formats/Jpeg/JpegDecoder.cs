// <copyright file="JpegDecoder.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Formats
{
    using System;
    using System.IO;

    using SixLabors.ImageSharp.PixelFormats;

    /// <summary>
    /// Image decoder for generating an image out of a jpg stream.
    /// </summary>
    public class JpegDecoder : IImageDecoder
    {
        /// <inheritdoc/>
        public Image<TPixel> Decode<TPixel>(Configuration configuration, Stream stream, IDecoderOptions options)
            where TPixel : struct, IPixel<TPixel>
        {
            Guard.NotNull(stream, "stream");

            using (JpegDecoderCore decoder = new JpegDecoderCore(options, configuration))
            {
                return decoder.Decode<TPixel>(stream);
            }
        }
    }
}
