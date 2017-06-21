// <copyright file="EntropyCropTest.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Tests.Processing.Transforms
{
    using SixLabors.ImageSharp.PixelFormats;

    using Xunit;

    public class EntropyCropTest : FileTestBase
    {
        public static readonly TheoryData<float> EntropyCropValues
        = new TheoryData<float>
        {
            .25F,
            .75F
        };

        [Theory]
        [WithFileCollection(nameof(DefaultFiles), nameof(EntropyCropValues), DefaultPixelType)]
        public void ImageShouldEntropyCrop<TPixel>(TestImageProvider<TPixel> provider, float value)
            where TPixel : struct, IPixel<TPixel>
        {
            using (Image<TPixel> image = provider.GetImage())
            {
                image.EntropyCrop(value)
                    .DebugSave(provider, value, Extensions.Bmp);
            }
        }
    }
}