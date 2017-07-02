// <copyright file="PolaroidProcessor.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Processing.Processors
{
    using System;
    using System.Numerics;

    using ImageSharp.PixelFormats;
    using SixLabors.Primitives;

    /// <summary>
    /// Converts the colors of the image recreating an old Polaroid effect.
    /// </summary>
    internal class PolaroidProcessor : ColorMatrixProcessor
    {
        private static Color veryDarkOrange = new Color(102, 34, 0);
        private static Color lightOrange = new Color(255, 153, 102, 178);
        private GraphicsOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="PolaroidProcessor" /> class.
        /// </summary>
        /// <param name="options">The options effecting blending and composition.</param>
        public PolaroidProcessor(GraphicsOptions options)
        {
            this.options = options;
        }

        /// <inheritdoc/>
        public override Matrix4x4 Matrix => new Matrix4x4()
        {
            M11 = 1.538F,
            M12 = -0.062F,
            M13 = -0.262F,
            M21 = -0.022F,
            M22 = 1.578F,
            M23 = -0.022F,
            M31 = .216F,
            M32 = -.16F,
            M33 = 1.5831F,
            M41 = 0.02F,
            M42 = -0.05F,
            M43 = -0.05F,
            M44 = 1
        };

        /// <inheritdoc/>
        protected override void AfterApply<TPixel>(ImageBase<TPixel> source, Rectangle sourceRectangle)
        {
            new VignetteProcessor(veryDarkOrange, this.options).Apply(source, sourceRectangle);
            new GlowProcessor(lightOrange, source.Width / 4F, this.options).Apply(source, sourceRectangle);
        }
    }
}