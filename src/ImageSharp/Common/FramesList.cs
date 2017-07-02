// <copyright file="InterfaceWrappingList.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using ImageSharp.PixelFormats;

    /// <summary>
    /// Encapulates a list of ImageFrames but handles converion for when using IImageFrames for the incorrect pixel format.
    /// </summary>
    /// <typeparam name="TPixel">The pixel format</typeparam>
    internal class FramesList<TPixel> : IList<IImageFrame>, IList<ImageFrame<TPixel>>
        where TPixel : struct, IPixel<TPixel>
    {
        private readonly IList<IImageFrame> source = new List<IImageFrame>();

        /// <inheritdoc/>
        public int Count => this.source.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => this.source.IsReadOnly;

        /// <inheritdoc/>
        public ImageFrame<TPixel> this[int index] { get => this.source[index].As<TPixel>(); set => this.source[index] = value; }

        /// <inheritdoc/>
        IImageFrame IList<IImageFrame>.this[int index] { get => this.source[index]; set => this.source[index] = value; }

        /// <inheritdoc/>
        public void Add(IImageFrame item)
        {
            this.source.Add(item);
        }

        /// <inheritdoc/>
        public void Add(ImageFrame<TPixel> item)
        {
            this.source.Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.source.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(ImageFrame<TPixel> item)
        {
            return this.source.Contains(item);
        }

        /// <inheritdoc/>
        public bool Contains(IImageFrame item)
        {
            return this.source.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(ImageFrame<TPixel>[] array, int arrayIndex)
        {
            this.source.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public void CopyTo(IImageFrame[] array, int arrayIndex)
        {
            this.source.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<ImageFrame<TPixel>> GetEnumerator()
        {
            return this.GetEnumerable().GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator<IImageFrame> IEnumerable<IImageFrame>.GetEnumerator()
        {
            return this.source.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(IImageFrame item)
        {
            return this.source.IndexOf(item);
        }

        /// <inheritdoc/>
        public int IndexOf(ImageFrame<TPixel> item)
        {
            return this.source.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, ImageFrame<TPixel> item)
        {
            this.source.Insert(index, item);
        }

        /// <inheritdoc/>
        public void Insert(int index, IImageFrame item)
        {
            this.source.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(IImageFrame item)
        {
            return this.source.Remove(item);
        }

        /// <inheritdoc/>
        public bool Remove(ImageFrame<TPixel> item)
        {
            return this.source.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            this.source.RemoveAt(index);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.source.GetEnumerator();
        }

        private IEnumerable<ImageFrame<TPixel>> GetEnumerable()
        {
            foreach (var s in this.source)
            {
                yield return s.As<TPixel>();
            }
        }
    }
}
