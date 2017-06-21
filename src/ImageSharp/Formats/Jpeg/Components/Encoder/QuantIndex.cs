// <copyright file="QuantIndex.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>
namespace SixLabors.ImageSharp.Formats.Jpg
{
    /// <summary>
    ///     Enumerates the quantization tables
    /// </summary>
    internal enum QuantIndex
    {
        /// <summary>
        ///     The luminance quantization table index
        /// </summary>
        Luminance = 0,

        /// <summary>
        ///     The chrominance quantization table index
        /// </summary>
        Chrominance = 1,
    }
}