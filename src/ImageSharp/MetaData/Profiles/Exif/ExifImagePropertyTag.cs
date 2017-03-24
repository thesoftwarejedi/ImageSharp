// <copyright file="ExifValue.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Marker interface to identify general exif tags
    /// </summary>
    internal interface IExifImagePropertyTag
    {
    }

    internal static class ExifImagePropertyTag
    {
        public static ImagePropertyTag<bool> ExifLoaded = ImagePropertyTag.Other<bool>("Imported Metadata", "Exif");

        /// <summary>
        /// The tag represting an other values.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">The tags name</param>
        /// <returns>The tag</returns>
        public static ImagePropertyTag<T> GetTag<T>(ExifTag tag) => ExifImagePropertyTag<T>.GetTag(tag);
    }

    /// <summary>
    /// Represent the value of the EXIF profile.
    /// </summary>
    internal sealed class ExifImagePropertyTag<T> : ImagePropertyTag<T>, IExifImagePropertyTag
    {
        private static Dictionary<ExifTag, ImagePropertyTag<T>> cache = new Dictionary<ExifTag, ImagePropertyTag<T>>();

        public static ImagePropertyTag<T> GetTag(ExifTag tag)
        {
            if (cache.ContainsKey(tag))
            {
                return cache[tag];
            }

            lock (cache)
            {
                if (cache.ContainsKey(tag))
                {
                    return cache[tag];
                }

                var prop = new ExifImagePropertyTag<T>(tag);
                cache.Add(tag, prop);
                return prop;
            }
        }

        private ExifImagePropertyTag(ExifTag tag)
            : base("Exif", tag.ToString(), false)
        {
        }
    }
}