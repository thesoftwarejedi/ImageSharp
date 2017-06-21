// <copyright file="FlipTest.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Tests.Processing.Transforms
{
    using SixLabors.ImageSharp.PixelFormats;
    using SixLabors.ImageSharp.Processing;

    using Xunit;

    public class FlipTests : FileTestBase
    {
        public static readonly string[] FlipFiles = { TestImages.Bmp.F };

        public static readonly TheoryData<FlipType> FlipValues
            = new TheoryData<FlipType>
        {
            { FlipType.None },
            { FlipType.Vertical },
            { FlipType.Horizontal },
        };

        [Theory]
        [WithFileCollection(nameof(FlipFiles), nameof(FlipValues), DefaultPixelType)]
        public void ImageShouldFlip<TPixel>(TestImageProvider<TPixel> provider, FlipType flipType)
            where TPixel : struct, IPixel<TPixel>
        {
            using (Image<TPixel> image = provider.GetImage())
            {
                image.Flip(flipType)
                    .DebugSave(provider, flipType, Extensions.Bmp);
            }
        }
    }
}