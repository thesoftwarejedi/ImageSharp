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
    public abstract partial class ImagePropertyTag
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertyTag"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="allowMultiple">is this tag allowed to have mutliple copies.</param>
        internal ImagePropertyTag(string name, bool allowMultiple)
        {
            Guard.NotNullOrEmpty(name, nameof(name));

            this.Name = name;
            this.AllowMultiple = allowMultiple;
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="ImageMetaData"/> is allowed to retain multiple versions of this tag.
        /// </summary>
        public bool AllowMultiple { get; }

        /// <summary>
        /// Gets the name of this <see cref="ImagePropertyTag"/> indicating which kind of
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
            return Enumerable.Empty<ExifValue>();
        }

        /// <summary>
        /// Convertes an ExifProfile to a collection of properties.
        /// </summary>
        /// <param name="profile">the profile to read the value from.</param>
        /// <returns>A collection of Exif values derived from this ImageProperty.</returns>
        internal virtual IEnumerable<ImageProperty> CreateFromExifProfile(ExifProfile profile)
        {
            return Enumerable.Empty<ImageProperty>();
        }
    }
}
