

namespace ImageSharp.MetaData.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Text;

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

        /// <inheritdoc />
        internal override IEnumerable<ExifValue> ConvertToExifValues(double resolution)
        {
            Rational val = new Rational(resolution, false);
            yield return ExifValue.Create(ExifTag.YResolution, val);
        }

        /// <inheritdoc />
        internal override IEnumerable<double> CreateTypedFromExifProfile(ExifProfile profile)
        {
            ExifValue val = profile.GetValue(ExifTag.YResolution);
            if(val != null && val.Value is Rational)
            {
                Rational rat = (Rational)val.Value;
                yield return rat.ToDouble();
            }
        }
    }
}
