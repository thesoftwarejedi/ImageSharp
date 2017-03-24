// <copyright file="ExifProfile.cs" company="James Jackson-South">
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
    internal sealed partial class ExifProfile
    {
        private static readonly ExifTag[] HardCodedTags = new ExifTag[]
        {
            ExifTag.XResolution,
            ExifTag.YResolution,
            ExifTag.Copyright
        };

        private void SetExifValue<T>(ImageMetaData metadata, ExifTag exiftag, ImagePropertyTag<T> tag)
        {
            if (metadata.GetProperties(tag).Any())
            {
                // if value set
                this.SetValue(exiftag, metadata.GetValue(tag));
            }
        }

        private void SetMetaDataValue<T>(ImageMetaData metadata, ExifTag exiftag, ImagePropertyTag<T> tag)
        {
            ExifValue val = this.GetValue(exiftag);
            if (val != null)
            {
                metadata.SetValue(tag, (T)val.Value);
            }
        }

        public void LoadFrom(ImageMetaData metaData)
        {
            this.SetExifValue(metaData, ExifTag.Copyright, ImagePropertyTag.Copyright);
            this.SetExifValue(metaData, ExifTag.ImageDescription, ImagePropertyTag.Description);
            this.SetResolution(ExifTag.XResolution, metaData.HorizontalResolution);
            this.SetResolution(ExifTag.YResolution, metaData.VerticalResolution);

            // load all other values into generic exif specific tags
            foreach (ImageProperty pro in metaData)
            {
                if (pro.Tag is IExifImagePropertyTag)
                {
                    if (Enum.TryParse(pro.Tag.Name, out ExifTag tag))
                    {
                        this.SetValue(tag, pro.Value);
                    }
                }
            }
        }

        public void PopulateTo(ImageMetaData metaData)
        {
            metaData.SetValue(ExifImagePropertyTag.ExifLoaded, true);
            this.SetMetaDataValue(metaData, ExifTag.Copyright, ImagePropertyTag.Copyright);
            this.SetMetaDataValue(metaData, ExifTag.ImageDescription, ImagePropertyTag.Description);

            double? horizontalResolution = this.GetResolution(ExifTag.XResolution);
            if (horizontalResolution.HasValue)
            {
                metaData.HorizontalResolution = horizontalResolution.Value;
            }

            double? verticalResolution = this.GetResolution(ExifTag.XResolution);
            if (verticalResolution.HasValue)
            {
                metaData.VerticalResolution = verticalResolution.Value;
            }

            // load all other values into generic exif specific tags
            foreach (ExifValue val in this.values)
            {
                if (!HardCodedTags.Contains(val.Tag))
                {
                    switch (val.DataType)
                    {
                        case ExifDataType.Unknown:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<object>(val.Tag), val.Value);
                            break;
                        case ExifDataType.Byte:
                            metaData.SetValue(ExifImagePropertyTag<byte>.GetTag(val.Tag), (byte)val.Value);
                            break;
                        case ExifDataType.Ascii:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<string>(val.Tag), val.Value?.ToString());
                            break;
                        case ExifDataType.Short:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<short>(val.Tag), (short)val.Value);
                            break;
                        case ExifDataType.Long:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<ulong>(val.Tag), (ulong)val.Value);
                            break;
                        case ExifDataType.Rational:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<SignedRational>(val.Tag), (SignedRational)val.Value);
                            break;
                        case ExifDataType.SignedByte:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<sbyte>(val.Tag), (sbyte)val.Value);
                            break;
                        case ExifDataType.Undefined:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<object>(val.Tag), (object)val.Value);
                            break;
                        case ExifDataType.SignedShort:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<short>(val.Tag), (short)val.Value);
                            break;
                        case ExifDataType.SignedLong:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<long>(val.Tag), (long)val.Value);
                            break;
                        case ExifDataType.SignedRational:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<SignedRational>(val.Tag), (SignedRational)val.Value);
                            break;
                        case ExifDataType.SingleFloat:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<float>(val.Tag), (float)val.Value);
                            break;
                        case ExifDataType.DoubleFloat:
                            metaData.SetValue(ExifImagePropertyTag.GetTag<double>(val.Tag), (double)val.Value);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}