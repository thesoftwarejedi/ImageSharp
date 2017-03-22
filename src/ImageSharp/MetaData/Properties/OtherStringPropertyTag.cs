

namespace ImageSharp.MetaData.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a comment
    /// </summary>
    internal class OtherStringPropertyTag : ImagePropertyTag<string>
    {
        static Dictionary<string, OtherStringPropertyTag> cache = new Dictionary<string, OtherStringPropertyTag>();
        private readonly string tagName;

        /// <summary>
        /// Initializes a new instance of the <see cref="OtherStringPropertyTag"/> class.
        /// </summary>
        private OtherStringPropertyTag(string name)
            : base($"Other - " + name, true)
        {
            this.tagName = name;
        }

        /// <summary>
        /// Creates a new instance or a property with the value set.
        /// </summary>
        /// <param name="value">The value to store in the property.</param>
        /// <returns>a newly created property.</returns>
        internal static OtherStringPropertyTag Get(string name)
        {
            lock (cache)
            {
                if (cache.ContainsKey(name))
                {
                    return cache[name];
                }
                else
                {
                    OtherStringPropertyTag newTag = new OtherStringPropertyTag(name);
                    cache.Add(name, newTag);
                    return newTag;
                }
            }
        }
    }
}
