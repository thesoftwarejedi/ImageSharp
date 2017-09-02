using System.Numerics;

using Microsoft.Xna.Framework.Graphics.PackedVector;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace SixLabors.ImageSharp.Tests.PixelFormats
{
    using Bgr565 = ImageSharp.PixelFormats.Bgr565;
    using MonoBgr565 = Microsoft.Xna.Framework.Graphics.PackedVector.Bgr565;
    using MonoIPackedVector = Microsoft.Xna.Framework.Graphics.PackedVector.IPackedVector<ushort>;


    public class Bgr565Tests
    {
        [Fact]
        public void LimitsAreEnforced()
        {
            // Test the limits.
            Assert.Equal(0x0, new Bgr565(Vector3.Zero).PackedValue);
            Assert.Equal(0xFFFF, new Bgr565(Vector3.One).PackedValue);
            Assert.Equal(0x0, new MonoBgr565(Vector3.Zero).PackedValue);
            Assert.Equal(0xFFFF, new MonoBgr565(Vector3.One).PackedValue);

            // Test clamping.
            Assert.True(CompareHelpers.Equal(Vector3.Zero, new Bgr565(Vector3.One * -1234F).ToVector3()));
            Assert.True(CompareHelpers.Equal(Vector3.One, new Bgr565(Vector3.One * 1234F).ToVector3()));
            Assert.True(CompareHelpers.Equal(Vector3.Zero, new MonoBgr565(Vector3.One * -1234F).ToVector3()));
            Assert.True(CompareHelpers.Equal(Vector3.One, new MonoBgr565(Vector3.One * 1234F).ToVector3()));
        }

        [Fact]
        public void PackedValueIsCorrect()
        {
            // Make sure the swizzle is correct.
            var bgr = new Bgr565(Vector3.UnitX);
            var bgr1 = new Bgr565(Vector3.UnitY);
            var bgr2 = new Bgr565(Vector3.UnitZ);
            var bgr3 = new Bgr565(1, 0, 0);
            var bgr4 = new Bgr565(0, 1, 0);
            var bgr5 = new Bgr565(0, 0, 1);

            Assert.Equal(0xF800, bgr.PackedValue);
            Assert.Equal(0x07E0, bgr1.PackedValue);
            Assert.Equal(0x001F, bgr2.PackedValue);
            Assert.Equal(0xF800, bgr3.PackedValue);
            Assert.Equal(0x07E0, bgr4.PackedValue);
            Assert.Equal(0x001F, bgr5.PackedValue);

            Assert.Equal(new MonoBgr565(Vector3.UnitX).PackedValue, bgr.PackedValue);
            Assert.Equal(new MonoBgr565(Vector3.UnitY).PackedValue, bgr1.PackedValue);
            Assert.Equal(new MonoBgr565(Vector3.UnitZ).PackedValue, bgr2.PackedValue);
            Assert.Equal(new MonoBgr565(1, 0, 0).PackedValue, bgr3.PackedValue);
            Assert.Equal(new MonoBgr565(0, 1, 0).PackedValue, bgr4.PackedValue);
            Assert.Equal(new MonoBgr565(0, 0, 1).PackedValue, bgr5.PackedValue);
        }

        [Fact]
        public void ToVectorOrderingIsCorrect()
        {
            // Test ordering
            var vector = new Bgr565(1, 0, 0).ToVector4();
            var monoVector = ((IPackedVector)new MonoBgr565(1, 0, 0)).ToVector4();

            Assert.Equal(1, vector.X);
            Assert.Equal(0, vector.Y);
            Assert.Equal(0, vector.Z);
            Assert.Equal(1, vector.W);

            Assert.Equal(monoVector.X, vector.X);
            Assert.Equal(monoVector.Y, vector.Y);
            Assert.Equal(monoVector.Z, vector.Z);
            Assert.Equal(monoVector.W, vector.W);
        }

        [Fact]
        public void FromVectorOrderingIsCorrect()
        {
            var vector = new Vector4(0, 1, 0, 1);
            var bgr565 = default(Bgr565);
            var monoBgr565 = (MonoIPackedVector)default(MonoBgr565);

            bgr565.PackFromVector4(vector);
            monoBgr565.PackFromVector4(vector);

            Assert.Equal(0x07E0, bgr565.PackedValue);
            Assert.Equal(monoBgr565.PackedValue, bgr565.PackedValue);
        }

        [Fact]
        public void ByteOrderingIsCorrect()
        {
            float x = 0.1F;
            float y = -0.3F;
            float z = 0.5F;

            // Test ordering
            byte[] rgb = new byte[3];
            byte[] rgba = new byte[4];
            byte[] bgr = new byte[3];
            byte[] bgra = new byte[4];

            new Bgr565(x, y, z).ToXyzBytes(rgb, 0);
            Assert.Equal(rgb, new byte[] { 25, 0, 132 });

            new Bgr565(x, y, z).ToXyzwBytes(rgba, 0);
            Assert.Equal(rgba, new byte[] { 25, 0, 132, 255 });

            new Bgr565(x, y, z).ToZyxBytes(bgr, 0);
            Assert.Equal(bgr, new byte[] { 132, 0, 25 });

            new Bgr565(x, y, z).ToZyxwBytes(bgra, 0);
            Assert.Equal(bgra, new byte[] { 132, 0, 25, 255 });
        }

        [Fact]
        public void ToRgb24IsCorrect()
        {
            var bgr565 = new Bgr565(1, 0, 0);
            var rgb = default(Rgb24);

            bgr565.ToRgb24(ref rgb);

            Assert.Equal(new Rgb24(255, 0, 0), rgb);
        }

        [Fact]
        public void ToRgba32IsCorrect()
        {
            var bgr565 = new Bgr565(1, 0, 0);
            var rgba = default(Rgba32);

            bgr565.ToRgba32(ref rgba);

            Assert.Equal(new Rgba32(255, 0, 0, 255), rgba);
        }

        [Fact]
        public void ToBgr24IsCorrect()
        {
            var bgr565 = new Bgr565(1, 0, 0);
            var bgr = default(Bgr24);

            bgr565.ToBgr24(ref bgr);
            var expected = new Bgr24(255, 0, 0);
            Assert.Equal(expected, bgr);
        }

        [Fact]
        public void ToBgra32IsCorrect()
        {
            var bgr565 = new Bgr565(1, 0, 0);
            var bgra = default(Bgra32);

            bgr565.ToBgra32(ref bgra);

            Assert.Equal(new Bgra32(255, 0, 0, 255), bgra);
        }
    }
}