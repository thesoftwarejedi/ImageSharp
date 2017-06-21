// <copyright file="GifDecoder.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Formats
{
    using System;
    using System.IO;

    using SixLabors.ImageSharp.PixelFormats;

    /// <summary>
    /// Decoder for generating an image out of a gif encoded stream.
    /// </summary>
    public class GifDecoder : IImageDecoder
    {
        /// <inheritdoc/>
        public Image<TPixel> Decode<TPixel>(Configuration configuration, Stream stream, IDecoderOptions options)

            where TPixel : struct, IPixel<TPixel>
        {
            IGifDecoderOptions gifOptions = GifDecoderOptions.Create(options);

            return this.Decode<TPixel>(configuration, stream, gifOptions);
        }

        /// <summary>
        /// Decodes the image from the specified stream to the <see cref="ImageBase{TPixel}"/>.
        /// </summary>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="stream">The <see cref="Stream"/> containing image data.</param>
        /// <param name="options">The options for the decoder.</param>
        /// <returns>The image thats been decoded.</returns>
        public Image<TPixel> Decode<TPixel>(Configuration configuration, Stream stream, IGifDecoderOptions options)
            where TPixel : struct, IPixel<TPixel>
        {
            return new GifDecoderCore<TPixel>(options, configuration).Decode(stream);
        }
    }
}
