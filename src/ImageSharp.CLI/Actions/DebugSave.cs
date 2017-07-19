using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ImageSharp.CLI.Actions
{
    public class DebugSave : ImageAction
    {
        public string Output { get; set; }

        public override void RunAction(ImageOperationContext imageOperationContext)
        {
            if (bool.TryParse(Environment.GetEnvironmentVariable("CI"), out bool isCi) && isCi)
            {
                return;
            }

            var path = Output.Interpolate(imageOperationContext);
            var full = Path.GetFullPath(Path.Combine(".", path));

            Directory.CreateDirectory(Path.GetDirectoryName(full));
            imageOperationContext.Image.Save(full);
        }
    }
}
