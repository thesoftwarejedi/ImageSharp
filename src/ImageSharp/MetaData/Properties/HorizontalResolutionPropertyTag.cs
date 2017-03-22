

namespace ImageSharp.MetaData.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ImageSharp.Formats;

    /// <summary>
    /// Represents a horizontal resolution
    /// </summary>
    internal class HorizontalResolutionPropertyTag : ImagePropertyTag<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalResolutionPropertyTag"/> class.
        /// </summary>
        internal HorizontalResolutionPropertyTag()
            : base("Horizontal Resolution", false)
        {
        }

        internal override IEnumerable<double> ReadMetaDataValue(ExifProfile profile)
        {
            ExifValue val = profile.GetValue(ExifTag.XResolution);
            if (val != null && val.Value is Rational)
            {
                Rational rat = (Rational)val.Value;
                yield return rat.ToDouble();
            }
        }

        internal override void SetMetaDataValue(ImageProperty<double> value, ExifProfile profile)
        {
            profile.SetValue(ExifTag.XResolution, new Rational(value.TypedValue, false));
        }

        internal override IEnumerable<double> ReadMetaDataValue(PngMetaData profile)
        {
            yield return profile.HorizontalResolution;
        }

        internal override void SetMetaDataValue(ImageProperty<double> value, PngMetaData profile)
        {
            profile.HorizontalResolution = value.TypedValue;
        }
    }
}
