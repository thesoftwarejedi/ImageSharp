// <copyright file="ImageProperty.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Stores meta information about a image, like the name of the author,
    /// the copyright information, the date, where the image was created
    /// or some other information.
    /// </summary>
    public class ImageProperty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProperty"/> class.
        /// </summary>
        /// <param name="tag">The tag of the property.</param>
        /// <param name="value">The value of the property.</param>
        internal ImageProperty(ImagePropertyTag tag, object value)
        {
            this.Tag = tag;
            this.Value = value;
        }

        /// <summary>
        /// Gets the tag of this <see cref="ImageProperty{T}"/> indicating which kind of
        /// information this property stores.
        /// </summary>
        /// <example>
        /// Typical properties are the author, copyright
        /// information or other meta information.
        /// </example>
        public ImagePropertyTag Tag { get; }

        /// <summary>
        /// Gets the value of this <see cref="ImageProperty{T}"/>.
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Generates exif values for a property value.
        /// </summary>
        /// <returns>a collection of exif values this property represents</returns>
        internal IEnumerable<ExifValue> ConvertToExifValues()
        {
            return this.Tag.ConvertToExifValues(this);
        }
    }
}