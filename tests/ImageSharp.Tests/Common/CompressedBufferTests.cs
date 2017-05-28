namespace ImageSharp.Tests.Common
{
    using System;

    using ImageSharp.Memory;

    using Xunit;

    public class CompressedBufferTests
    {
        struct DummyCompression : IConstantBitRateCompression<float>
        {
            public int CompressedElementSizeInBytes => 4;

            public void Compress(Span<float> source, Span<byte> destination)
            {
                source.AsBytes().CopyTo(destination);
            }

            public void Decompress(Span<byte> source, Span<float> destination)
            {
                source.CopyTo(destination.AsBytes());
            }
        }

        struct AsUInt16Compression : IConstantBitRateCompression<float>
        {
            public int CompressedElementSizeInBytes => sizeof(ushort);

            public void Compress(Span<float> source, Span<byte> destination)
            {
                var d = destination.NonPortableCast<byte, ushort>();

                for (int i = 0; i < source.Length; i++)
                {
                    d[i] = (ushort)source[i];
                }
            }

            public void Decompress(Span<byte> source, Span<float> destination)
            {
                var s = source.NonPortableCast<byte, ushort>();

                for (int i = 0; i < destination.Length; i++)
                {
                    destination[i] = s[i];
                }
            }
        }

        public static TheoryData<bool, int, int> TestData = new TheoryData<bool, int, int>
                                                                {
                                                                    { false, 0, 0 },
                                                                    { false, 1, 1 },
                                                                    { false, 42, 42 },
                                                                    { false, 1024, 1024 },
                                                                    { false, 1024, 1023 },
                                                                    { false, 1024, 128 },
                                                                    { false, 1023, 128 },
                                                                    { false, 1025, 128 },

                                                                    { true, 0, 0 },
                                                                    { true, 1, 1 },
                                                                    { true, 42, 42 },
                                                                    { true, 1024, 1024 },
                                                                    { true, 1024, 1023 },
                                                                    { true, 1024, 128 },
                                                                    { true, 1023, 128 },
                                                                    { true, 1025, 128 },
                                                                };

        [Theory]
        [MemberData(nameof(TestData))]
        public void WriteRead(bool dummyCompression, int length, int partitionLength)
        {
            using (PackedBuffer<float> buffer = dummyCompression
                                                    ? (PackedBuffer<float>)new CompressedBuffer<float, DummyCompression>(
                                                        length,
                                                        partitionLength)
                                                    : new CompressedBuffer<float, AsUInt16Compression>(
                                                        length,
                                                        partitionLength))
            {
                int value = 1;

                foreach (PackedBuffer<float>.Partition p in buffer.WriteAllPartitions())
                {
                    Span<float> span = p.Span;

                    for (int i = 0; i < span.Length; i++)
                    {
                        span[i] = value;
                        value++;
                    }
                }

                value = 0;

                foreach (PackedBuffer<float>.Partition p in buffer.ReadAllPartitions())
                {
                    Span<float> span = p.Span;
                    for (int i = 0; i < span.Length; i++)
                    {
                        value += (int)span[i];
                    }
                }

                int expected = length * (length + 1) / 2;

                Assert.Equal(expected, value);
            }
        }
    }
}