// <copyright file="ColorConversionTests.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Tests.Drawing
{
    using Drawing;
    using ImageSharp.Drawing;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Numerics;

    using ImageSharp.PixelFormats;

    using Xunit;

    public class FillSolidBrushTests : FileTestBase
    {
        [Fact]
        public void ImageShouldBeFloodFilledWithColorOnDefaultBackground()
        {
            string path = this.CreateOutputDirectory("Fill", "SolidBrush");
            using (Image<Rgba32> image = new Image<Rgba32>(500, 500))
            {
                image.Mutate(x => x
                    .Fill(Color.HotPink));
                image
                    .Save($"{path}/DefaultBack.png");

                using (PixelAccessor<Rgba32> sourcePixels = image.Lock())
                {
                    Assert.Equal(Rgba32.HotPink, sourcePixels[9, 9]);

                    Assert.Equal(Rgba32.HotPink, sourcePixels[199, 149]);
                }
            }
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithColor()
        {
            string path = this.CreateOutputDirectory("Fill", "SolidBrush");
            using (Image<Rgba32> image = new Image<Rgba32>(500, 500))
            {
                image.Mutate(x => x
                    .BackgroundColor(Color.Blue)
                    .Fill(Color.HotPink));
                image.Save($"{path}/Simple.png");

                using (PixelAccessor<Rgba32> sourcePixels = image.Lock())
                {
                    Assert.Equal(Rgba32.HotPink, sourcePixels[9, 9]);

                    Assert.Equal(Rgba32.HotPink, sourcePixels[199, 149]);
                }
            }
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithColorOpacity()
        {
            string path = this.CreateOutputDirectory("Fill", "SolidBrush");
            using (Image<Rgba32> image = new Image<Rgba32>(500, 500))
            {
                Color color = Color.HotPink.WithOpacity(150);

                image.Mutate(x => x
                    .BackgroundColor(Color.Blue)
                    .Fill(color));
                image.Save($"{path}/Opacity.png");

                //shift background color towards forground color by the opacity amount
                Rgba32 mergedColor = new Rgba32(Vector4.Lerp(Rgba32.Blue.ToVector4(), Rgba32.HotPink.ToVector4(), 150f / 255f));


                using (PixelAccessor<Rgba32> sourcePixels = image.Lock())
                {
                    Assert.Equal(mergedColor, sourcePixels[9, 9]);
                    Assert.Equal(mergedColor, sourcePixels[199, 149]);
                }
            }
        }

    }
}
