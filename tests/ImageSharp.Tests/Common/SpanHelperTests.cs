// ReSharper disable ObjectCreationAsStatement
// ReSharper disable InconsistentNaming

namespace ImageSharp.Tests.Common
{
    using System;
    using System.Numerics;
    using System.Runtime.CompilerServices;

    using ImageSharp.Memory;
    using ImageSharp.PixelFormats;

    using Xunit;

    using static TestStructs;

    public unsafe class SpanHelperTests
    {
        // ReSharper disable once ClassNeverInstantiated.Local
        private class Assert : Xunit.Assert
        {
            public static void SameRefs<T1, T2>(ref T1 a, ref T2 b)
            {
                ref T1 bb = ref Unsafe.As<T2, T1>(ref b);

                True(Unsafe.AreSame(ref a, ref bb), "References are not same!");
            }
        }

        [Fact]
        public void FetchVector()
        {
            float[] stuff = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            Span<float> span = new Span<float>(stuff);

            ref Vector<float> v = ref span.FetchVector();

            Assert.Equal(0, v[0]);
            Assert.Equal(1, v[1]);
            Assert.Equal(2, v[2]);
            Assert.Equal(3, v[3]);
        }
        
        public class Copy
        {
            private static void AssertNotDefault<T>(T[] data, int idx)
                where T : struct
            {
                Assert.NotEqual(default(T), data[idx]);
            }

            private static byte[] CreateTestBytes(int count)
            {
                byte[] result = new byte[count];
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = (byte)((i % 200) + 1);
                }
                return result;
            }

            private static int[] CreateTestInts(int count)
            {
                int[] result = new int[count];
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = i + 1;
                }
                return result;
            }

            [Theory]
            [InlineData(4)]
            [InlineData(1500)]
            public void GenericToOwnType(int count)
            {
                Foo[] source = Foo.CreateArray(count + 2);
                Foo[] dest = new Foo[count + 5];

                Span<Foo> apSource = new Span<Foo>(source, 1);
                Span<Foo> apDest = new Span<Foo>(dest, 1);

                SpanHelper.Copy(apSource, apDest, count - 1);

                AssertNotDefault(source, 1);
                AssertNotDefault(dest, 1);

                Assert.NotEqual(source[0], dest[0]);
                Assert.Equal(source[1], dest[1]);
                Assert.Equal(source[2], dest[2]);
                Assert.Equal(source[count - 1], dest[count - 1]);
                Assert.NotEqual(source[count], dest[count]);
            }

            [Theory]
            [InlineData(4)]
            [InlineData(1500)]
            public void GenericToOwnType_Aligned(int count)
            {
                AlignedFoo[] source = AlignedFoo.CreateArray(count + 2);
                AlignedFoo[] dest = new AlignedFoo[count + 5];

                Span<AlignedFoo> apSource = new Span<AlignedFoo>(source, 1);
                Span<AlignedFoo> apDest = new Span<AlignedFoo>(dest, 1);

                SpanHelper.Copy(apSource, apDest, count - 1);

                AssertNotDefault(source, 1);
                AssertNotDefault(dest, 1);

                Assert.NotEqual(source[0], dest[0]);
                Assert.Equal(source[1], dest[1]);
                Assert.Equal(source[2], dest[2]);
                Assert.Equal(source[count - 1], dest[count - 1]);
                Assert.NotEqual(source[count], dest[count]);
            }

            [Theory]
            [InlineData(4)]
            [InlineData(1500)]
            public void IntToInt(int count)
            {
                int[] source = CreateTestInts(count + 2);
                int[] dest = new int[count + 5];

                Span<int> apSource = new Span<int>(source, 1);
                Span<int> apDest = new Span<int>(dest, 1);

                SpanHelper.Copy(apSource, apDest, count - 1);

                AssertNotDefault(source, 1);
                AssertNotDefault(dest, 1);

                Assert.NotEqual(source[0], dest[0]);
                Assert.Equal(source[1], dest[1]);
                Assert.Equal(source[2], dest[2]);
                Assert.Equal(source[count - 1], dest[count - 1]);
                Assert.NotEqual(source[count], dest[count]);
            }

            [Theory]
            [InlineData(4)]
            [InlineData(1500)]
            public void GenericToBytes(int count)
            {
                int destCount = count * sizeof(Foo);
                Foo[] source = Foo.CreateArray(count + 2);
                byte[] dest = new byte[destCount + sizeof(Foo) * 2];

                Span<Foo> apSource = new Span<Foo>(source, 1);
                Span<byte> apDest = new Span<byte>(dest, sizeof(Foo));

                SpanHelper.Copy(apSource.AsBytes(), apDest, (count - 1) * sizeof(Foo));

                AssertNotDefault(source, 1);

                Assert.False(ElementsAreEqual(source, dest, 0));
                Assert.True(ElementsAreEqual(source, dest, 1));
                Assert.True(ElementsAreEqual(source, dest, 2));
                Assert.True(ElementsAreEqual(source, dest, count - 1));
                Assert.False(ElementsAreEqual(source, dest, count));
            }

            [Theory]
            [InlineData(4)]
            [InlineData(1500)]
            public void GenericToBytes_Aligned(int count)
            {
                int destCount = count * sizeof(Foo);
                AlignedFoo[] source = AlignedFoo.CreateArray(count + 2);
                byte[] dest = new byte[destCount + sizeof(AlignedFoo) * 2];

                Span<AlignedFoo> apSource = new Span<AlignedFoo>(source, 1);
                Span<byte> apDest = new Span<byte>(dest, sizeof(AlignedFoo));

                SpanHelper.Copy(apSource.AsBytes(), apDest, (count - 1) * sizeof(AlignedFoo));

                AssertNotDefault(source, 1);

                Assert.False(ElementsAreEqual(source, dest, 0));
                Assert.True(ElementsAreEqual(source, dest, 1));
                Assert.True(ElementsAreEqual(source, dest, 2));
                Assert.True(ElementsAreEqual(source, dest, count - 1));
                Assert.False(ElementsAreEqual(source, dest, count));
            }

            [Theory]
            [InlineData(4)]
            [InlineData(1500)]
            public void IntToBytes(int count)
            {
                int destCount = count * sizeof(int);
                int[] source = CreateTestInts(count + 2);
                byte[] dest = new byte[destCount + sizeof(int) + 1];

                Span<int> apSource = new Span<int>(source);
                Span<byte> apDest = new Span<byte>(dest);

                SpanHelper.Copy(apSource.AsBytes(), apDest, count * sizeof(int));

                AssertNotDefault(source, 1);

                Assert.True(ElementsAreEqual(source, dest, 0));
                Assert.True(ElementsAreEqual(source, dest, count - 1));
                Assert.False(ElementsAreEqual(source, dest, count));
            }

            [Theory]
            [InlineData(4)]
            [InlineData(1500)]
            public void BytesToGeneric(int count)
            {
                int srcCount = count * sizeof(Foo);
                byte[] source = CreateTestBytes(srcCount);
                Foo[] dest = new Foo[count + 2];

                Span<byte> apSource = new Span<byte>(source);
                Span<Foo> apDest = new Span<Foo>(dest);

                SpanHelper.Copy(apSource, apDest.AsBytes(), count * sizeof(Foo));

                AssertNotDefault(source, sizeof(Foo) + 1);
                AssertNotDefault(dest, 1);

                Assert.True(ElementsAreEqual(dest, source, 0));
                Assert.True(ElementsAreEqual(dest, source, 1));
                Assert.True(ElementsAreEqual(dest, source, count - 1));
                Assert.False(ElementsAreEqual(dest, source, count));
            }

            [Fact]
            public void Color32ToBytes()
            {
                Rgba32[] colors = { new Rgba32(0, 1, 2, 3), new Rgba32(4, 5, 6, 7), new Rgba32(8, 9, 10, 11), };

                using (Buffer<Rgba32> colorBuf = new Buffer<Rgba32>(colors))
                using (Buffer<byte> byteBuf = new Buffer<byte>(colors.Length * 4))
                {
                    SpanHelper.Copy(colorBuf.Span.AsBytes(), byteBuf, colorBuf.Length * sizeof(Rgba32));

                    byte[] a = byteBuf.Array;

                    for (int i = 0; i < byteBuf.Length; i++)
                    {
                        Assert.Equal((byte)i, a[i]);
                    }
                }
            }

            internal static bool ElementsAreEqual(Foo[] array, byte[] rawArray, int index)
            {
                fixed (Foo* pArray = array)
                fixed (byte* pRaw = rawArray)
                {
                    Foo* pCasted = (Foo*)pRaw;

                    Foo val1 = pArray[index];
                    Foo val2 = pCasted[index];

                    return val1.Equals(val2);
                }
            }

            internal static bool ElementsAreEqual(AlignedFoo[] array, byte[] rawArray, int index)
            {
                fixed (AlignedFoo* pArray = array)
                fixed (byte* pRaw = rawArray)
                {
                    AlignedFoo* pCasted = (AlignedFoo*)pRaw;

                    AlignedFoo val1 = pArray[index];
                    AlignedFoo val2 = pCasted[index];

                    return val1.Equals(val2);
                }
            }

            internal static bool ElementsAreEqual(int[] array, byte[] rawArray, int index)
            {
                fixed (int* pArray = array)
                fixed (byte* pRaw = rawArray)
                {
                    int* pCasted = (int*)pRaw;

                    int val1 = pArray[index];
                    int val2 = pCasted[index];

                    return val1.Equals(val2);
                }
            }
        }
    }
}