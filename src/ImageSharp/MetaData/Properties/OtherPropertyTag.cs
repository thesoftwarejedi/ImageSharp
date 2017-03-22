

namespace ImageSharp.MetaData.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a comment
    /// </summary>
    internal class OtherPropertyTag : ImagePropertyTag<string>
    {
        static Dictionary<string, OtherPropertyTag> cache = new Dictionary<string, OtherPropertyTag>();
        private readonly string tagName;

        /// <summary>
        /// Initializes a new instance of the <see cref="OtherPropertyTag"/> class.
        /// </summary>
        internal OtherPropertyTag(string name)
            : base($"Other - " + name, true)
        {
            this.tagName = name;
        }

        /// <summary>
        /// Creates a new instance or a property with the value set.
        /// </summary>
        /// <param name="value">The value to store in the property.</param>
        /// <returns>a newly created property.</returns>
        internal static OtherPropertyTag Create(string name)
        {
            lock (cache)
            {
                if (cache.ContainsKey(name))
                {
                    return cache[name];
                }
                else
                {
                    OtherPropertyTag newTag = new OtherPropertyTag(name);
                    cache.Add(name, newTag);
                    return newTag;
                }
            }
        }

        /// <inheritdoc />
        internal override IEnumerable<ExifValue> ConvertToExifValues(string comment)
        {
            return Enumerable.Empty<ExifValue>();
        }

        /// <inheritdoc />
        internal sealed override IEnumerable<string> CreateTypedFromExifProfile(ExifProfile profile)
        {
            return Enumerable.Empty<string>();
        }
    }
}
