// <copyright file="ImageProperty.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Stores meta information about a image, like the name of the author,
    /// the copyright information, the date, where the image was created
    /// or some other information.
    /// </summary>
    /// <typeparam name="T">Type of the value that can be set against this tag.</typeparam>
    public abstract class ImagePropertyTag<T> : ImagePropertyTag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertyTag{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="allowMultiple">does this tage allow multiple instances.</param>
        internal ImagePropertyTag(string name, bool allowMultiple)
            : base(name, allowMultiple)
        {
        }

        /// <summary>
        /// Creates a new instance or a property with the value set.
        /// </summary>
        /// <param name="value">The value to store in the property.</param>
        /// <returns>a newly created property.</returns>
        public ImageProperty Create(T value)
        {
            return new ImageProperty<T>(this, value);
        }

        /// <summary>
        /// Convertes an image property value into an ExifValue.
        /// </summary>
        /// <param name="value">the property value to read.</param>
        /// <returns>A collection of Exif values derived from this ImageProperty.</returns>
        internal abstract IEnumerable<ExifValue> ConvertToExifValues(T value);

        /// <inheritdoc />
        internal sealed override IEnumerable<ExifValue> ConvertToExifValues(ImageProperty property)
        {
            if (property is ImageProperty<T> && property.Tag.GetType() == this.GetType())
            {
                return this.ConvertToExifValues((ImageProperty<T>)property);
            }

            return Enumerable.Empty<ExifValue>();
        }

        /// <summary>
        /// Convertes an ExifProfile to a collection of values.
        /// </summary>
        /// <param name="profile">the profile to read the value from.</param>
        /// <returns>A collection of values derived from this ExifProfile.</returns>
        internal abstract IEnumerable<T> CreateTypedFromExifProfile(ExifProfile profile);

        /// <inheritdoc />
        internal override IEnumerable<ImageProperty> CreateFromExifProfile(ExifProfile profile)
        {
            return this.CreateTypedFromExifProfile(profile).Select(this.Create).ToArray();
        }
    }
}
