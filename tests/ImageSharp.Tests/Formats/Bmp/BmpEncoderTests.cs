// <copyright file="BmpEncoderTests.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

using SixLabors.ImageSharp.Formats;

namespace SixLabors.ImageSharp.Tests
{
    using SixLabors.ImageSharp.PixelFormats;

    using Xunit;

    public class BmpEncoderTests : FileTestBase
    {
        public static readonly TheoryData<BmpBitsPerPixel> BitsPerPixel
        = new TheoryData<BmpBitsPerPixel>
        {
            BmpBitsPerPixel.Pixel24,
            BmpBitsPerPixel.Pixel32
        };

        [Theory]
        [MemberData(nameof(BitsPerPixel))]
        public void BitmapCanEncodeDifferentBitRates(BmpBitsPerPixel bitsPerPixel)
        {
            string path = this.CreateOutputDirectory("Bmp");

            foreach (TestFile file in Files)
            {
                string filename = file.GetFileNameWithoutExtension(bitsPerPixel);
                using (Image<Rgba32> image = file.CreateImage())
                {
                    image.Save($"{path}/{filename}.bmp", new BmpEncoderOptions { BitsPerPixel = bitsPerPixel });
                }
            }
        }
    }
}