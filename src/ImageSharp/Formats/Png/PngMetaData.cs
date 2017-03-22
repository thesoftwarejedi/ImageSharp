
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

        public double Quality { get; set; }
        public double HorizontalResolution { get; internal set; }
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
    }
}
