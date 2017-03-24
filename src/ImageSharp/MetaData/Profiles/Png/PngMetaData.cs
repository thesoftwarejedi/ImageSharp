namespace ImageSharp.Formats
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Png meta data collection
    /// </summary>
    internal class PngMetaData
    {
        private List<KeyValuePair<string, string>> properties = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Gets or sets the quality.
        /// </summary>
        public double Quality { get; set; }

        /// <summary>
        /// Gets or sets the Horizontal Resolution
        /// </summary>
        public double HorizontalResolution { get; internal set; }

        /// <summary>
        /// Gets or sets the Vertical Resolution
        /// </summary>
        public double VerticalResolution { get; internal set; }

        /// <summary>
        /// sets the value
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        public void SetValue(string key, string value)
        {
            this.properties.Add(new KeyValuePair<string, string>(key, value));
        }

        /// <summary>
        /// Gets the last set value
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the value or null</returns>
        public string GetValue(string key)
        {
            return this.properties.LastOrDefault(x => x.Key == key).Value;
        }

        private static string[] HardCodedProperties = new[] {
            "Comment",
            "Software"
        };
        
        public void LoadFrom(ImageMetaData metadata)
        {
            foreach (var p in metadata)
            {
                if (p.Tag.NameSpace == "png")
                {
                    this.SetValue(p.Tag.Name, p.Value?.ToString());
                }
            }
        }

        public void PopulateTo(ImageMetaData metadata)
        {
            metadata.SetValue(ImagePropertyTag.UserComment, GetValue("Comment"));
            metadata.SetValue(ImagePropertyTag.Software, GetValue("Software"));

            var lastProperties = this.properties.GroupBy(X => X.Key).Select(x => x.Last()).Where(x => !HardCodedProperties.Contains(x.Key));
            foreach(var p in lastProperties)
            {
                metadata.SetValue(ImagePropertyTag.Other<string>("png", p.Key), p.Value);
            }
        }
    }
}
