// <copyright file="PngInterlaceMode.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Formats
{
    /// <summary>
    /// Provides enumeration of available PNG interlace modes.
    /// </summary>
    public enum PngInterlaceMode : byte
    {
        /// <summary>
        /// Non interlaced
        /// </summary>
        None = 0,

        /// <summary>
        /// Adam 7 interlacing.
        /// </summary>
        Adam7 = 1
    }
}