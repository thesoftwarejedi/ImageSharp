// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using System.Numerics;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace SixLabors.ImageSharp.Tests
{
    public class Bgr24Tests
    {
        public static readonly TheoryData<byte, byte, byte> ColorData =
            new TheoryData<byte, byte, byte>() { { 1, 2, 3 }, { 4, 5, 6 }, { 0, 255, 42 } };

        [Theory]
        [MemberData(nameof(ColorData))]
        public void Constructor(byte r, byte g, byte b)
        {
            var p = new Rgb24(r, g, b);

            Assert.Equal(r, p.R);
            Assert.Equal(g, p.G);
            Assert.Equal(b, p.B);
        }
        
        [Fact]
        public unsafe void ByteLayoutIsSequentialBgr()
        {
            var color = new Bgr24(3, 2, 1);
            byte* ptr = (byte*)&color;
        
            Assert.Equal(3, ptr[0]);
            Assert.Equal(2, ptr[1]);
            Assert.Equal(1, ptr[2]);
        }

        [Theory]
        [MemberData(nameof(ColorData))]
        public void Equals_WhenTrue(byte r, byte g, byte b)
        {
            var x = new Bgr24(b, g, r);
            var y = new Bgr24(b, g, r);

            Assert.True(x.Equals(y));
            Assert.True(x.Equals((object)y));
            Assert.Equal(x.GetHashCode(), y.GetHashCode());
        }

        [Theory]
        [InlineData(1, 2, 3, 1, 2, 4)]
        [InlineData(0, 255, 0, 0, 244, 0)]
        [InlineData(1, 255, 0, 0, 255, 0)]
        public void Equals_WhenFalse(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2)
        {
            var a = new Bgr24(b1, g1, r1);
            var b = new Bgr24(b2, g2, r2);

            Assert.False(a.Equals(b));
            Assert.False(a.Equals((object)b));
        }


        [Fact]
        public void PackFromRgba32()
        {
            var bgr = default(Bgr24);
            bgr.PackFromRgba32(new Rgba32(1, 2, 3, 4));

            Assert.Equal(1, bgr.R);
            Assert.Equal(2, bgr.G);
            Assert.Equal(3, bgr.B);
        }

        private static Vector4 Vec(byte r, byte g, byte b, byte a = 255) => new Vector4(
            r / 255f,
            g / 255f,
            b / 255f,
            a / 255f);

        [Fact]
        public void PackFromVector4()
        {
            var bgr = default(Bgr24);
            bgr.PackFromVector4(Vec(1, 2, 3, 4));

            Assert.Equal(1, bgr.R);
            Assert.Equal(2, bgr.G);
            Assert.Equal(3, bgr.B);
        }

        [Fact]
        public void ToVector4()
        {
            var bgr = new Bgr24(3, 2, 1);

            Assert.Equal(Vec(1, 2, 3), bgr.ToVector4());
        }

        [Fact]
        public void ToRgb24()
        {
            var bgr = new Bgr24(3, 2, 1);
            var dest = default(Rgb24);

            bgr.ToRgb24(ref dest);

            Assert.Equal(new Rgb24(1, 2, 3), dest);
        }

        [Fact]
        public void ToRgba32()
        {
            var bgr = new Bgr24(3, 2, 1);
            var rgba = default(Rgba32);

            bgr.ToRgba32(ref rgba);

            Assert.Equal(new Rgba32(1, 2, 3, 255), rgba);
        }

        [Fact]
        public void ToBgr24()
        {
            var bgr = new Bgr24(3, 2, 1);
            var bgr2 = default(Bgr24);

            bgr.ToBgr24(ref bgr2);

            Assert.Equal(new Bgr24(3, 2, 1), bgr2);
        }

        [Fact]
        public void ToBgra32()
        {
            var bgr = new Bgr24(3, 2, 1);
            var bgra = default(Bgra32);

            bgr.ToBgra32(ref bgra);

            Assert.Equal(new Bgra32(1, 2, 3, 255), bgra);
        }
    }
}