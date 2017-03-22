

namespace ImageSharp.MetaData.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Text;

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

        /// <inheritdoc />
        internal override IEnumerable<ExifValue> ConvertToExifValues(double resolution)
        {
            Rational val = new Rational(resolution, false);
            yield return ExifValue.Create(ExifTag.XResolution, val);
        }

        /// <inheritdoc />
        internal override IEnumerable<double> CreateTypedFromExifProfile(ExifProfile profile)
        {
            ExifValue val = profile.GetValue(ExifTag.XResolution);
            if (val != null && val.Value is Rational)
            {
                Rational rat = (Rational)val.Value;
                yield return rat.ToDouble();
            }
        }
    }
}
