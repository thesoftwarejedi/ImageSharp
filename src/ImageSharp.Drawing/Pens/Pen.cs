// <copyright file="Pen{TPixel}.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Drawing.Pens
{
    using System;
    using System.Numerics;

    using ImageSharp.Drawing.Brushes;
    using ImageSharp.PixelFormats;
    using Processors;

    /// <summary>
    /// Provides a pen that can apply a pattern to a line with a set brush and thickness
    /// </summary>
    /// <remarks>
    /// The pattern will be in to the form of new float[]{ 1f, 2f, 0.5f} this will be
    /// converted into a pattern that is 3.5 times longer that the width with 3 sections
    /// section 1 will be width long (making a square) and will be filled by the brush
    /// section 2 will be width * 2 long and will be empty
    /// section 3 will be width/2 long and will be filled
    /// the the pattern will imidiatly repeat without gap.
    /// </remarks>
    public sealed class Pen
    {
        private static readonly float[] EmptyPattern = new float[0];
        private readonly float[] pattern;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSharp.Drawing.Pens.Pen"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="width">The width.</param>
        /// <param name="pattern">The pattern.</param>
        public Pen(Color color, float width, float[] pattern)
            : this(new SolidBrush(color), width, pattern)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSharp.Drawing.Pens.Pen"/> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="width">The width.</param>
        /// <param name="pattern">The pattern.</param>
        public Pen(Brush brush, float width, float[] pattern)
        {
            this.StrokeFill = brush;
            this.StrokeWidth = width;
            this.pattern = pattern;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSharp.Drawing.Pens.Pen"/> class.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <param name="width">The width.</param>
        public Pen(Color color, float width)
            : this(new SolidBrush(color), width)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSharp.Drawing.Pens.Pen"/> class.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="width">The width.</param>
        public Pen(Brush brush, float width)
            : this(brush, width, EmptyPattern)
        {
        }

        /// <summary>
        /// Gets the stroke fill.
        /// </summary>
        public Brush StrokeFill { get; }

        /// <summary>
        /// Gets the width to apply to the stroke
        /// </summary>
        public float StrokeWidth { get; }

        /// <summary>
        /// Gets the stoke pattern.
        /// </summary>
        public ReadOnlySpan<float> StrokePattern => this.pattern;
    }
}
