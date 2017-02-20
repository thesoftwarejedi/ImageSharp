// <copyright file="ColorConstants{TColor}.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides useful color definitions.
    /// </summary>
    public static class ColorConstants<TColor>
        where TColor : struct, IPackedPixel, IEquatable<TColor>
    {
        /// <summary>
        /// Provides a lazy, one time method of returning the colors.
        /// </summary>
        private static readonly Lazy<TColor[]> SafeColors = new Lazy<TColor[]>(GetWebSafeColors);

        /// <summary>
        /// Gets a collection of named, web safe, colors as defined in the CSS Color Module Level 4.
        /// </summary>
        public static TColor[] WebSafeColors => SafeColors.Value;


        /// <summary>
        /// Determines whether the specified color has a wellknown name.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>
        ///   <c>true</c> if the specified color is named; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWebSafe(TColor color)
        {
            if (WebSafeColors.Contains(color))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns an array of web safe colors.
        /// </summary>
        /// <returns>The <see cref="T:Color[]"/></returns>
        private static TColor[] GetWebSafeColors()
        {
            return new TColor[]
            {
                NamedColors<TColor>.AliceBlue,
                NamedColors<TColor>.AntiqueWhite,
                NamedColors<TColor>.Aqua,
                NamedColors<TColor>.Aquamarine,
                NamedColors<TColor>.Azure,
                NamedColors<TColor>.Beige,
                NamedColors<TColor>.Bisque,
                NamedColors<TColor>.Black,
                NamedColors<TColor>.BlanchedAlmond,
                NamedColors<TColor>.Blue,
                NamedColors<TColor>.BlueViolet,
                NamedColors<TColor>.Brown,
                NamedColors<TColor>.BurlyWood,
                NamedColors<TColor>.CadetBlue,
                NamedColors<TColor>.Chartreuse,
                NamedColors<TColor>.Chocolate,
                NamedColors<TColor>.Coral,
                NamedColors<TColor>.CornflowerBlue,
                NamedColors<TColor>.Cornsilk,
                NamedColors<TColor>.Crimson,
                NamedColors<TColor>.Cyan,
                NamedColors<TColor>.DarkBlue,
                NamedColors<TColor>.DarkCyan,
                NamedColors<TColor>.DarkGoldenrod,
                NamedColors<TColor>.DarkGray,
                NamedColors<TColor>.DarkGreen,
                NamedColors<TColor>.DarkKhaki,
                NamedColors<TColor>.DarkMagenta,
                NamedColors<TColor>.DarkOliveGreen,
                NamedColors<TColor>.DarkOrange,
                NamedColors<TColor>.DarkOrchid,
                NamedColors<TColor>.DarkRed,
                NamedColors<TColor>.DarkSalmon,
                NamedColors<TColor>.DarkSeaGreen,
                NamedColors<TColor>.DarkSlateBlue,
                NamedColors<TColor>.DarkSlateGray,
                NamedColors<TColor>.DarkTurquoise,
                NamedColors<TColor>.DarkViolet,
                NamedColors<TColor>.DeepPink,
                NamedColors<TColor>.DeepSkyBlue,
                NamedColors<TColor>.DimGray,
                NamedColors<TColor>.DodgerBlue,
                NamedColors<TColor>.Firebrick,
                NamedColors<TColor>.FloralWhite,
                NamedColors<TColor>.ForestGreen,
                NamedColors<TColor>.Fuchsia,
                NamedColors<TColor>.Gainsboro,
                NamedColors<TColor>.GhostWhite,
                NamedColors<TColor>.Gold,
                NamedColors<TColor>.Goldenrod,
                NamedColors<TColor>.Gray,
                NamedColors<TColor>.Green,
                NamedColors<TColor>.GreenYellow,
                NamedColors<TColor>.Honeydew,
                NamedColors<TColor>.HotPink,
                NamedColors<TColor>.IndianRed,
                NamedColors<TColor>.Indigo,
                NamedColors<TColor>.Ivory,
                NamedColors<TColor>.Khaki,
                NamedColors<TColor>.Lavender,
                NamedColors<TColor>.LavenderBlush,
                NamedColors<TColor>.LawnGreen,
                NamedColors<TColor>.LemonChiffon,
                NamedColors<TColor>.LightBlue,
                NamedColors<TColor>.LightCoral,
                NamedColors<TColor>.LightCyan,
                NamedColors<TColor>.LightGoldenrodYellow,
                NamedColors<TColor>.LightGray,
                NamedColors<TColor>.LightGreen,
                NamedColors<TColor>.LightPink,
                NamedColors<TColor>.LightSalmon,
                NamedColors<TColor>.LightSeaGreen,
                NamedColors<TColor>.LightSkyBlue,
                NamedColors<TColor>.LightSlateGray,
                NamedColors<TColor>.LightSteelBlue,
                NamedColors<TColor>.LightYellow,
                NamedColors<TColor>.Lime,
                NamedColors<TColor>.LimeGreen,
                NamedColors<TColor>.Linen,
                NamedColors<TColor>.Magenta,
                NamedColors<TColor>.Maroon,
                NamedColors<TColor>.MediumAquamarine,
                NamedColors<TColor>.MediumBlue,
                NamedColors<TColor>.MediumOrchid,
                NamedColors<TColor>.MediumPurple,
                NamedColors<TColor>.MediumSeaGreen,
                NamedColors<TColor>.MediumSlateBlue,
                NamedColors<TColor>.MediumSpringGreen,
                NamedColors<TColor>.MediumTurquoise,
                NamedColors<TColor>.MediumVioletRed,
                NamedColors<TColor>.MidnightBlue,
                NamedColors<TColor>.MintCream,
                NamedColors<TColor>.MistyRose,
                NamedColors<TColor>.Moccasin,
                NamedColors<TColor>.NavajoWhite,
                NamedColors<TColor>.Navy,
                NamedColors<TColor>.OldLace,
                NamedColors<TColor>.Olive,
                NamedColors<TColor>.OliveDrab,
                NamedColors<TColor>.Orange,
                NamedColors<TColor>.OrangeRed,
                NamedColors<TColor>.Orchid,
                NamedColors<TColor>.PaleGoldenrod,
                NamedColors<TColor>.PaleGreen,
                NamedColors<TColor>.PaleTurquoise,
                NamedColors<TColor>.PaleVioletRed,
                NamedColors<TColor>.PapayaWhip,
                NamedColors<TColor>.PeachPuff,
                NamedColors<TColor>.Peru,
                NamedColors<TColor>.Pink,
                NamedColors<TColor>.Plum,
                NamedColors<TColor>.PowderBlue,
                NamedColors<TColor>.Purple,
                NamedColors<TColor>.RebeccaPurple,
                NamedColors<TColor>.Red,
                NamedColors<TColor>.RosyBrown,
                NamedColors<TColor>.RoyalBlue,
                NamedColors<TColor>.SaddleBrown,
                NamedColors<TColor>.Salmon,
                NamedColors<TColor>.SandyBrown,
                NamedColors<TColor>.SeaGreen,
                NamedColors<TColor>.SeaShell,
                NamedColors<TColor>.Sienna,
                NamedColors<TColor>.Silver,
                NamedColors<TColor>.SkyBlue,
                NamedColors<TColor>.SlateBlue,
                NamedColors<TColor>.SlateGray,
                NamedColors<TColor>.Snow,
                NamedColors<TColor>.SpringGreen,
                NamedColors<TColor>.SteelBlue,
                NamedColors<TColor>.Tan,
                NamedColors<TColor>.Teal,
                NamedColors<TColor>.Thistle,
                NamedColors<TColor>.Tomato,
                NamedColors<TColor>.Transparent,
                NamedColors<TColor>.Turquoise,
                NamedColors<TColor>.Violet,
                NamedColors<TColor>.Wheat,
                NamedColors<TColor>.White,
                NamedColors<TColor>.WhiteSmoke,
                NamedColors<TColor>.Yellow,
                NamedColors<TColor>.YellowGreen
            };
        }
    }
}
