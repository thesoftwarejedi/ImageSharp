// ReSharper disable InconsistentNaming
namespace ImageSharp.Tests.Common
{
    using System;
    using System.Linq;
    using System.Numerics;

    using ImageSharp.Memory;

    using Xunit;
    using Xunit.Abstractions;

    public class DummyPackedBufferTests
    {
        private ITestOutputHelper output;

        public DummyPackedBufferTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Foo()
        {
            this.output.WriteLine(System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
        }

        [Fact]
        public void ReadAllPartitions_ReadsOriginalBufferInSinglePartition()
        {
            var source = new Buffer<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            using (PackedBuffer<int> buffer = new DummyPackedBuffer<int>(source))
            {
                Assert.Equal(1, buffer.ReadAllPartitions().Count());

                foreach (PackedBuffer<int>.Partition partition in buffer.ReadAllPartitions())
                {
                    Assert.True(source.Span == partition.Span);
                }
            }
        }

        [Fact]
        public void WriteAllPartitions_WritesToOriginalBufferInSinglePartition()
        {
            using (var source = new Buffer<int>(42))
            {
                using (PackedBuffer<int> buffer = new DummyPackedBuffer<int>(source))
                {
                    Assert.Equal(1, buffer.WriteAllPartitions().Count());

                    foreach (PackedBuffer<int>.Partition partition in buffer.WriteAllPartitions())
                    {
                        Span<int> span = partition.Span;
                        Assert.True(source.Span == span);
                        for (int i = 0; i < source.Span.Length; i++)
                        {
                            span[i] = i;
                        }
                    }
                }

                for (int i = 0; i < source.Length; i++)
                {
                    Assert.Equal(i, source[i]);
                }
            }
        }

        [Fact]
        public void NoWrappingBuffer_WriteAndRead()
        {
            using (var buffer = new DummyPackedBuffer<int>(42))
            {
                foreach (PackedBuffer<int>.Partition p in buffer.WriteAllPartitions())
                {
                    Span<int> span = p.Span;
                    for (int i = 0; i < p.Span.Length; i++)
                    {
                        span[i] = i;
                    }
                }

                foreach (PackedBuffer<int>.Partition p in buffer.ReadAllPartitions())
                {
                    Span<int> span = p.Span;
                    for (int i = 0; i < p.Span.Length; i++)
                    {
                        Assert.Equal(i, span[i]);
                    }
                }
            }
        }
    }
}