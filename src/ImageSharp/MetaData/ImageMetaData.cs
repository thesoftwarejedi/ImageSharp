// <copyright file="ImageMetaData.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using ImageSharp.Formats;

    /// <summary>
    /// Encapsulates the metadata of an image.
    /// </summary>
    public sealed partial class ImageMetaData : IMetaData, IEnumerable<ImageProperty>
    {
        /// <summary>
        /// The default horizontal resolution value (dots per inch) in x direction.
        /// <remarks>The default value is 96 dots per inch.</remarks>
        /// </summary>
        public const double DefaultHorizontalResolution = 96;

        /// <summary>
        /// The default vertical resolution value (dots per inch) in y direction.
        /// <remarks>The default value is 96 dots per inch.</remarks>
        /// </summary>
        public const double DefaultVerticalResolution = 96;

        /// <summary>
        /// Gets the list of properties for storing meta information about this image.
        /// </summary>
        /// <value>A list of image properties.</value>
        private IList<ImageProperty> properties = new List<ImageProperty>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMetaData"/> class.
        /// </summary>
        internal ImageMetaData()
        {
            this.HorizontalResolution = DefaultHorizontalResolution;
            this.VerticalResolution = DefaultVerticalResolution;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageMetaData"/> class
        /// by making a copy from other metadata.
        /// </summary>
        /// <param name="other">
        /// The other <see cref="ImageMetaData"/> to create this instance from.
        /// </param>
        internal ImageMetaData(ImageMetaData other)
        {
            DebugGuard.NotNull(other, nameof(other));

            this.LoadFrom(other);
        }

        /// <summary>
        /// Gets or sets the resolution of the image in x- direction. It is defined as
        ///  number of dots per inch and should be an positive value.
        /// </summary>
        /// <value>The density of the image in x- direction.</value>
        public double HorizontalResolution
        {
            get
            {
                return this.GetValue(ImagePropertyTag.HorizontalResolution, DefaultHorizontalResolution);
            }

            set
            {
                if (value > 0)
                {
                    this.SetValue(ImagePropertyTag.HorizontalResolution, value);
                }
                else
                {
                    this.SetValue(ImagePropertyTag.HorizontalResolution, DefaultHorizontalResolution);
                }
            }
        }

        /// <summary>
        /// Gets or sets the resolution of the image in y- direction. It is defined as
        /// number of dots per inch and should be an positive value.
        /// </summary>
        /// <value>The density of the image in y- direction.</value>
        public double VerticalResolution
        {
            get
            {
                return this.GetValue(ImagePropertyTag.VerticalResolution, DefaultHorizontalResolution);
            }

            set
            {
                if (value > 0)
                {
                    this.SetValue(ImagePropertyTag.VerticalResolution, value);
                }
                else
                {
                    this.SetValue(ImagePropertyTag.VerticalResolution, DefaultHorizontalResolution);
                }
            }
        }

        /// <summary>
        /// Gets or sets the frame delay for animated images.
        /// If not 0, this field specifies the number of hundredths (1/100) of a second to
        /// wait before continuing with the processing of the Data Stream.
        /// The clock starts ticking immediately after the graphic is rendered.
        /// </summary>
        public int FrameDelay
        {
            get => this.GetValue(ImagePropertyTag.FrameDelay, 0);
            set => this.SetValue(ImagePropertyTag.FrameDelay, value);
        }

        /// <summary>
        /// Gets or sets the quality of the image. This affects the output quality of lossy image formats.
        /// </summary>
        public int Quality
        {
            get => this.GetValue(ImagePropertyTag.Quality, 0);
            set => this.SetValue(ImagePropertyTag.Quality, value);
        }

        /// <summary>
        /// Gets or sets the number of times any animation is repeated.
        /// <remarks>0 means to repeat indefinitely.</remarks>
        /// </summary>
        public ushort RepeatCount
        {
            get => this.GetValue(ImagePropertyTag.RepeatCount, (ushort)0);
            set => this.SetValue(ImagePropertyTag.RepeatCount, value);
        }

        /// <summary>
        /// Gets the property that matches a tag.
        /// </summary>
        /// <param name="tag">the tag to retrive the matchng property for.</param>
        /// <returns>The matching property or null</returns>
        public ImageProperty this[ImagePropertyTag tag]
        {
            get
            {
                lock (this.properties)
                {
                    // always returns the last of any type
                    return this.properties.LastOrDefault(x => x.Tag == tag);
                }
            }
        }

        /// <summary>
        /// Set the value for a named property tag
        /// </summary>
        /// <typeparam name="T">the type ove vlaue</typeparam>
        /// <param name="tag">the tag</param>
        /// <param name="value">the value</param>
        public void SetValue<T>(ImagePropertyTag<T> tag, T value)
        {
            this.SetProperty(tag.Create(value));
        }

        /// <summary>
        /// Sets the property in the metadata
        /// </summary>
        /// <param name="property">The property to set.</param>
        public void SetProperty(ImageProperty property)
        {
            lock (this.properties)
            {
                ImageProperty[] old = this.properties.Where(x => x.Tag == property.Tag).ToArray();
                foreach (ImageProperty p in old)
                {
                    this.properties.Remove(p);
                }
            }

            this.properties.Add(property);
        }

        /// <summary>
        /// Sets the property in the metadata
        /// </summary>
        /// <param name="tag">The property tag to retrieve.</param>
        /// <returns>Teh property value if set otherwise null.</returns>
        public object GetValue(ImagePropertyTag tag)
        {
            ImageProperty property = this[tag];
            if (property != null)
            {
                return property.Value;
            }

            return null;
        }

        /// <summary>
        /// Sets the property in the metadata
        /// </summary>
        /// <param name="tag">The property tag to retrieve.</param>
        /// <returns>Teh property value if set otherwise null.</returns>
        public IEnumerable<ImageProperty> GetProperties(ImagePropertyTag tag)
        {
            lock (this.properties)
            {
                // always returns the last of any type
                return this.properties.Where(x => x.Tag == tag).ToArray();
            }
        }

        /// <summary>
        /// Sets the property in the metadata
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="tag">The property tag to retrieve.</param>
        /// <returns>Teh property value if set otherwise null.</returns>
        public T GetValue<T>(ImagePropertyTag<T> tag)
        {
            return this.GetValue(tag, default(T));
        }

        /// <summary>
        /// Sets the property in the metadata
        /// </summary>
        /// <typeparam name="T">The value type</typeparam>
        /// <param name="tag">The property tag to retrieve.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Teh property value if set otherwise null.</returns>
        public T GetValue<T>(ImagePropertyTag<T> tag, T defaultValue)
        {
            lock (this.properties)
            {
                // always returns the last of any type
                ImageProperty<T> prop = this.properties.OfType<ImageProperty<T>>().LastOrDefault(x => x.Tag == tag);

                if (prop == null)
                {
                    return defaultValue;
                }

                return prop.TypedValue;
            }
        }

        /// <summary>
        /// Enumerates the properties
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator<ImageProperty> IEnumerable<ImageProperty>.GetEnumerator()
        {
            return this.properties.GetEnumerator();
        }

        /// <summary>
        /// Enumerates the properties
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.properties.GetEnumerator();
        }

        /// <summary>
        /// Sets the current metadata values based on a previous metadata object.
        /// </summary>
        /// <param name="other">Meta data object to copy values from.</param>
        internal void LoadFrom(ImageMetaData other)
        {
            if (this != other)
            {
                foreach (ImageProperty p in other)
                {
                    this.SetProperty(p);
                }
            }
        }
    }
}
