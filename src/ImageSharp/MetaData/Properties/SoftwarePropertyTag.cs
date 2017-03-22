

namespace ImageSharp.MetaData.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a the software
    /// </summary>
    internal class SoftwarePropertyTag : ImagePropertyTag<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftwarePropertyTag"/> class.
        /// </summary>
        internal SoftwarePropertyTag()
            : base("Software", false)
        {
        }

        internal override IEnumerable<string> ReadMetaDataValue(ExifProfile profile)
        {
            ExifValue val = profile.GetValue(ExifTag.Software);
            if (val != null && val.Value != null && val.Value is string)
            {
                yield return val.Value.ToString();
            }
        }

        internal override void SetMetaDataValue(ImageProperty<string> value, ExifProfile profile)
        {
            profile.SetValue(ExifTag.Software, value.TypedValue);
        }
    }
}
