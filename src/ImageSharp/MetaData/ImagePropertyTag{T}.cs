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
    using ImageSharp.Formats;

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

        internal virtual IEnumerable<T> ReadMetaDataValue(ExifProfile profile)
        {
            return Enumerable.Empty<T>();
        }

        internal sealed override IEnumerable<ImageProperty> ReadMetaData(ExifProfile profile)
        {
            return ReadMetaDataValue(profile).Select(Create);
        }

        internal virtual IEnumerable<T> ReadMetaDataValue(PngMetaData profile)
        {
            return Enumerable.Empty<T>();
        }

        internal sealed override IEnumerable<ImageProperty> ReadMetaData(PngMetaData profile)
        {
            return ReadMetaDataValue(profile).Select(Create);
        }

        internal virtual void SetMetaDataValue(ImageProperty<T> value, ExifProfile profile)
        {
        }

        internal sealed override void SetMetaData(ImageProperty property, ExifProfile profile)
        {
            if (property.Tag == this && property is ImageProperty<T>)
            {
                ImageProperty<T> propertyTyped = property as ImageProperty<T>;
                base.SetMetaData(propertyTyped, profile);
            }
        }

        internal virtual void SetMetaDataValue(ImageProperty<T> value, PngMetaData profile)
        {
        }

        internal sealed override void SetMetaData(ImageProperty property, PngMetaData profile)
        {
            if (property.Tag == this && property is ImageProperty<T>)
            {
                ImageProperty<T> propertyTyped = property as ImageProperty<T>;
                base.SetMetaData(propertyTyped, profile);
            }
        }
    }
}
