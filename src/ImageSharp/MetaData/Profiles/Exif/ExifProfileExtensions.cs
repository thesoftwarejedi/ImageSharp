// <copyright file="ExifProfileExtensions.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents an EXIF profile providing access to the collection of values.
    /// </summary>
    internal static class ExifProfileExtensions
    {
        public static ExifProfile ExifProfile(this ImageMetaData metadata)
        {
            ExifProfile profile = new ExifProfile();
            profile.LoadFrom(metadata);
            return profile;
        }

        public static void ReplaceWith(this ImageMetaData metadata, ExifProfile profile)
        {
            metadata.Clear();
            profile.PopulateTo(metadata);
        }
    }
}