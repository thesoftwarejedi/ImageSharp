namespace ImageSharp.Memory
{
    using System;

    /// <summary>
    /// <see cref="PackedBuffer{T}"/> implementation that stores the elements in a CBR compressed form.
    /// A sample compression: <see cref="float"/>-s in [0..255] range rounded and converted to <see cref="byte"/>-s.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <typeparam name="TCompression">The compression provider.</typeparam>
    internal class CompressedBuffer<T, TCompression> : PackedBuffer<T>
        where T : struct
        where TCompression : struct, IConstantBitRateCompression<T>
    {
        private readonly TCompression compression;

        private readonly Buffer<byte> compressedData;

        private readonly int compressedElementSize;

        private int preferredPartitionLength;

        private int partitionCount;

        public CompressedBuffer(int totalLength, int preferredPartitionLength)
            : base(totalLength)
        {
            this.compression = default(TCompression);
            this.compressedElementSize = this.compression.CompressedElementSizeInBytes;
            this.compressedData = new Buffer<byte>(totalLength * this.compressedElementSize);

            this.preferredPartitionLength = preferredPartitionLength;

            if (totalLength == 0)
            {
                return;
            }

            this.partitionCount = totalLength / preferredPartitionLength;

            if (totalLength % preferredPartitionLength > 0)
            {
                this.partitionCount++;
            }
        }

        protected override PartitionIterator CreateIterator()
        {
            return new Iterator(this);
        }

        private class Iterator : PartitionIterator
        {
            private CompressedBuffer<T, TCompression> compressedBuffer;

            private int currentPosition;

            private Buffer<T> partitionBuffer;

            private int compressedElementSize;

            private TCompression compression;

            public Iterator(CompressedBuffer<T, TCompression> compressedBuffer)
            {
                this.compression = compressedBuffer.compression;
                this.compressedBuffer = compressedBuffer;
                this.compressedElementSize = compressedBuffer.compressedElementSize;

                this.partitionBuffer = new Buffer<T>(compressedBuffer.preferredPartitionLength);

                this.currentPosition = -compressedBuffer.preferredPartitionLength;
            }

            public override void Dispose()
            {
                this.partitionBuffer.Dispose();
            }

            internal override bool MoveNext()
            {
                if (this.currentPosition >= this.compressedBuffer.TotalLength)
                {
                    this.CurrentPartition = Partition.Empty;
                    return false;
                }

                this.currentPosition += this.compressedBuffer.preferredPartitionLength;

                if (this.currentPosition >= this.compressedBuffer.TotalLength)
                {
                    this.CurrentPartition = Partition.Empty;
                    return false;
                }

                int partitionLength = this.compressedBuffer.preferredPartitionLength;

                int d = this.compressedBuffer.TotalLength - this.currentPosition;
                if (d < partitionLength)
                {
                    partitionLength = d;
                }

                this.CurrentPartition = new Partition(this.partitionBuffer, 0, partitionLength);

                return true;
            }

            internal override void ReadCurrent()
            {
                Span<byte> compressedSpan = this.GetCompressedSpan();

                this.compression.Decompress(compressedSpan, this.CurrentPartition.Span);
            }

            internal override void WriteCurrent()
            {
                Span<byte> compressedSpan = this.GetCompressedSpan();

                this.compression.Compress(this.CurrentPartition.Span, compressedSpan);
            }

            private Span<byte> GetCompressedSpan()
            {
                int compressedStart = this.currentPosition * this.compressedElementSize;
                int compressedLength = this.CurrentPartition.Span.Length * this.compressedElementSize;

                Span<byte> compressedSpan =
                    this.compressedBuffer.compressedData.Slice(compressedStart, compressedLength);
                return compressedSpan;
            }
        }
    }
}