// <copyright file="ImageProperty.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Stores meta information about a image, like the name of the author,
    /// the copyright information, the date, where the image was created
    /// or some other information.
    /// </summary>
    public abstract class ImagePropertyTag
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertyTag{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        internal ImagePropertyTag(string name)
        {
            Guard.NotNullOrEmpty(name, nameof(name));

            this.Name = name;
        }

        /// <summary>
        /// Gets the name of this <see cref="ImagePropertyTag{T}"/> indicating which kind of
        /// information this property stores.
        /// </summary>
        /// <example>
        /// Typical properties are the author, copyright
        /// information or other meta information.
        /// </example>
        public string Name { get; }

        /// <summary>
        /// Convertes an image property into an ExifValue.
        /// </summary>
        /// <param name="property">the property to read the value from.</param>
        /// <returns>A collection of Exif values derived from this ImageProperty.</returns>
        internal virtual IEnumerable<ExifValue> ConvertToExifValues(ImageProperty property)
        {
            if (property.Value != null)
            {
                yield return new ExifValue(ExifTag.Unknown, ExifDataType.Ascii, property.Value?.ToString(), false);
            }
        }
    }

    /// <summary>
    /// Stores meta information about a image, like the name of the author,
    /// the copyright information, the date, where the image was created
    /// or some other information.
    /// </summary>
    /// <typeparam name="T">Type of the value that can be set against this tag.</typeparam>
    public class ImagePropertyTag<T> : ImagePropertyTag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertyTag{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        internal ImagePropertyTag(string name)
            : this(name, null)
        {
        }
    }
}
