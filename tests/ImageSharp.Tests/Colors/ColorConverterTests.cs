// <copyright file="ColorConstructorTests.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Tests.Colors
{
    using System.Collections.Generic;
    using System.Numerics;
    using Xunit;

    public class ColorConverterTests
    {
        public static TheoryData<byte, byte, byte, byte, string> HexData = new TheoryData<byte, byte, byte, byte, string>() {
            { 255, 255, 255, 255, "FFFFFFFF" },
            { 255, 255, 255, 0, "FFFFFF00" },
            { 255, 255, 0, 255, "FFFF00FF" },
            { 255, 0, 255, 255, "FF00FFFF" },
            { 0, 255, 255, 255, "00FFFFFF" }
        };

        [Theory]
        [MemberData(nameof(HexData))]
        public void ToHex(byte r, byte g, byte b, byte a, string hexExpected)
        {
            var color = ColorConverter<Color>.FromRGBA(r, g, b, a);

            var hex = ColorConverter<Color>.ToHex(color);
            Assert.Equal(hexExpected, hex, true);

            var convertedColor = ColorConverter<Color>.FromHex(hex);

            Assert.Equal(color, convertedColor);
        }

        [Theory]
        [MemberData(nameof(HexData))]
        public void FromHex(byte r, byte g, byte b, byte a, string hex)
        {
            var color = ColorConverter<Color>.FromRGBA(r, g, b, a);
            
            var convertedColor = ColorConverter<Color>.FromHex(hex);

            Assert.Equal(color, convertedColor);
        }

        public static TheoryData<byte, byte, byte, byte, string> WebData = new TheoryData<byte, byte, byte, byte, string>() {
            { 254, 254, 254, 255, "#FEFEFE" },
            { 254, 254, 254, 0, "rgba(254, 254, 254, 0)" },
            { 254, 254, 0, 255, "#FEFE00" },
            { 254, 0, 254, 255, "#FE00FE" },
            { 0, 254, 254, 255, "#00FEFE" },
            { 128, 254, 254, 255, "#80FEFE" },
            {240, 248, 255, 255, "AliceBlue" }
        };

        [Theory]
        [MemberData(nameof(WebData))]
        public void ToWeb(byte r, byte g, byte b, byte a, string expected)
        {
            var color = ColorConverter<Color>.FromRGBA(r, g, b, a);
            var actual = ColorConverter<Color>.ToWeb(color);
            Assert.Equal(expected, actual, true, true, true);
        }
    }
}
