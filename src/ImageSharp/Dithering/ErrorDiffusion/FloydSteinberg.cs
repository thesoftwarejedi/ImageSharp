// <copyright file="FloydSteinberg.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Dithering
{
    using SixLabors.ImageSharp.Memory;

    /// <summary>
    /// Applies error diffusion based dithering using the Floyd–Steinberg image dithering algorithm.
    /// <see href="http://www.efg2.com/Lab/Library/ImageProcessing/DHALF.TXT"/>
    /// </summary>
    public sealed class FloydSteinberg : ErrorDiffuser
    {
        /// <summary>
        /// The diffusion matrix
        /// </summary>
        private static readonly Fast2DArray<float> FloydSteinbergMatrix =
            new float[,]
            {
                { 0, 0, 7 },
                { 3, 5, 1 }
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="FloydSteinberg"/> class.
        /// </summary>
        public FloydSteinberg()
            : base(FloydSteinbergMatrix, 16)
        {
        }
    }
}