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
    public abstract partial class ImagePropertyTag
    {
        /// <summary>
        /// Gets the tag represting horizontal resolutions
        /// </summary>
        public static ImagePropertyTag<double> HorizontalResolution { get; } = ImagePropertyTag<double>.Get("Horizontal Resoluton");

        /// <summary>
        /// Gets the tag represting FrameDelay
        /// </summary>
        public static ImagePropertyTag<int> FrameDelay { get; } = ImagePropertyTag<int>.Get("Animation", "Frame Delay");

        /// <summary>
        /// Gets the tag represting Repeat Count
        /// </summary>
        public static ImagePropertyTag<ushort> RepeatCount { get; } = ImagePropertyTag<ushort>.Get("Animation", "Repeat Count");

        /// <summary>
        /// Gets the tag represting FrameDelay
        /// </summary>
        public static ImagePropertyTag<int> Quality { get; } = ImagePropertyTag<int>.Get("Quality");

        /// <summary>
        /// Gets the tag represting vertical resolutions
        /// </summary>
        public static ImagePropertyTag<double> VerticalResolution { get; } = ImagePropertyTag<double>.Get("Vertical Resoluton");

        /// <summary>
        /// Gets the tag represting a user comment
        /// </summary>
        public static ImagePropertyTag<string> UserComment { get; } = ImagePropertyTag<string>.Get("User Comment");

        /// <summary>
        /// Gets the tag represting a software that generated the image.
        /// </summary>
        public static ImagePropertyTag<string> Software { get; } = ImagePropertyTag<string>.Get("Software");

        /// <summary>
        /// The tag represting an other string value.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="name">The tags name</param>
        /// <returns>The tag</returns>
        public static ImagePropertyTag<T> Other<T>(string name) => ImagePropertyTag<T>.Get(name);

        /// <summary>
        /// The tag represting an other string value.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="tagNamespace">The tags namespace</param>
        /// <param name="name">The tags name</param>
        /// <returns>The tag</returns>
        public static ImagePropertyTag<T> Other<T>(string tagNamespace, string name) => ImagePropertyTag<T>.Get(tagNamespace, name);
    }
}
