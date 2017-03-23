// <copyright file="IMetaData.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    /// <summary>
    /// Encapsulates the metadata of an image frame.
    /// </summary>
    internal interface IMetaDataProvider
    {
        void LoadFrom(ImageMetaData metadata);

        void Populate(ImageMetaData metadata);
    }
}
