namespace ImageSharp.Memory
{
    using System;

    internal class DummyPackedBuffer<T> : PackedBuffer<T>
        where T : struct
    {
        private bool isBufferOwner;

        private readonly Buffer<T> buffer;

        public DummyPackedBuffer(Buffer<T> buffer)
            : base(buffer.Length)
        {
            Guard.NotNull(buffer, nameof(buffer));

            this.buffer = buffer;
        }

        public DummyPackedBuffer(int length)
            : this(new Buffer<T>(length))
        {
            this.isBufferOwner = true;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing && this.isBufferOwner)
            {
                this.buffer.Dispose();
            }
        }

        private sealed class Iterator : PartitionIterator
        {
            private Buffer<T> buffer;

            public Iterator(Buffer<T> buffer)
            {
                this.buffer = buffer;
            }

            internal override void ReadCurrent()
            {
                
            }

            internal override void WriteCurrent()
            {
            }

            internal override bool MoveNext()
            {
                if (this.buffer == null)
                {
                    this.CurrentPartition = Partition.Empty;
                    return false;
                }
                else
                {
                    this.CurrentPartition = new Partition(this.buffer, 0, this.buffer.Length);
                }

                this.buffer = null;
                return true;
            }

            public override void Dispose()
            {
            }
        }

        protected override PartitionIterator CreateIterator()
        {
            return new Iterator(this.buffer);
        }
    }
}