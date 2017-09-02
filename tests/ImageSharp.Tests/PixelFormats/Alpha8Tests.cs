using System.Numerics;

using Microsoft.Xna.Framework.Graphics.PackedVector;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace SixLabors.ImageSharp.Tests.PixelFormats
{
    using Alpha8 = ImageSharp.PixelFormats.Alpha8;
    using MonoAlpha8 = Microsoft.Xna.Framework.Graphics.PackedVector.Alpha8;
    using MonoIPackedVector = Microsoft.Xna.Framework.Graphics.PackedVector.IPackedVector<byte>;

    public class Alpha8Tests
    {
        [Fact]
        public void LimitsAreEnforced()
        {
            // Test the limits.
            Assert.Equal(0, new Alpha8(0F).PackedValue);
            Assert.Equal(255, new Alpha8(1F).PackedValue);

            Assert.Equal(0, new MonoAlpha8(0F).PackedValue);
            Assert.Equal(255, new MonoAlpha8(1F).PackedValue);

            // Test clamping.
            Assert.Equal(0, new Alpha8(-1234F).PackedValue);
            Assert.Equal(255, new Alpha8(1234F).PackedValue);

            Assert.Equal(0, new MonoAlpha8(-1234F).PackedValue);
            Assert.Equal(255, new MonoAlpha8(1234F).PackedValue);
        }

        public void PackedValueIsCorrect()
        {
            // Test ordering
            Assert.Equal(124, new Alpha8(124F / 0xFF).PackedValue);
            Assert.Equal(26, new Alpha8(0.1F).PackedValue);

            Assert.Equal(124, new MonoAlpha8(124F / 0xFF).PackedValue);
            Assert.Equal(26, new MonoAlpha8(0.1F).PackedValue);
        }

        [Fact]
        public void ToVectorOrderingIsCorrect()
        {
            // Test ordering
            var vector = new Alpha8(.5F).ToVector4();
            var monoVector = ((IPackedVector)new MonoAlpha8(.5F)).ToVector4();

            Assert.Equal(0, vector.X);
            Assert.Equal(0, vector.Y);
            Assert.Equal(0, vector.Z);
            Assert.Equal(.5F, vector.W, 2);

            Assert.Equal(monoVector.X, vector.X);
            Assert.Equal(monoVector.Y, vector.Y);
            Assert.Equal(monoVector.Z, vector.Z);
            Assert.Equal(monoVector.W, vector.W);
        }

        [Fact]
        public void FromVectorOrderingIsCorrect()
        {
            var vector = new Vector4(0, 0, 0, .5F);
            var alpha8 = default(Alpha8);
            var monoAlpha8 = (MonoIPackedVector)default(MonoAlpha8);

            alpha8.PackFromVector4(vector);
            monoAlpha8.PackFromVector4(vector);

            Assert.Equal(128, alpha8.PackedValue);
            Assert.Equal(monoAlpha8.PackedValue, alpha8.PackedValue);
        }

        [Fact]
        public void ByteOrderingIsCorrect()
        {
            byte[] rgb = new byte[3];
            byte[] rgba = new byte[4];
            byte[] bgr = new byte[3];
            byte[] bgra = new byte[4];

            new Alpha8(.5F).ToXyzBytes(rgb, 0);
            Assert.Equal(rgb, new byte[] { 0, 0, 0 });

            new Alpha8(.5F).ToXyzwBytes(rgba, 0);
            Assert.Equal(rgba, new byte[] { 0, 0, 0, 128 });

            new Alpha8(.5F).ToZyxBytes(bgr, 0);
            Assert.Equal(bgr, new byte[] { 0, 0, 0 });

            new Alpha8(.5F).ToZyxwBytes(bgra, 0);
            Assert.Equal(bgra, new byte[] { 0, 0, 0, 128 });
        }

        [Fact]
        public void ToRgba32IsCorrect()
        {
            var alpha8 = new Alpha8(1);
            var rgba = default(Rgba32);

            alpha8.ToRgba32(ref rgba);

            Assert.Equal(new Rgba32(0, 0, 0, 255), rgba);
        }

        [Fact]
        public void ToBgr24IsCorrect()
        {
            var alpha8 = new Alpha8(1);
            var bgr = default(Bgr24);

            alpha8.ToBgr24(ref bgr);

            Assert.Equal(new Bgr24(0, 0, 0), bgr);
        }

        [Fact]
        public void ToBgra32IsCorrect()
        {
            var alpha8 = new Alpha8(1);
            var bgra = default(Bgra32);

            alpha8.ToBgra32(ref bgra);

            Assert.Equal(new Bgra32(0, 0, 0, 255), bgra);
        }
    }
}