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
    public class ImagePropertyTag<T> : ImagePropertyTag
    {
        private static Dictionary<Tuple<string, string>, ImagePropertyTag<T>> cache = new Dictionary<Tuple<string, string>, ImagePropertyTag<T>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertyTag{T}"/> class.
        /// </summary>
        /// <param name="tagNamespace">a namespace to group the tags.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="allowMultiple">does this tage allow multiple instances.</param>
        private ImagePropertyTag(string tagNamespace, string name, bool allowMultiple)
            : base(tagNamespace, name, allowMultiple)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertyTag{T}"/> class.
        /// </summary>
        /// <param name="tagNamespace">a namespace to group the tags.</param>
        /// <param name="name">The name of the property.</param>
        private ImagePropertyTag(string tagNamespace, string name)
            : base(tagNamespace, name, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertyTag{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="allowMultiple">does this tage allow multiple instances.</param>
        private ImagePropertyTag(string name, bool allowMultiple)
            : base(null, name, allowMultiple)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePropertyTag{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        private ImagePropertyTag(string name)
            : base(null, name, false)
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
        /// Creates a new instance or a property with the value set.
        /// </summary>
        /// <param name="tagNamespace">The tag namespace.</param>
        /// <param name="name">The tag name.</param>
        /// <returns>a newly created property.</returns>
        internal static ImagePropertyTag<T> Get(string tagNamespace, string name)
        {
            lock (cache)
            {
                Tuple<string, string> key = Tuple.Create(tagNamespace, name);
                if (cache.ContainsKey(key))
                {
                    return cache[key];
                }
                else
                {
                    ImagePropertyTag<T> newTag = new ImagePropertyTag<T>(name);
                    cache.Add(key, newTag);
                    return newTag;
                }
            }
        }

        /// <summary>
        /// Creates a new instance or a property with the value set.
        /// </summary>
        /// <param name="name">The tag name.</param>
        /// <returns>a newly created property.</returns>
        internal static ImagePropertyTag<T> Get(string name)
        {
            return Get(null, name);
        }
    }
}
