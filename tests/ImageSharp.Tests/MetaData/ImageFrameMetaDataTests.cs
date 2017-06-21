// <copyright file="ImageFrameMetaDataTests.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Tests
{
    using SixLabors.ImageSharp.Formats;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="ImageFrameMetaDataTests"/> class.
    /// </summary>
    public class ImageFrameMetaDataTests
    {
        [Fact]
        public void ConstructorImageFrameMetaData()
        {
            ImageFrameMetaData metaData = new ImageFrameMetaData();
            metaData.FrameDelay = 42;
            metaData.DisposalMethod = DisposalMethod.RestoreToBackground;

            ImageFrameMetaData clone = new ImageFrameMetaData(metaData);

            Assert.Equal(42, clone.FrameDelay);
            Assert.Equal(DisposalMethod.RestoreToBackground, clone.DisposalMethod);
        }
    }
}
