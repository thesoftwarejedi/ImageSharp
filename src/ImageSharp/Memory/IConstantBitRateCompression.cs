namespace ImageSharp.Memory
{
    using System;

    /// <summary>
    /// Represents a CBR compression algorithm for <see cref="CompressedBuffer{T, TCompression}"/>.
    /// Implementation should be a stateless struct to utilize CLR inling.
    /// </summary>
    /// <remarks>
    /// A sample implementation: <see cref="float"/>-s in [0..255] range rounded and converted to <see cref="byte"/>-s.
    /// </remarks>
    /// <typeparam name="T">The element type to be compressed</typeparam>
    internal interface IConstantBitRateCompression<T>
        where T : struct
    {
        /// <summary>
        /// Gets a value indicating how many bytes will take a single item of type 'T' in bytes.
        /// </summary>
        int CompressedElementSizeInBytes { get; }

        void Compress(Span<T> source, Span<byte> destination);

        void Decompress(Span<byte> source, Span<T> destination);
    }
}