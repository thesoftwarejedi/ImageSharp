// <copyright file="IColorVector.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.ColorSpaces
{
    using System.Numerics;

    /// <summary>
    /// Color represented as a vector in its color space
    /// </summary>
    public interface IColorVector
    {
        /// <summary>
        /// Gets the vector representation of the color
        /// </summary>
        Vector3 Vector { get; }
    }
}