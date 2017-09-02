namespace SixLabors.ImageSharp.Tests.PixelFormats
{
    using System;
    using System.Numerics;

    public static class CompareHelpers
    {
        // Comparison helpers with small tolerance to allow for floating point rounding during computations.
        public static bool Equal(float a, float b)
        {
            return Math.Abs(a - b) < 1e-5;
        }

        public static bool Equal(Vector2 a, Vector2 b)
        {
            return Equal(a.X, b.X) && Equal(a.Y, b.Y);
        }

        public static bool Equal(Vector3 a, Vector3 b)
        {
            return Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z);
        }

        public static bool Equal(Vector4 a, Vector4 b)
        {
            return Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z) && Equal(a.W, b.W);
        }
    }
}
