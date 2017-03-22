

namespace ImageSharp.MetaData.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ImageSharp.Formats;

    /// <summary>
    /// represetns a vertical resolution
    /// </summary>
    internal class VerticalResolutionPropertyTag : ImagePropertyTag<double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalResolutionPropertyTag"/> class.
        /// </summary>
        internal VerticalResolutionPropertyTag()
            : base("Vertical Resolution", false)
        {
        }

        internal override IEnumerable<double> ReadMetaDataValue(ExifProfile profile)
        {
            ExifValue val = profile.GetValue(ExifTag.YResolution);
            if (val != null && val.Value is Rational)
            {
                Rational rat = (Rational)val.Value;
                yield return rat.ToDouble();
            }
        }

        internal override void SetMetaDataValue(ImageProperty<double> value, ExifProfile profile)
        {
            profile.SetValue(ExifTag.YResolution, new Rational(value.TypedValue, false));
        }

        internal override IEnumerable<double> ReadMetaDataValue(PngMetaData profile)
        {
            yield return profile.VerticalResolution;
        }

        internal override void SetMetaDataValue(ImageProperty<double> value, PngMetaData profile)
        {
            profile.VerticalResolution = value.TypedValue;
        }
    }
}
