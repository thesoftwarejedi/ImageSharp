

namespace ImageSharp.MetaData.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents a comment
    /// </summary>
    internal class UserCommentPropertyTag : ImagePropertyTag<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCommentPropertyTag"/> class.
        /// </summary>
        internal UserCommentPropertyTag()
            : base("User Comments", false)
        {
        }

        /// <inheritdoc />
        internal override IEnumerable<ExifValue> ConvertToExifValues(string comment)
        {
            yield return ExifValue.Create(ExifTag.UserComment, comment);
        }

        /// <inheritdoc />
        internal override IEnumerable<string> CreateTypedFromExifProfile(ExifProfile profile)
        {
            ExifValue val = profile.GetValue(ExifTag.UserComment);
            if (val != null && val.Value != null && val.Value is string)
            {
                yield return val.Value.ToString();
            }
        }
    }
}
