// <copyright file="FlipType.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Processing
{
    /// <summary>
    /// Provides enumeration over how a image should be flipped.
    /// </summary>
    public enum FlipType
    {
        /// <summary>
        /// Don't flip the image.
        /// </summary>
        None,

        /// <summary>
        /// Flip the image horizontally.
        /// </summary>
        Horizontal,

        /// <summary>
        /// Flip the image vertically.
        /// </summary>
        Vertical,
    }
}
