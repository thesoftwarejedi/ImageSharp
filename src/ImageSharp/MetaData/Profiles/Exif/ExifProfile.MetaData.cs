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
            ExifTag.YResolution
        };

        /// <inheritdoc />
        public void LoadFrom(ImageMetaData metaData)
        {
            this.SetResolution(ExifTag.XResolution, metaData.HorizontalResolution);
            this.SetResolution(ExifTag.YResolution, metaData.VerticalResolution);

            // load all other values into generic exif specific tags
            foreach (ImageProperty pro in metaData)
            {
                if (pro.Tag.Namespace == "exif")
                {
                    if (Enum.TryParse<ExifTag>(pro.Tag.Name, out ExifTag tag))
                    {
                        this.SetValue(tag, pro.Value);
                    }
                }
            }
        }

        /// <inheritdoc />
        public void PopulateTo(ImageMetaData metaData)
        {
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
                            metaData.SetValue(ImagePropertyTag.Other<object>("exif", val.Tag.ToString()), val.Value);
                            break;
                        case ExifDataType.Byte:
                            metaData.SetValue(ImagePropertyTag.Other<byte>("exif", val.Tag.ToString()), (byte)val.Value);
                            break;
                        case ExifDataType.Ascii:
                            metaData.SetValue(ImagePropertyTag.Other<string>("exif", val.Tag.ToString()), val.Value?.ToString());
                            break;
                        case ExifDataType.Short:
                            metaData.SetValue(ImagePropertyTag.Other<short>("exif", val.Tag.ToString()), (short)val.Value);
                            break;
                        case ExifDataType.Long:
                            metaData.SetValue(ImagePropertyTag.Other<ulong>("exif", val.Tag.ToString()), (ulong)val.Value);
                            break;
                        case ExifDataType.Rational:
                            metaData.SetValue(ImagePropertyTag.Other<SignedRational>("exif", val.Tag.ToString()), (SignedRational)val.Value);
                            break;
                        case ExifDataType.SignedByte:
                            metaData.SetValue(ImagePropertyTag.Other<sbyte>("exif", val.Tag.ToString()), (sbyte)val.Value);
                            break;
                        case ExifDataType.Undefined:
                            metaData.SetValue(ImagePropertyTag.Other<object>("exif", val.Tag.ToString()), val.Value);
                            break;
                        case ExifDataType.SignedShort:
                            metaData.SetValue(ImagePropertyTag.Other<short>("exif", val.Tag.ToString()), (short)val.Value);
                            break;
                        case ExifDataType.SignedLong:
                            metaData.SetValue(ImagePropertyTag.Other<long>("exif", val.Tag.ToString()), (long)val.Value);
                            break;
                        case ExifDataType.SignedRational:
                            metaData.SetValue(ImagePropertyTag.Other<SignedRational>("exif", val.Tag.ToString()), (SignedRational)val.Value);
                            break;
                        case ExifDataType.SingleFloat:
                            metaData.SetValue(ImagePropertyTag.Other<float>("exif", val.Tag.ToString()), (float)val.Value);
                            break;
                        case ExifDataType.DoubleFloat:
                            metaData.SetValue(ImagePropertyTag.Other<double>("exif", val.Tag.ToString()), (double)val.Value);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}