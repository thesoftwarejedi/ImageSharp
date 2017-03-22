// <copyright file="ImageProperty.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using ImageSharp.MetaData.Properties;

    /// <summary>
    /// Stores meta information about a image, like the name of the author,
    /// the copyright information, the date, where the image was created
    /// or some other information.
    /// </summary>
    public abstract partial class ImagePropertyTag
    {
        /// <summary>
        /// The tag represting horizontal resolutions
        /// </summary>
        public static ImagePropertyTag<double> HorizontalResolution { get; } = new HorizontalResolutionPropertyTag();

        /// <summary>
        /// The tag represting vertical resolutions
        /// </summary>
        public static ImagePropertyTag<double> VerticalResolution { get; } = new VerticalResolutionPropertyTag();

        /// <summary>
        /// The tag represting a user comment
        /// </summary>
        public static ImagePropertyTag<string> UserComment { get; } = new UserCommentPropertyTag();

        /// <summary>
        /// create a new generic named meatadata item.
        /// </summary>
        /// <param name="name">the name</param>
        /// <returns>I string baded tag</returns>
        public static ImagePropertyTag<string> Other(string name) => OtherPropertyTag.Create(name);

        internal static readonly IEnumerable<ImagePropertyTag> AllTags = new ImagePropertyTag[]
        {
            HorizontalResolution,
            VerticalResolution,
            UserComment,
            OtherPropertyTag.Create("other") // we need a general one registerd to reversing
        };
    }
}
