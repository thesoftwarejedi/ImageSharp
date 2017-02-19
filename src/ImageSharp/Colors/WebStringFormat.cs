// <copyright file="WebStringFormat.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    /// <summary>
    /// Various formats supported by webstrings.
    /// </summary>
    public enum WebStringFormat
    {
        /// <summary>
        /// The color's name
        /// </summary>
        Name,

        /// <summary>
        /// The color as a hex string in the form  #RRGGBB
        /// </summary>
        HexFull,

        /// <summary>
        /// The color represented in the rgb function format. rgb(r,g,b).
        /// </summary>
        RgbFunctionBytes,

        /// <summary>
        /// The color represented in the rgba function format. rgb(r,g,b,a).
        /// </summary>
        RgbaFunctionBytes,

        /// <summary>
        /// The color in the reduced hex formation #RGB.
        /// </summary>
        Hex,

        /// <summary>
        /// The color represented in the rgb function format. rgb(r,g,b) where rgb are representd as percentages.
        /// </summary>
        RgbFunctionPercentages,

        /// <summary>
        /// The color represented in the rgba function format. rgb(r,g,b,a) where rgba are representd as percentages.
        /// </summary>
        RgbaFunctionPercentages,
    }
}
