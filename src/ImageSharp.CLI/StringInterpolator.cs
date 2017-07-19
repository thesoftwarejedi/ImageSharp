using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RazorLight;
using RazorLight.Extensions;

namespace ImageSharp.CLI
{
    public static class StringInterpolator
    {
        private static IRazorLightEngine _engine = RazorLight.EngineFactory.CreateEmbedded(typeof(StringInterpolator));

        public static string Interpolate<T>(this string template, T data)
        {
            return _engine.ParseString(template, data);
        }
    }
}
