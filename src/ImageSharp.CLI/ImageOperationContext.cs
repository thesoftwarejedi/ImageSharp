using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DotNet.Globbing;
using ImageSharp.CLI.Actions;
using ImageSharp.CLI.Reporters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImageSharp.CLI
{
    public class ImageOperationContext : IDisposable
    {
        private readonly Image<Rgba32> image;
        private readonly ImageOperation _operation;

        public ImageOperationContext(SystemEntryDetails file, ImageOperation operation)
        {
            Name = operation.Name;
            FullFilePath = file.FullPath;
            FilePath = file.RelativePath;
            FileDirectory = file.ParentDirectory;

            this.image = ImageSharp.Image.Load(FullFilePath);
            _operation = operation;
        }
        public string Name { get; set; }

        public string FullFilePath { get; set; }
        public string FilePath { get; set; }
        public string FileDirectory { get; set; }

        public Image<Rgba32> Image => image;

        internal void Execute(IReporter reporter)
        {
            reporter.StartExecution(this);
            try
            {
                foreach (var a in _operation.Actions)
                {
                    a.RunAction(this);
                }

                reporter.CompleteExecution(this, null);
            }
            catch(Exception ex)
            {
                reporter.CompleteExecution(this, ex.Message);
            }
        }

        public void Dispose()
        {
            image.Dispose();
        }

    }
}
