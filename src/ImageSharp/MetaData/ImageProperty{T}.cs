// <copyright file="ImageProperty.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;

    /// <summary>
    /// Stores meta information about a image, like the name of the author,
    /// the copyright information, the date, where the image was created
    /// or some other information.
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    public class ImageProperty<T> : ImageProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProperty{T}"/> class.
        /// </summary>
        /// <param name="tag">The tag of the property.</param>
        /// <param name="value">The value of the property.</param>
        internal ImageProperty(ImagePropertyTag<T> tag, T value)
            : base(tag, value)
        {
        }

        /// <summary>
        /// Gets the value of this <see cref="ImageProperty{T}"/>.
        /// </summary>
        public T TypedValue => (T)this.Value;
    }
}
