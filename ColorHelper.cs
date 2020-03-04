//
// The Open Toolkit Library License
//
// Copyright (c) 2006 - 2008 the Open Toolkit library, except where noted.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;

namespace Monochrome.GUI
{
    public static class ColorHelper
    {
        public static Color Multiply(Color a, Color b)
        {
            return new Color(a.R*b.R, a.G*b.G, a.B*b.B, a.A*b.A);
        }
        
        public static Color? TryFromHex(ReadOnlySpan<char> hexColor)
        {
            if (hexColor.Length <= 0 || hexColor[0] != '#') return null;
            if (hexColor.Length == 9)
            {
                if (!byte.TryParse(hexColor[1..3], NumberStyles.HexNumber, null, out var r)) return null;
                if (!byte.TryParse(hexColor[3..5], NumberStyles.HexNumber, null, out var g)) return null;
                if (!byte.TryParse(hexColor[5..7], NumberStyles.HexNumber, null, out var b)) return null;
                if (!byte.TryParse(hexColor[7..9], NumberStyles.HexNumber, null, out var a)) return null;
                return new Color(r, g, b, a);
            }
            if (hexColor.Length == 7)
            {
                if (!byte.TryParse(hexColor[1..3], NumberStyles.HexNumber, null, out var r)) return null;
                if (!byte.TryParse(hexColor[3..5], NumberStyles.HexNumber, null, out var g)) return null;
                if (!byte.TryParse(hexColor[5..7], NumberStyles.HexNumber, null, out var b)) return null;
                return new Color(r, g, b);
            }

            static bool ParseDup(char chr, out byte value)
            {
                Span<char> buf = stackalloc char[2];
                buf[0] = chr;
                buf[1] = chr;

                return byte.TryParse(buf, NumberStyles.HexNumber, null, out value);
            }

            if (hexColor.Length == 5)
            {
                if (!ParseDup(hexColor[1], out var rByte)) return null;
                if (!ParseDup(hexColor[2], out var gByte)) return null;
                if (!ParseDup(hexColor[3], out var bByte)) return null;
                if (!ParseDup(hexColor[4], out var aByte)) return null;

                return new Color(rByte, gByte, bByte, aByte);
            }
            if (hexColor.Length == 4)
            {
                if (!ParseDup(hexColor[1], out var rByte)) return null;
                if (!ParseDup(hexColor[2], out var gByte)) return null;
                if (!ParseDup(hexColor[3], out var bByte)) return null;

                return new Color(rByte, gByte, bByte);
            }
            return null;
        }

        public static Color FromHex(ReadOnlySpan<char> hexColor, Color? fallback = null)
        {
            var color = TryFromHex(hexColor);
            if (color.HasValue)
                return color.Value;
            if (fallback.HasValue)
                return fallback.Value;
            throw new ArgumentException("Invalid color code and no fallback provided.", nameof(hexColor));
        }
        
        public static Color FromName(string colorname)
        {
            return DefaultColors[colorname.ToLower()];
        }
        
        public static bool TryFromName(string colorName, out Color color)
        {
            return DefaultColors.TryGetValue(colorName.ToLower(), out color);
        }
        
        public static IEnumerable<KeyValuePair<string, Color>> GetAllDefaultColors()
        {
            return DefaultColors;
        }
        
                /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 255, 255, 0).
        /// </summary>
        public static Color Transparent => new Color(255, 255, 255, 0);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (240, 248, 255, 255).
        /// </summary>
        public static Color AliceBlue => new Color(240, 248, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (250, 235, 215, 255).
        /// </summary>
        public static Color AntiqueWhite => new Color(250, 235, 215, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 255, 255, 255).
        /// </summary>
        public static Color Aqua => new Color(0, 255, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (127, 255, 212, 255).
        /// </summary>
        public static Color Aquamarine => new Color(127, 255, 212, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (240, 255, 255, 255).
        /// </summary>
        public static Color Azure => new Color(240, 255, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (245, 245, 220, 255).
        /// </summary>
        public static Color Beige => new Color(245, 245, 220, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 228, 196, 255).
        /// </summary>
        public static Color Bisque => new Color(255, 228, 196, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 0, 0, 255).
        /// </summary>
        public static Color Black => new Color(0, 0, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 235, 205, 255).
        /// </summary>
        public static Color BlanchedAlmond => new Color(255, 235, 205, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 0, 255, 255).
        /// </summary>
        public static Color Blue => new Color(0, 0, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (138, 43, 226, 255).
        /// </summary>
        public static Color BlueViolet => new Color(138, 43, 226, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (165, 42, 42, 255).
        /// </summary>
        public static Color Brown => new Color(165, 42, 42, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (222, 184, 135, 255).
        /// </summary>
        public static Color BurlyWood => new Color(222, 184, 135, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (95, 158, 160, 255).
        /// </summary>
        public static Color CadetBlue => new Color(95, 158, 160, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (127, 255, 0, 255).
        /// </summary>
        public static Color Chartreuse => new Color(127, 255, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (210, 105, 30, 255).
        /// </summary>
        public static Color Chocolate => new Color(210, 105, 30, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 127, 80, 255).
        /// </summary>
        public static Color Coral => new Color(255, 127, 80, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (100, 149, 237, 255).
        /// </summary>
        public static Color CornflowerBlue => new Color(100, 149, 237, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 248, 220, 255).
        /// </summary>
        public static Color Cornsilk => new Color(255, 248, 220, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (220, 20, 60, 255).
        /// </summary>
        public static Color Crimson => new Color(220, 20, 60, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 255, 255, 255).
        /// </summary>
        public static Color Cyan => new Color(0, 255, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 0, 139, 255).
        /// </summary>
        public static Color DarkBlue => new Color(0, 0, 139, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 139, 139, 255).
        /// </summary>
        public static Color DarkCyan => new Color(0, 139, 139, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (184, 134, 11, 255).
        /// </summary>
        public static Color DarkGoldenrod => new Color(184, 134, 11, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (169, 169, 169, 255).
        /// </summary>
        public static Color DarkGray => new Color(169, 169, 169, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 100, 0, 255).
        /// </summary>
        public static Color DarkGreen => new Color(0, 100, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (189, 183, 107, 255).
        /// </summary>
        public static Color DarkKhaki => new Color(189, 183, 107, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (139, 0, 139, 255).
        /// </summary>
        public static Color DarkMagenta => new Color(139, 0, 139, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (85, 107, 47, 255).
        /// </summary>
        public static Color DarkOliveGreen => new Color(85, 107, 47, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 140, 0, 255).
        /// </summary>
        public static Color DarkOrange => new Color(255, 140, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (153, 50, 204, 255).
        /// </summary>
        public static Color DarkOrchid => new Color(153, 50, 204, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (139, 0, 0, 255).
        /// </summary>
        public static Color DarkRed => new Color(139, 0, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (233, 150, 122, 255).
        /// </summary>
        public static Color DarkSalmon => new Color(233, 150, 122, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (143, 188, 139, 255).
        /// </summary>
        public static Color DarkSeaGreen => new Color(143, 188, 139, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (72, 61, 139, 255).
        /// </summary>
        public static Color DarkSlateBlue => new Color(72, 61, 139, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (47, 79, 79, 255).
        /// </summary>
        public static Color DarkSlateGray => new Color(47, 79, 79, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 206, 209, 255).
        /// </summary>
        public static Color DarkTurquoise => new Color(0, 206, 209, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (148, 0, 211, 255).
        /// </summary>
        public static Color DarkViolet => new Color(148, 0, 211, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 20, 147, 255).
        /// </summary>
        public static Color DeepPink => new Color(255, 20, 147, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 191, 255, 255).
        /// </summary>
        public static Color DeepSkyBlue => new Color(0, 191, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (105, 105, 105, 255).
        /// </summary>
        public static Color DimGray => new Color(105, 105, 105, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (30, 144, 255, 255).
        /// </summary>
        public static Color DodgerBlue => new Color(30, 144, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (178, 34, 34, 255).
        /// </summary>
        public static Color Firebrick => new Color(178, 34, 34, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 250, 240, 255).
        /// </summary>
        public static Color FloralWhite => new Color(255, 250, 240, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (34, 139, 34, 255).
        /// </summary>
        public static Color ForestGreen => new Color(34, 139, 34, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 0, 255, 255).
        /// </summary>
        public static Color Fuchsia => new Color(255, 0, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (220, 220, 220, 255).
        /// </summary>
        public static Color Gainsboro => new Color(220, 220, 220, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (248, 248, 255, 255).
        /// </summary>
        public static Color GhostWhite => new Color(248, 248, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 215, 0, 255).
        /// </summary>
        public static Color Gold => new Color(255, 215, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (218, 165, 32, 255).
        /// </summary>
        public static Color Goldenrod => new Color(218, 165, 32, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (128, 128, 128, 255).
        /// </summary>
        public static Color Gray => new Color(128, 128, 128, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 128, 0, 255).
        /// </summary>
        public static Color Green => new Color(0, 128, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (173, 255, 47, 255).
        /// </summary>
        public static Color GreenYellow => new Color(173, 255, 47, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (240, 255, 240, 255).
        /// </summary>
        public static Color Honeydew => new Color(240, 255, 240, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 105, 180, 255).
        /// </summary>
        public static Color HotPink => new Color(255, 105, 180, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (205, 92, 92, 255).
        /// </summary>
        public static Color IndianRed => new Color(205, 92, 92, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (75, 0, 130, 255).
        /// </summary>
        public static Color Indigo => new Color(75, 0, 130, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 255, 240, 255).
        /// </summary>
        public static Color Ivory => new Color(255, 255, 240, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (240, 230, 140, 255).
        /// </summary>
        public static Color Khaki => new Color(240, 230, 140, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (230, 230, 250, 255).
        /// </summary>
        public static Color Lavender => new Color(230, 230, 250, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 240, 245, 255).
        /// </summary>
        public static Color LavenderBlush => new Color(255, 240, 245, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (124, 252, 0, 255).
        /// </summary>
        public static Color LawnGreen => new Color(124, 252, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 250, 205, 255).
        /// </summary>
        public static Color LemonChiffon => new Color(255, 250, 205, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (173, 216, 230, 255).
        /// </summary>
        public static Color LightBlue => new Color(173, 216, 230, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (240, 128, 128, 255).
        /// </summary>
        public static Color LightCoral => new Color(240, 128, 128, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (224, 255, 255, 255).
        /// </summary>
        public static Color LightCyan => new Color(224, 255, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (250, 250, 210, 255).
        /// </summary>
        public static Color LightGoldenrodYellow => new Color(250, 250, 210, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (144, 238, 144, 255).
        /// </summary>
        public static Color LightGreen => new Color(144, 238, 144, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (211, 211, 211, 255).
        /// </summary>
        public static Color LightGray => new Color(211, 211, 211, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 182, 193, 255).
        /// </summary>
        public static Color LightPink => new Color(255, 182, 193, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 160, 122, 255).
        /// </summary>
        public static Color LightSalmon => new Color(255, 160, 122, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (32, 178, 170, 255).
        /// </summary>
        public static Color LightSeaGreen => new Color(32, 178, 170, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (135, 206, 250, 255).
        /// </summary>
        public static Color LightSkyBlue => new Color(135, 206, 250, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (119, 136, 153, 255).
        /// </summary>
        public static Color LightSlateGray => new Color(119, 136, 153, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (176, 196, 222, 255).
        /// </summary>
        public static Color LightSteelBlue => new Color(176, 196, 222, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 255, 224, 255).
        /// </summary>
        public static Color LightYellow => new Color(255, 255, 224, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 255, 0, 255).
        /// </summary>
        public static Color Lime => new Color(0, 255, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (50, 205, 50, 255).
        /// </summary>
        public static Color LimeGreen => new Color(50, 205, 50, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (250, 240, 230, 255).
        /// </summary>
        public static Color Linen => new Color(250, 240, 230, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 0, 255, 255).
        /// </summary>
        public static Color Magenta => new Color(255, 0, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (128, 0, 0, 255).
        /// </summary>
        public static Color Maroon => new Color(128, 0, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (102, 205, 170, 255).
        /// </summary>
        public static Color MediumAquamarine => new Color(102, 205, 170, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 0, 205, 255).
        /// </summary>
        public static Color MediumBlue => new Color(0, 0, 205, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (186, 85, 211, 255).
        /// </summary>
        public static Color MediumOrchid => new Color(186, 85, 211, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (147, 112, 219, 255).
        /// </summary>
        public static Color MediumPurple => new Color(147, 112, 219, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (60, 179, 113, 255).
        /// </summary>
        public static Color MediumSeaGreen => new Color(60, 179, 113, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (123, 104, 238, 255).
        /// </summary>
        public static Color MediumSlateBlue => new Color(123, 104, 238, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 250, 154, 255).
        /// </summary>
        public static Color MediumSpringGreen => new Color(0, 250, 154, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (72, 209, 204, 255).
        /// </summary>
        public static Color MediumTurquoise => new Color(72, 209, 204, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (199, 21, 133, 255).
        /// </summary>
        public static Color MediumVioletRed => new Color(199, 21, 133, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (25, 25, 112, 255).
        /// </summary>
        public static Color MidnightBlue => new Color(25, 25, 112, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (245, 255, 250, 255).
        /// </summary>
        public static Color MintCream => new Color(245, 255, 250, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 228, 225, 255).
        /// </summary>
        public static Color MistyRose => new Color(255, 228, 225, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 228, 181, 255).
        /// </summary>
        public static Color Moccasin => new Color(255, 228, 181, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 222, 173, 255).
        /// </summary>
        public static Color NavajoWhite => new Color(255, 222, 173, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 0, 128, 255).
        /// </summary>
        public static Color Navy => new Color(0, 0, 128, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (253, 245, 230, 255).
        /// </summary>
        public static Color OldLace => new Color(253, 245, 230, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (128, 128, 0, 255).
        /// </summary>
        public static Color Olive => new Color(128, 128, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (107, 142, 35, 255).
        /// </summary>
        public static Color OliveDrab => new Color(107, 142, 35, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 165, 0, 255).
        /// </summary>
        public static Color Orange => new Color(255, 165, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 69, 0, 255).
        /// </summary>
        public static Color OrangeRed => new Color(255, 69, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (218, 112, 214, 255).
        /// </summary>
        public static Color Orchid => new Color(218, 112, 214, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (238, 232, 170, 255).
        /// </summary>
        public static Color PaleGoldenrod => new Color(238, 232, 170, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (152, 251, 152, 255).
        /// </summary>
        public static Color PaleGreen => new Color(152, 251, 152, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (175, 238, 238, 255).
        /// </summary>
        public static Color PaleTurquoise => new Color(175, 238, 238, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (219, 112, 147, 255).
        /// </summary>
        public static Color PaleVioletRed => new Color(219, 112, 147, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 239, 213, 255).
        /// </summary>
        public static Color PapayaWhip => new Color(255, 239, 213, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 218, 185, 255).
        /// </summary>
        public static Color PeachPuff => new Color(255, 218, 185, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (205, 133, 63, 255).
        /// </summary>
        public static Color Peru => new Color(205, 133, 63, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 192, 203, 255).
        /// </summary>
        public static Color Pink => new Color(255, 192, 203, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (221, 160, 221, 255).
        /// </summary>
        public static Color Plum => new Color(221, 160, 221, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (176, 224, 230, 255).
        /// </summary>
        public static Color PowderBlue => new Color(176, 224, 230, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (128, 0, 128, 255).
        /// </summary>
        public static Color Purple => new Color(128, 0, 128, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 0, 0, 255).
        /// </summary>
        public static Color Red => new Color(255, 0, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (188, 143, 143, 255).
        /// </summary>
        public static Color RosyBrown => new Color(188, 143, 143, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (65, 105, 225, 255).
        /// </summary>
        public static Color RoyalBlue => new Color(65, 105, 225, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (139, 69, 19, 255).
        /// </summary>
        public static Color SaddleBrown => new Color(139, 69, 19, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (250, 128, 114, 255).
        /// </summary>
        public static Color Salmon => new Color(250, 128, 114, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (244, 164, 96, 255).
        /// </summary>
        public static Color SandyBrown => new Color(244, 164, 96, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (46, 139, 87, 255).
        /// </summary>
        public static Color SeaGreen => new Color(46, 139, 87, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 245, 238, 255).
        /// </summary>
        public static Color SeaShell => new Color(255, 245, 238, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (160, 82, 45, 255).
        /// </summary>
        public static Color Sienna => new Color(160, 82, 45, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (192, 192, 192, 255).
        /// </summary>
        public static Color Silver => new Color(192, 192, 192, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (135, 206, 235, 255).
        /// </summary>
        public static Color SkyBlue => new Color(135, 206, 235, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (106, 90, 205, 255).
        /// </summary>
        public static Color SlateBlue => new Color(106, 90, 205, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (112, 128, 144, 255).
        /// </summary>
        public static Color SlateGray => new Color(112, 128, 144, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 250, 250, 255).
        /// </summary>
        public static Color Snow => new Color(255, 250, 250, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 255, 127, 255).
        /// </summary>
        public static Color SpringGreen => new Color(0, 255, 127, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (70, 130, 180, 255).
        /// </summary>
        public static Color SteelBlue => new Color(70, 130, 180, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (210, 180, 140, 255).
        /// </summary>
        public static Color Tan => new Color(210, 180, 140, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (0, 128, 128, 255).
        /// </summary>
        public static Color Teal => new Color(0, 128, 128, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (216, 191, 216, 255).
        /// </summary>
        public static Color Thistle => new Color(216, 191, 216, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 99, 71, 255).
        /// </summary>
        public static Color Tomato => new Color(255, 99, 71, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (64, 224, 208, 255).
        /// </summary>
        public static Color Turquoise => new Color(64, 224, 208, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (238, 130, 238, 255).
        /// </summary>
        public static Color Violet => new Color(238, 130, 238, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (245, 222, 179, 255).
        /// </summary>
        public static Color Wheat => new Color(245, 222, 179, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 255, 255, 255).
        /// </summary>
        public static Color White => new Color(255, 255, 255, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (245, 245, 245, 255).
        /// </summary>
        public static Color WhiteSmoke => new Color(245, 245, 245, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (255, 255, 0, 255).
        /// </summary>
        public static Color Yellow => new Color(255, 255, 0, 255);

        /// <summary>
        ///     Gets the system color with (R, G, B, A) = (154, 205, 50, 255).
        /// </summary>
        public static Color YellowGreen => new Color(154, 205, 50, 255);

        private static readonly Dictionary<string, Color> DefaultColors = new Dictionary<string, Color>
        {
            ["transparent"] = Transparent,
            ["aliceblue"] = AliceBlue,
            ["antiquewhite"] = AntiqueWhite,
            ["aqua"] = Aqua,
            ["aquamarine"] = Aquamarine,
            ["azure"] = Azure,
            ["beige"] = Beige,
            ["bisque"] = Bisque,
            ["black"] = Black,
            ["blanchedalmond"] = BlanchedAlmond,
            ["blue"] = Blue,
            ["blueviolet"] = BlueViolet,
            ["brown"] = Brown,
            ["burlywood"] = BurlyWood,
            ["cadetblue"] = CadetBlue,
            ["chartreuse"] = Chartreuse,
            ["chocolate"] = Chocolate,
            ["coral"] = Coral,
            ["cornflowerblue"] = CornflowerBlue,
            ["cornsilk"] = Cornsilk,
            ["crimson"] = Crimson,
            ["cyan"] = Cyan,
            ["darkblue"] = DarkBlue,
            ["darkcyan"] = DarkCyan,
            ["darkgoldenrod"] = DarkGoldenrod,
            ["darkgray"] = DarkGray,
            ["darkgreen"] = DarkGreen,
            ["darkkhaki"] = DarkKhaki,
            ["darkmagenta"] = DarkMagenta,
            ["darkolivegreen"] = DarkOliveGreen,
            ["darkorange"] = DarkOrange,
            ["darkorchid"] = DarkOrchid,
            ["darkred"] = DarkRed,
            ["darksalmon"] = DarkSalmon,
            ["darkseagreen"] = DarkSeaGreen,
            ["darkslateblue"] = DarkSlateBlue,
            ["darkslategray"] = DarkSlateGray,
            ["darkturquoise"] = DarkTurquoise,
            ["darkviolet"] = DarkViolet,
            ["deeppink"] = DeepPink,
            ["deepskyblue"] = DeepSkyBlue,
            ["dimgray"] = DimGray,
            ["dodgerblue"] = DodgerBlue,
            ["firebrick"] = Firebrick,
            ["floralwhite"] = FloralWhite,
            ["forestgreen"] = ForestGreen,
            ["fuchsia"] = Fuchsia,
            ["gainsboro"] = Gainsboro,
            ["ghostwhite"] = GhostWhite,
            ["gold"] = Gold,
            ["goldenrod"] = Goldenrod,
            ["gray"] = Gray,
            ["green"] = Green,
            ["greenyellow"] = GreenYellow,
            ["honeydew"] = Honeydew,
            ["hotpink"] = HotPink,
            ["indianred"] = IndianRed,
            ["indigo"] = Indigo,
            ["ivory"] = Ivory,
            ["khaki"] = Khaki,
            ["lavender"] = Lavender,
            ["lavenderblush"] = LavenderBlush,
            ["lawngreen"] = LawnGreen,
            ["lemonchiffon"] = LemonChiffon,
            ["lightblue"] = LightBlue,
            ["lightcoral"] = LightCoral,
            ["lightcyan"] = LightCyan,
            ["lightgoldenrodyellow"] = LightGoldenrodYellow,
            ["lightgreen"] = LightGreen,
            ["lightgray"] = LightGray,
            ["lightpink"] = LightPink,
            ["lightsalmon"] = LightSalmon,
            ["lightseagreen"] = LightSeaGreen,
            ["lightskyblue"] = LightSkyBlue,
            ["lightslategray"] = LightSlateGray,
            ["lightsteelblue"] = LightSteelBlue,
            ["lightyellow"] = LightYellow,
            ["lime"] = Lime,
            ["limegreen"] = LimeGreen,
            ["linen"] = Linen,
            ["magenta"] = Magenta,
            ["maroon"] = Maroon,
            ["mediumaquamarine"] = MediumAquamarine,
            ["mediumblue"] = MediumBlue,
            ["mediumorchid"] = MediumOrchid,
            ["mediumpurple"] = MediumPurple,
            ["mediumseagreen"] = MediumSeaGreen,
            ["mediumslateblue"] = MediumSlateBlue,
            ["mediumspringgreen"] = MediumSpringGreen,
            ["mediumturquoise"] = MediumTurquoise,
            ["mediumvioletred"] = MediumVioletRed,
            ["midnightblue"] = MidnightBlue,
            ["mintcream"] = MintCream,
            ["mistyrose"] = MistyRose,
            ["moccasin"] = Moccasin,
            ["navajowhite"] = NavajoWhite,
            ["navy"] = Navy,
            ["oldlace"] = OldLace,
            ["olive"] = Olive,
            ["olivedrab"] = OliveDrab,
            ["orange"] = Orange,
            ["orangered"] = OrangeRed,
            ["orchid"] = Orchid,
            ["palegoldenrod"] = PaleGoldenrod,
            ["palegreen"] = PaleGreen,
            ["paleturquoise"] = PaleTurquoise,
            ["palevioletred"] = PaleVioletRed,
            ["papayawhip"] = PapayaWhip,
            ["peachpuff"] = PeachPuff,
            ["peru"] = Peru,
            ["pink"] = Pink,
            ["plum"] = Plum,
            ["powderblue"] = PowderBlue,
            ["purple"] = Purple,
            ["red"] = Red,
            ["rosybrown"] = RosyBrown,
            ["royalblue"] = RoyalBlue,
            ["saddlebrown"] = SaddleBrown,
            ["salmon"] = Salmon,
            ["sandybrown"] = SandyBrown,
            ["seagreen"] = SeaGreen,
            ["seashell"] = SeaShell,
            ["sienna"] = Sienna,
            ["silver"] = Silver,
            ["skyblue"] = SkyBlue,
            ["slateblue"] = SlateBlue,
            ["slategray"] = SlateGray,
            ["snow"] = Snow,
            ["springgreen"] = SpringGreen,
            ["steelblue"] = SteelBlue,
            ["tan"] = Tan,
            ["teal"] = Teal,
            ["thistle"] = Thistle,
            ["tomato"] = Tomato,
            ["turquoise"] = Turquoise,
            ["violet"] = Violet,
            ["wheat"] = Wheat,
            ["white"] = White,
            ["whitesmoke"] = WhiteSmoke,
            ["yellow"] = Yellow,
            ["yellowgreen"] = YellowGreen,
        };

    }
}