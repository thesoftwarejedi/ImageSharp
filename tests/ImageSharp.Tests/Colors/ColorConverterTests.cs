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
        }

        [Theory]
        [MemberData(nameof(HexData))]
        public void FromHex(byte r, byte g, byte b, byte a, string hex)
        {
            var color = ColorConverter<Color>.FromRGBA(r, g, b, a);

            var convertedColor = ColorConverter<Color>.FromHex(hex);
            Assert.Equal(color, convertedColor);
        }

        public static TheoryData<byte, byte, byte, byte, string> FromWebData = new TheoryData<byte, byte, byte, byte, string>() {
            { 255, 255, 255, 255, "#fff" },
            { 254, 254, 254, 255, "#FEFEFE" },
            { 221, 221, 221, 255, "#ddd" },
            { 254, 254, 254, 0, "rgba(254, 254, 254, 0)" },
            { 254, 254, 0, 255, "#FEFE00" },
            { 254, 0, 254, 255, "#FE00FE" },
            { 0, 254, 254, 255, "#00FEFE" },
            { 128, 254, 254, 255, "#80FEFE" },
            { 240, 248, 255, 255, "AliceBlue" },
            { 255, 0, 0, 255, "red" }, // case insensative
            { 255, 255, 255, 127, "rgba(100%, 100%, 100%, 0.5)" },
            { 255, 127, 255, 255, "rgb(100%, 50%, 100%)" },
            { 255, 128, 255, 127, "rgba(255, 128, 255, 0.5)" },
            { 255, 128, 255, 255, "rgb(255, 128, 255)" },
            { 255, 128, 255, 255, "RGB(255, 128, 255)" }, // case insensitive
        };

        [Theory]
        [MemberData(nameof(FromWebData))]
        public void FromWeb(byte r, byte g, byte b, byte a, string webString)
        {
            var color = ColorConverter<Color>.FromRGBA(r, g, b, a);
            var actual = ColorConverter<Color>.FromWeb(webString);
            Assert.Equal(color, actual);
        }

        public static IEnumerable<object[]> ToWebExactData = new[]{
            new object[] { 255, 255, 255, 255, WebStringFormat.Hex, "#fff" },
            new object[] { 254, 255, 255, 255, WebStringFormat.Hex, "#feffff" }, // can reduce but return the full hex
            new object[] { 254, 254, 254, 255, WebStringFormat.HexFull, "#FEFEFE" },
            new object[] { 255, 255, 255, 255, WebStringFormat.HexFull, "#FFFFFF" }, //can reduce but asked for full hex
            new object[] { 254, 254, 254, 0, WebStringFormat.RgbaFunctionBytes, "rgba(254, 254, 254, 0)" },
            new object[] { 240, 248, 255, 255, WebStringFormat.Name, "AliceBlue" },
            new object[] { 240, 248, 255, 0, WebStringFormat.Name, null }, // no name found returned null
            new object[] { 255, 255, 255, 127, WebStringFormat.RgbaFunctionPercentages, "rgba(100%, 100%, 100%, 0.5)" },
            new object[] { 255, 127, 255, 255, WebStringFormat.RgbFunctionPercentages, "rgb(100%, 49.8%, 100%)" },
            new object[] { 255, 128, 255, 255, WebStringFormat.RgbaFunctionPercentages, "rgba(100%, 50.2%, 100%, 1)" },
            new object[] { 255, 127, 255, 200, WebStringFormat.RgbFunctionPercentages, null }, // cant do just rgb needs to set opacity
            new object[] { 255, 128, 255, 127, WebStringFormat.RgbaFunctionBytes, "rgba(255, 128, 255, 0.5)" },
            new object[] { 255, 128, 255, 255, WebStringFormat.RgbFunctionBytes,"rgb(255, 128, 255)" },
            new object[] { 255, 128, 255, 255, WebStringFormat.RgbaFunctionBytes,"rgba(255, 128, 255, 1)" },
        };

        [Theory]
        [MemberData(nameof(ToWebExactData))]
        public void ToWebExact(byte r, byte g, byte b, byte a, WebStringFormat format, string webString)
        {
            var color = ColorConverter<Color>.FromRGBA(r, g, b, a);
            var actual = ColorConverter<Color>.ToWebExact(color, format);
            Assert.Equal(webString, actual, true);
        }

        public static IEnumerable<object[]> ToWebData = new[]{
            new object[] { 255, 255, 255, 255, "White" },
            new object[] { 254, 255, 255, 255, "#feffff" },
            new object[] { 254, 254, 254, 255, "#FEFEFE" },
            new object[] { 221, 221, 221, 255, "#ddd" },
            new object[] { 254, 254, 254, 0, "rgba(254, 254, 254, 0)" },
            new object[] { 240, 248, 255, 255, "AliceBlue" },
            new object[] { 240, 248, 255, 0,  "rgba(240, 248, 255, 0)" },
            new object[] { 255, 255, 255, 127, "rgba(255, 255, 255, 0.5)" },
            new object[] { 255, 127, 255, 255, "#FF7FFF" },
            new object[] { 255, 127, 255, 200, "rgba(255, 127, 255, 0.78)" },
            new object[] { 255, 128, 255, 127, "rgba(255, 128, 255, 0.5)" },
            new object[] { 255, 128, 255, 255, "#FF80FF" },
        };

        [Theory]
        [MemberData(nameof(ToWebData))]
        public void ToWeb(byte r, byte g, byte b, byte a, string webString)
        {
            var color = ColorConverter<Color>.FromRGBA(r, g, b, a);
            var actual = ColorConverter<Color>.ToWeb(color);
            Assert.Equal(webString, actual, true);
        }
    }
}
