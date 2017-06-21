// <copyright file="EncoderOptions.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp
{
    /// <summary>
    /// Encapsulates the shared encoder options.
    /// </summary>
    public class EncoderOptions : IEncoderOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EncoderOptions"/> class.
        /// </summary>
        public EncoderOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncoderOptions"/> class.
        /// </summary>
        /// <param name="options">The encoder options</param>
        protected EncoderOptions(IEncoderOptions options)
        {
            if (options != null)
            {
                this.IgnoreMetadata = options.IgnoreMetadata;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the metadata should be ignored when the image is being encoded.
        /// </summary>
        public bool IgnoreMetadata { get; set; } = false;
    }
}
