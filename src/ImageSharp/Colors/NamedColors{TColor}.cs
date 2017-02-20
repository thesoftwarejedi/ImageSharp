// <copyright file="NamedColors{TColor}.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// A set of named colors mapped to the provided Color space.
    /// </summary>
    /// <typeparam name="TColor">The type of the color.</typeparam>
    public static class NamedColors<TColor>
        where TColor : struct, IPackedPixel, IEquatable<TColor>
    {
        // The private member has to be first so its instanciated first.
#pragma warning disable SA1202
        // note this use be new'ed up before the Calls to RegisterNamedColor.
        private static readonly ConcurrentDictionary<TColor, string> NameLookup = new ConcurrentDictionary<TColor, string>();
        private static readonly ConcurrentDictionary<string, TColor> ColorLookup = new ConcurrentDictionary<string, TColor>(StringComparer.OrdinalIgnoreCase);
        private static readonly List<TColor> WebSafeColorLookup = new List<TColor>();

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F0F8FF.
        /// </summary>
        public static readonly TColor AliceBlue = RegisterNamedColor(240, 248, 255, 255, "AliceBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FAEBD7.
        /// </summary>
        public static readonly TColor AntiqueWhite = RegisterNamedColor(250, 235, 215, 255, "AntiqueWhite", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #00FFFF.
        /// </summary>
        public static readonly TColor Aqua = RegisterNamedColor(0, 255, 255, 255, "Aqua", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #7FFFD4.
        /// </summary>
        public static readonly TColor Aquamarine = RegisterNamedColor(127, 255, 212, 255, "Aquamarine", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F0FFFF.
        /// </summary>
        public static readonly TColor Azure = RegisterNamedColor(240, 255, 255, 255, "Azure", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F5F5DC.
        /// </summary>
        public static readonly TColor Beige = RegisterNamedColor(245, 245, 220, 255, "Beige", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFE4C4.
        /// </summary>
        public static readonly TColor Bisque = RegisterNamedColor(255, 228, 196, 255, "Bisque", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #000000.
        /// </summary>
        public static readonly TColor Black = RegisterNamedColor(0, 0, 0, 255, "Black", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFEBCD.
        /// </summary>
        public static readonly TColor BlanchedAlmond = RegisterNamedColor(255, 235, 205, 255, "BlanchedAlmond", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #0000FF.
        /// </summary>
        public static readonly TColor Blue = RegisterNamedColor(0, 0, 255, 255, "Blue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #8A2BE2.
        /// </summary>
        public static readonly TColor BlueViolet = RegisterNamedColor(138, 43, 226, 255, "BlueViolet", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #A52A2A.
        /// </summary>
        public static readonly TColor Brown = RegisterNamedColor(165, 42, 42, 255, "Brown", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #DEB887.
        /// </summary>
        public static readonly TColor BurlyWood = RegisterNamedColor(222, 184, 135, 255, "BurlyWood", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #5F9EA0.
        /// </summary>
        public static readonly TColor CadetBlue = RegisterNamedColor(95, 158, 160, 255, "CadetBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #7FFF00.
        /// </summary>
        public static readonly TColor Chartreuse = RegisterNamedColor(127, 255, 0, 255, "Chartreuse", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #D2691E.
        /// </summary>
        public static readonly TColor Chocolate = RegisterNamedColor(210, 105, 30, 255, "Chocolate", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FF7F50.
        /// </summary>
        public static readonly TColor Coral = RegisterNamedColor(255, 127, 80, 255, "Coral", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #6495ED.
        /// </summary>
        public static readonly TColor CornflowerBlue = RegisterNamedColor(100, 149, 237, 255, "CornflowerBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFF8DC.
        /// </summary>
        public static readonly TColor Cornsilk = RegisterNamedColor(255, 248, 220, 255, "Cornsilk", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #DC143C.
        /// </summary>
        public static readonly TColor Crimson = RegisterNamedColor(220, 20, 60, 255, "Crimson", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #00FFFF.
        /// </summary>
        public static readonly TColor Cyan = RegisterNamedColor(0, 255, 255, 255, "Cyan", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #00008B.
        /// </summary>
        public static readonly TColor DarkBlue = RegisterNamedColor(0, 0, 139, 255, "DarkBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #008B8B.
        /// </summary>
        public static readonly TColor DarkCyan = RegisterNamedColor(0, 139, 139, 255, "DarkCyan", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #B8860B.
        /// </summary>
        public static readonly TColor DarkGoldenrod = RegisterNamedColor(184, 134, 11, 255, "DarkGoldenrod", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #A9A9A9.
        /// </summary>
        public static readonly TColor DarkGray = RegisterNamedColor(169, 169, 169, 255, "DarkGray", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #006400.
        /// </summary>
        public static readonly TColor DarkGreen = RegisterNamedColor(0, 100, 0, 255, "DarkGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #BDB76B.
        /// </summary>
        public static readonly TColor DarkKhaki = RegisterNamedColor(189, 183, 107, 255, "DarkKhaki", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #8B008B.
        /// </summary>
        public static readonly TColor DarkMagenta = RegisterNamedColor(139, 0, 139, 255, "DarkMagenta", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #556B2F.
        /// </summary>
        public static readonly TColor DarkOliveGreen = RegisterNamedColor(85, 107, 47, 255, "DarkOliveGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FF8C00.
        /// </summary>
        public static readonly TColor DarkOrange = RegisterNamedColor(255, 140, 0, 255, "DarkOrange", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #9932CC.
        /// </summary>
        public static readonly TColor DarkOrchid = RegisterNamedColor(153, 50, 204, 255, "DarkOrchid", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #8B0000.
        /// </summary>
        public static readonly TColor DarkRed = RegisterNamedColor(139, 0, 0, 255, "DarkRed", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #E9967A.
        /// </summary>
        public static readonly TColor DarkSalmon = RegisterNamedColor(233, 150, 122, 255, "DarkSalmon", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #8FBC8B.
        /// </summary>
        public static readonly TColor DarkSeaGreen = RegisterNamedColor(143, 188, 139, 255, "DarkSeaGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #483D8B.
        /// </summary>
        public static readonly TColor DarkSlateBlue = RegisterNamedColor(72, 61, 139, 255, "DarkSlateBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #2F4F4F.
        /// </summary>
        public static readonly TColor DarkSlateGray = RegisterNamedColor(47, 79, 79, 255, "DarkSlateGray", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #00CED1.
        /// </summary>
        public static readonly TColor DarkTurquoise = RegisterNamedColor(0, 206, 209, 255, "DarkTurquoise", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #9400D3.
        /// </summary>
        public static readonly TColor DarkViolet = RegisterNamedColor(148, 0, 211, 255, "DarkViolet", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FF1493.
        /// </summary>
        public static readonly TColor DeepPink = RegisterNamedColor(255, 20, 147, 255, "DeepPink", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #00BFFF.
        /// </summary>
        public static readonly TColor DeepSkyBlue = RegisterNamedColor(0, 191, 255, 255, "DeepSkyBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #696969.
        /// </summary>
        public static readonly TColor DimGray = RegisterNamedColor(105, 105, 105, 255, "DimGray", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #1E90FF.
        /// </summary>
        public static readonly TColor DodgerBlue = RegisterNamedColor(30, 144, 255, 255, "DodgerBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #B22222.
        /// </summary>
        public static readonly TColor Firebrick = RegisterNamedColor(178, 34, 34, 255, "Firebrick", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFFAF0.
        /// </summary>
        public static readonly TColor FloralWhite = RegisterNamedColor(255, 250, 240, 255, "FloralWhite", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #228B22.
        /// </summary>
        public static readonly TColor ForestGreen = RegisterNamedColor(34, 139, 34, 255, "ForestGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FF00FF.
        /// </summary>
        public static readonly TColor Fuchsia = RegisterNamedColor(255, 0, 255, 255, "Fuchsia", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #DCDCDC.
        /// </summary>
        public static readonly TColor Gainsboro = RegisterNamedColor(220, 220, 220, 255, "Gainsboro", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F8F8FF.
        /// </summary>
        public static readonly TColor GhostWhite = RegisterNamedColor(248, 248, 255, 255, "GhostWhite", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFD700.
        /// </summary>
        public static readonly TColor Gold = RegisterNamedColor(255, 215, 0, 255, "Gold", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #DAA520.
        /// </summary>
        public static readonly TColor Goldenrod = RegisterNamedColor(218, 165, 32, 255, "Goldenrod", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #808080.
        /// </summary>
        public static readonly TColor Gray = RegisterNamedColor(128, 128, 128, 255, "Gray", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #008000.
        /// </summary>
        public static readonly TColor Green = RegisterNamedColor(0, 128, 0, 255, "Green", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #ADFF2F.
        /// </summary>
        public static readonly TColor GreenYellow = RegisterNamedColor(173, 255, 47, 255, "GreenYellow", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F0FFF0.
        /// </summary>
        public static readonly TColor Honeydew = RegisterNamedColor(240, 255, 240, 255, "Honeydew", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FF69B4.
        /// </summary>
        public static readonly TColor HotPink = RegisterNamedColor(255, 105, 180, 255, "HotPink", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #CD5C5C.
        /// </summary>
        public static readonly TColor IndianRed = RegisterNamedColor(205, 92, 92, 255, "IndianRed", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #4B0082.
        /// </summary>
        public static readonly TColor Indigo = RegisterNamedColor(75, 0, 130, 255, "Indigo", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFFFF0.
        /// </summary>
        public static readonly TColor Ivory = RegisterNamedColor(255, 255, 240, 255, "Ivory", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F0E68C.
        /// </summary>
        public static readonly TColor Khaki = RegisterNamedColor(240, 230, 140, 255, "Khaki", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #E6E6FA.
        /// </summary>
        public static readonly TColor Lavender = RegisterNamedColor(230, 230, 250, 255, "Lavender", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFF0F5.
        /// </summary>
        public static readonly TColor LavenderBlush = RegisterNamedColor(255, 240, 245, 255, "LavenderBlush", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #7CFC00.
        /// </summary>
        public static readonly TColor LawnGreen = RegisterNamedColor(124, 252, 0, 255, "LawnGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFFACD.
        /// </summary>
        public static readonly TColor LemonChiffon = RegisterNamedColor(255, 250, 205, 255, "LemonChiffon", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #ADD8E6.
        /// </summary>
        public static readonly TColor LightBlue = RegisterNamedColor(173, 216, 230, 255, "LightBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F08080.
        /// </summary>
        public static readonly TColor LightCoral = RegisterNamedColor(240, 128, 128, 255, "LightCoral", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #E0FFFF.
        /// </summary>
        public static readonly TColor LightCyan = RegisterNamedColor(224, 255, 255, 255, "LightCyan", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FAFAD2.
        /// </summary>
        public static readonly TColor LightGoldenrodYellow = RegisterNamedColor(250, 250, 210, 255, "LightGoldenrodYellow", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #D3D3D3.
        /// </summary>
        public static readonly TColor LightGray = RegisterNamedColor(211, 211, 211, 255, "LightGray", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #90EE90.
        /// </summary>
        public static readonly TColor LightGreen = RegisterNamedColor(144, 238, 144, 255, "LightGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFB6C1.
        /// </summary>
        public static readonly TColor LightPink = RegisterNamedColor(255, 182, 193, 255, "LightPink", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFA07A.
        /// </summary>
        public static readonly TColor LightSalmon = RegisterNamedColor(255, 160, 122, 255, "LightSalmon", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #20B2AA.
        /// </summary>
        public static readonly TColor LightSeaGreen = RegisterNamedColor(32, 178, 170, 255, "LightSeaGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #87CEFA.
        /// </summary>
        public static readonly TColor LightSkyBlue = RegisterNamedColor(135, 206, 250, 255, "LightSkyBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #778899.
        /// </summary>
        public static readonly TColor LightSlateGray = RegisterNamedColor(119, 136, 153, 255, "LightSlateGray", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #B0C4DE.
        /// </summary>
        public static readonly TColor LightSteelBlue = RegisterNamedColor(176, 196, 222, 255, "LightSteelBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFFFE0.
        /// </summary>
        public static readonly TColor LightYellow = RegisterNamedColor(255, 255, 224, 255, "LightYellow", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #00FF00.
        /// </summary>
        public static readonly TColor Lime = RegisterNamedColor(0, 255, 0, 255, "Lime", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #32CD32.
        /// </summary>
        public static readonly TColor LimeGreen = RegisterNamedColor(50, 205, 50, 255, "LimeGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FAF0E6.
        /// </summary>
        public static readonly TColor Linen = RegisterNamedColor(250, 240, 230, 255, "Linen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FF00FF.
        /// </summary>
        public static readonly TColor Magenta = RegisterNamedColor(255, 0, 255, 255, "Magenta", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #800000.
        /// </summary>
        public static readonly TColor Maroon = RegisterNamedColor(128, 0, 0, 255, "Maroon", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #66CDAA.
        /// </summary>
        public static readonly TColor MediumAquamarine = RegisterNamedColor(102, 205, 170, 255, "MediumAquamarine", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #0000CD.
        /// </summary>
        public static readonly TColor MediumBlue = RegisterNamedColor(0, 0, 205, 255, "MediumBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #BA55D3.
        /// </summary>
        public static readonly TColor MediumOrchid = RegisterNamedColor(186, 85, 211, 255, "MediumOrchid", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #9370DB.
        /// </summary>
        public static readonly TColor MediumPurple = RegisterNamedColor(147, 112, 219, 255, "MediumPurple", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #3CB371.
        /// </summary>
        public static readonly TColor MediumSeaGreen = RegisterNamedColor(60, 179, 113, 255, "MediumSeaGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #7B68EE.
        /// </summary>
        public static readonly TColor MediumSlateBlue = RegisterNamedColor(123, 104, 238, 255, "MediumSlateBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #00FA9A.
        /// </summary>
        public static readonly TColor MediumSpringGreen = RegisterNamedColor(0, 250, 154, 255, "MediumSpringGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #48D1CC.
        /// </summary>
        public static readonly TColor MediumTurquoise = RegisterNamedColor(72, 209, 204, 255, "MediumTurquoise", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #C71585.
        /// </summary>
        public static readonly TColor MediumVioletRed = RegisterNamedColor(199, 21, 133, 255, "MediumVioletRed", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #191970.
        /// </summary>
        public static readonly TColor MidnightBlue = RegisterNamedColor(25, 25, 112, 255, "MidnightBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F5FFFA.
        /// </summary>
        public static readonly TColor MintCream = RegisterNamedColor(245, 255, 250, 255, "MintCream", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFE4E1.
        /// </summary>
        public static readonly TColor MistyRose = RegisterNamedColor(255, 228, 225, 255, "MistyRose", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFE4B5.
        /// </summary>
        public static readonly TColor Moccasin = RegisterNamedColor(255, 228, 181, 255, "Moccasin", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFDEAD.
        /// </summary>
        public static readonly TColor NavajoWhite = RegisterNamedColor(255, 222, 173, 255, "NavajoWhite", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #000080.
        /// </summary>
        public static readonly TColor Navy = RegisterNamedColor(0, 0, 128, 255, "Navy", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FDF5E6.
        /// </summary>
        public static readonly TColor OldLace = RegisterNamedColor(253, 245, 230, 255, "OldLace", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #808000.
        /// </summary>
        public static readonly TColor Olive = RegisterNamedColor(128, 128, 0, 255, "Olive", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #6B8E23.
        /// </summary>
        public static readonly TColor OliveDrab = RegisterNamedColor(107, 142, 35, 255, "OliveDrab", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFA500.
        /// </summary>
        public static readonly TColor Orange = RegisterNamedColor(255, 165, 0, 255, "Orange", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FF4500.
        /// </summary>
        public static readonly TColor OrangeRed = RegisterNamedColor(255, 69, 0, 255, "OrangeRed", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #DA70D6.
        /// </summary>
        public static readonly TColor Orchid = RegisterNamedColor(218, 112, 214, 255, "Orchid", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #EEE8AA.
        /// </summary>
        public static readonly TColor PaleGoldenrod = RegisterNamedColor(238, 232, 170, 255, "PaleGoldenrod", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #98FB98.
        /// </summary>
        public static readonly TColor PaleGreen = RegisterNamedColor(152, 251, 152, 255, "PaleGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #AFEEEE.
        /// </summary>
        public static readonly TColor PaleTurquoise = RegisterNamedColor(175, 238, 238, 255, "PaleTurquoise", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #DB7093.
        /// </summary>
        public static readonly TColor PaleVioletRed = RegisterNamedColor(219, 112, 147, 255, "PaleVioletRed", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFEFD5.
        /// </summary>
        public static readonly TColor PapayaWhip = RegisterNamedColor(255, 239, 213, 255, "PapayaWhip", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFDAB9.
        /// </summary>
        public static readonly TColor PeachPuff = RegisterNamedColor(255, 218, 185, 255, "PeachPuff", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #CD853F.
        /// </summary>
        public static readonly TColor Peru = RegisterNamedColor(205, 133, 63, 255, "Peru", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFC0CB.
        /// </summary>
        public static readonly TColor Pink = RegisterNamedColor(255, 192, 203, 255, "Pink", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #DDA0DD.
        /// </summary>
        public static readonly TColor Plum = RegisterNamedColor(221, 160, 221, 255, "Plum", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #B0E0E6.
        /// </summary>
        public static readonly TColor PowderBlue = RegisterNamedColor(176, 224, 230, 255, "PowderBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #800080.
        /// </summary>
        public static readonly TColor Purple = RegisterNamedColor(128, 0, 128, 255, "Purple", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #663399.
        /// </summary>
        public static readonly TColor RebeccaPurple = RegisterNamedColor(102, 51, 153, 255, "RebeccaPurple", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FF0000.
        /// </summary>
        public static readonly TColor Red = RegisterNamedColor(255, 0, 0, 255, "Red", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #BC8F8F.
        /// </summary>
        public static readonly TColor RosyBrown = RegisterNamedColor(188, 143, 143, 255, "RosyBrown", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #4169E1.
        /// </summary>
        public static readonly TColor RoyalBlue = RegisterNamedColor(65, 105, 225, 255, "RoyalBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #8B4513.
        /// </summary>
        public static readonly TColor SaddleBrown = RegisterNamedColor(139, 69, 19, 255, "SaddleBrown", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FA8072.
        /// </summary>
        public static readonly TColor Salmon = RegisterNamedColor(250, 128, 114, 255, "Salmon", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F4A460.
        /// </summary>
        public static readonly TColor SandyBrown = RegisterNamedColor(244, 164, 96, 255, "SandyBrown", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #2E8B57.
        /// </summary>
        public static readonly TColor SeaGreen = RegisterNamedColor(46, 139, 87, 255, "SeaGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFF5EE.
        /// </summary>
        public static readonly TColor SeaShell = RegisterNamedColor(255, 245, 238, 255, "SeaShell", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #A0522D.
        /// </summary>
        public static readonly TColor Sienna = RegisterNamedColor(160, 82, 45, 255, "Sienna", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #C0C0C0.
        /// </summary>
        public static readonly TColor Silver = RegisterNamedColor(192, 192, 192, 255, "Silver", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #87CEEB.
        /// </summary>
        public static readonly TColor SkyBlue = RegisterNamedColor(135, 206, 235, 255, "SkyBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #6A5ACD.
        /// </summary>
        public static readonly TColor SlateBlue = RegisterNamedColor(106, 90, 205, 255, "SlateBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #708090.
        /// </summary>
        public static readonly TColor SlateGray = RegisterNamedColor(112, 128, 144, 255, "SlateGray", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFFAFA.
        /// </summary>
        public static readonly TColor Snow = RegisterNamedColor(255, 250, 250, 255, "Snow", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #00FF7F.
        /// </summary>
        public static readonly TColor SpringGreen = RegisterNamedColor(0, 255, 127, 255, "SpringGreen", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #4682B4.
        /// </summary>
        public static readonly TColor SteelBlue = RegisterNamedColor(70, 130, 180, 255, "SteelBlue", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #D2B48C.
        /// </summary>
        public static readonly TColor Tan = RegisterNamedColor(210, 180, 140, 255, "Tan", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #008080.
        /// </summary>
        public static readonly TColor Teal = RegisterNamedColor(0, 128, 128, 255, "Teal", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #D8BFD8.
        /// </summary>
        public static readonly TColor Thistle = RegisterNamedColor(216, 191, 216, 255, "Thistle", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FF6347.
        /// </summary>
        public static readonly TColor Tomato = RegisterNamedColor(255, 99, 71, 255, "Tomato", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFFFFF.
        /// </summary>
        public static readonly TColor Transparent = RegisterNamedColor(255, 255, 255, 0, "Transparent", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #40E0D0.
        /// </summary>
        public static readonly TColor Turquoise = RegisterNamedColor(64, 224, 208, 255, "Turquoise", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #EE82EE.
        /// </summary>
        public static readonly TColor Violet = RegisterNamedColor(238, 130, 238, 255, "Violet", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F5DEB3.
        /// </summary>
        public static readonly TColor Wheat = RegisterNamedColor(245, 222, 179, 255, "Wheat", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFFFFF.
        /// </summary>
        public static readonly TColor White = RegisterNamedColor(255, 255, 255, 255, "White", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #F5F5F5.
        /// </summary>
        public static readonly TColor WhiteSmoke = RegisterNamedColor(245, 245, 245, 255, "WhiteSmoke", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #FFFF00.
        /// </summary>
        public static readonly TColor Yellow = RegisterNamedColor(255, 255, 0, 255, "Yellow", true);

        /// <summary>
        /// Represents a <see cref="Color"/> matching the W3C definition that has an hex value of #9ACD32.
        /// </summary>
        public static readonly TColor YellowGreen = RegisterNamedColor(154, 205, 50, 255, "YellowGreen", true);

        public static IReadOnlyCollection<TColor> WebSafeColors => WebSafeColorLookup;

        /// <summary>
        /// Gets a collection of named, web safe, colors as defined in the CSS Color Module Level 4.
        /// </summary>
        public static IEnumerable<TColor> Colors => ColorLookup.Values;
#pragma warning restore CA1202

        /// <summary>
        /// Finds the wellknown name that this color represents.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="color">The color.</param>
        /// <returns>
        /// The true if the color is known; otherwise [false].
        /// </returns>
        public static bool TryFindColor(string name, out TColor color)
        {
            if (ColorLookup.ContainsKey(name))
            {
                color = ColorLookup[name];
                return true;
            }

            color = default(TColor);
            return false;
        }

        /// <summary>
        /// Finds the wellknown name that this color represents.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The name of the color if known; otherwise [null].</returns>
        public static string FindName(TColor color)
        {
            if (NameLookup.ContainsKey(color))
            {
                return NameLookup[color];
            }

            return null;
        }

        /// <summary>
        /// Finds the websafe wellknown name that this color represents.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>The name of the color if known; otherwise [null].</returns>
        public static string FindWebSafeName(TColor color)
        {
            if (ColorConstants<TColor>.IsWebSafe(color))
            {
                if (NameLookup.ContainsKey(color))
                {
                    return NameLookup[color];
                }
            }

            return null;
        }

        /// <summary>
        /// Determines whether the specified color has a wellknown name.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>
        ///   <c>true</c> if the specified color is named; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNamed(TColor color)
        {
            if (NameLookup.ContainsKey(color))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Registers a new named color.
        /// </summary>
        /// <param name="r">The red component represented as a byte.</param>
        /// <param name="g">The green component represented as a byte.</param>
        /// <param name="b">The blue component represented as a byte.</param>
        /// <param name="a">The alpha component represented as a byte</param>
        /// <param name="name">The name to associated with this color.</param>
        /// <param name="websafe">if set to <c>true</c> [websafe].</param>
        /// <returns>
        /// The color defined by the RGBA components.
        /// </returns>
        private static TColor RegisterNamedColor(byte r, byte g, byte b, byte a, string name, bool websafe)
        {
            TColor color = ColorConverter<TColor>.FromRGBA(r, g, b, a);
            NameLookup.AddOrUpdate(color, name, (c, n) => n);
            ColorLookup.AddOrUpdate(name, color, (n, c) => c);
            if (websafe)
            {
                lock (WebSafeColorLookup)
                {
                    if (!WebSafeColorLookup.Contains(color))
                    {
                        WebSafeColorLookup.Add(color);
                    }
                }
            }

            return color;
        }
    }
}