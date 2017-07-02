// <copyright file="ColorConversionTests.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Tests.Drawing
{
    using System;
    using System.IO;

    using ImageSharp.Drawing;
    using ImageSharp.Drawing.Brushes;
    using ImageSharp.Memory;
    using ImageSharp.PixelFormats;
    using Xunit;

    public class FillPatternBrushTests : FileTestBase
    {
        private void Test(string name, Color background, Brush brush, Rgba32[,] expectedPattern)
        {
            string path = this.CreateOutputDirectory("Fill", "PatternBrush");
            using (Image<Rgba32> image = new Image<Rgba32>(20, 20))
            {
                image.Mutate(x => x
                    .Fill(background)
                    .Fill(brush));

                image.Save($"{path}/{name}.png");

                using (PixelAccessor<Rgba32> sourcePixels = image.Lock())
                {
                    // lets pick random spots to start checking
                    Random r = new Random();
                    Fast2DArray<Rgba32> expectedPatternFast = new Fast2DArray<Rgba32>(expectedPattern);
                    int xStride = expectedPatternFast.Width;
                    int yStride = expectedPatternFast.Height;
                    int offsetX = r.Next(image.Width / xStride) * xStride;
                    int offsetY = r.Next(image.Height / yStride) * yStride;
                    for (int x = 0; x < xStride; x++)
                    {
                        for (int y = 0; y < yStride; y++)
                        {
                            int actualX = x + offsetX;
                            int actualY = y + offsetY;
                            Rgba32 expected = expectedPatternFast[y, x]; // inverted pattern
                            Rgba32 actual = sourcePixels[actualX, actualY];
                            if (expected != actual)
                            {
                                Assert.True(false, $"Expected {expected} but found {actual} at ({actualX},{actualY})");
                            }
                        }
                    }
                }
                image.Mutate(x => x.Resize(80, 80));
                image.Save($"{path}/{name}x4.png");
            }
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithPercent10()
        {
            this.Test("Percent10", Color.Blue, Brushes.Percent10(Color.HotPink, Color.LimeGreen),
                new[,]
                {
                { Rgba32.HotPink , Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.HotPink , Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen}
            });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithPercent10Transparent()
        {
            Test("Percent10_Transparent", Color.Blue, Brushes.Percent10(Color.HotPink),
            new Rgba32[,] {
                { Rgba32.HotPink , Rgba32.Blue, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.Blue, Rgba32.Blue, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.Blue, Rgba32.Blue, Rgba32.HotPink , Rgba32.Blue},
                { Rgba32.Blue, Rgba32.Blue, Rgba32.Blue, Rgba32.Blue}
            });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithPercent20()
        {
            Test("Percent20", Color.Blue, Brushes.Percent20(Color.HotPink, Color.LimeGreen),
           new Rgba32[,] {
                { Rgba32.HotPink , Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.HotPink , Rgba32.LimeGreen},
                { Rgba32.HotPink , Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.HotPink , Rgba32.LimeGreen}
           });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithPercent20_transparent()
        {
            Test("Percent20_Transparent", Color.Blue, Brushes.Percent20(Color.HotPink),
           new Rgba32[,] {
                { Rgba32.HotPink , Rgba32.Blue, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.Blue, Rgba32.Blue, Rgba32.HotPink , Rgba32.Blue},
                { Rgba32.HotPink , Rgba32.Blue, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.Blue, Rgba32.Blue, Rgba32.HotPink , Rgba32.Blue}
           });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithHorizontal()
        {
            Test("Horizontal", Color.Blue, Brushes.Horizontal(Color.HotPink, Color.LimeGreen),
           new Rgba32[,] {
                { Rgba32.LimeGreen , Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.HotPink, Rgba32.HotPink, Rgba32.HotPink , Rgba32.HotPink},
                { Rgba32.LimeGreen , Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen , Rgba32.LimeGreen}
           });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithHorizontal_transparent()
        {
            Test("Horizontal_Transparent", Color.Blue, Brushes.Horizontal(Color.HotPink),
           new Rgba32[,] {
                { Rgba32.Blue , Rgba32.Blue, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.HotPink, Rgba32.HotPink, Rgba32.HotPink , Rgba32.HotPink},
                { Rgba32.Blue , Rgba32.Blue, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.Blue, Rgba32.Blue, Rgba32.Blue , Rgba32.Blue}
           });
        }



        [Fact]
        public void ImageShouldBeFloodFilledWithMin()
        {
            Test("Min", Color.Blue, Brushes.Min(Color.HotPink, Color.LimeGreen),
           new Rgba32[,] {
                { Rgba32.LimeGreen , Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen , Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen , Rgba32.LimeGreen},
                { Rgba32.HotPink, Rgba32.HotPink, Rgba32.HotPink , Rgba32.HotPink}
           });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithMin_transparent()
        {
            Test("Min_Transparent", Color.Blue, Brushes.Min(Color.HotPink),
           new Rgba32[,] {
                { Rgba32.Blue , Rgba32.Blue, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.Blue , Rgba32.Blue, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.Blue, Rgba32.Blue, Rgba32.Blue , Rgba32.Blue},
                { Rgba32.HotPink, Rgba32.HotPink, Rgba32.HotPink , Rgba32.HotPink},
           });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithVertical()
        {
            Test("Vertical", Color.Blue, Brushes.Vertical(Color.HotPink, Color.LimeGreen),
           new Rgba32[,] {
                { Rgba32.LimeGreen, Rgba32.HotPink, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.HotPink, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.HotPink, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.HotPink, Rgba32.LimeGreen, Rgba32.LimeGreen}
           });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithVertical_transparent()
        {
            Test("Vertical_Transparent", Color.Blue, Brushes.Vertical(Color.HotPink),
           new Rgba32[,] {
                { Rgba32.Blue, Rgba32.HotPink, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.Blue, Rgba32.HotPink, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.Blue, Rgba32.HotPink, Rgba32.Blue, Rgba32.Blue},
                { Rgba32.Blue, Rgba32.HotPink, Rgba32.Blue, Rgba32.Blue}
           });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithForwardDiagonal()
        {
            Test("ForwardDiagonal", Color.Blue, Brushes.ForwardDiagonal(Color.HotPink, Color.LimeGreen),
           new Rgba32[,] {
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.HotPink},
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.HotPink, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.HotPink, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.HotPink, Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen}
           });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithForwardDiagonal_transparent()
        {
            Test("ForwardDiagonal_Transparent", Color.Blue, Brushes.ForwardDiagonal(Color.HotPink),
           new Rgba32[,] {
                { Rgba32.Blue,    Rgba32.Blue,    Rgba32.Blue,    Rgba32.HotPink},
                { Rgba32.Blue,    Rgba32.Blue,    Rgba32.HotPink, Rgba32.Blue},
                { Rgba32.Blue,    Rgba32.HotPink, Rgba32.Blue,    Rgba32.Blue},
                { Rgba32.HotPink, Rgba32.Blue,    Rgba32.Blue,    Rgba32.Blue}
           });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithBackwardDiagonal()
        {
            Test("BackwardDiagonal", Color.Blue, Brushes.BackwardDiagonal(Color.HotPink, Color.LimeGreen),
           new Rgba32[,] {
                { Rgba32.HotPink,   Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.HotPink,   Rgba32.LimeGreen, Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.HotPink,   Rgba32.LimeGreen},
                { Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.LimeGreen, Rgba32.HotPink}
           });
        }

        [Fact]
        public void ImageShouldBeFloodFilledWithBackwardDiagonal_transparent()
        {
            Test("BackwardDiagonal_Transparent", Color.Blue, Brushes.BackwardDiagonal(Color.HotPink),
           new Rgba32[,] {
                { Rgba32.HotPink, Rgba32.Blue,    Rgba32.Blue,    Rgba32.Blue},
                { Rgba32.Blue,    Rgba32.HotPink, Rgba32.Blue,    Rgba32.Blue},
                { Rgba32.Blue,    Rgba32.Blue,    Rgba32.HotPink, Rgba32.Blue},
                { Rgba32.Blue,    Rgba32.Blue,    Rgba32.Blue,    Rgba32.HotPink}
           });
        }
    }
}
