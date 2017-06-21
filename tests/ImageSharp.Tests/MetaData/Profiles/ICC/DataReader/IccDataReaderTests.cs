// <copyright file="IccDataReaderTests.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Tests.Icc
{
    using System;
    using Xunit;

    public class IccDataReaderTests
    {
        [Fact]
        public void ConstructorThrowsNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new IccDataReader(null));
        }
    }
}
