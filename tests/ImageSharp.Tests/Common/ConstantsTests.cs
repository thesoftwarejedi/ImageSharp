// <copyright file="ConstantsTests.cs" company="Six Labors">
// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace SixLabors.ImageSharp.Tests.Common
{
    using Xunit;

    public class ConstantsTests
    {
        [Fact]
        public void Epsilon()
        {
            Assert.Equal(0.001f, Constants.Epsilon);
        }
    }
}
