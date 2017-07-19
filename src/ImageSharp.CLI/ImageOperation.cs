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
    public class ImageOperation
    {
        //public static IEnumerable<ImageOperationContext> Load(IEnumerable<JObject> source)
        //{
        //    return source.SelectMany(x => LoadSingleSet(x));
        //}

        //private static IEnumerable<ImageOperationContext> LoadSingleSet(JObject source)
        //{
        //    var src = source["source"];

        //    var array = src as JArray;
        //    if (array == null)
        //    {
        //        array = new JArray();
        //        array.Add(src);
        //    }

        //    List<Glob> fileGlobs = new List<Glob>();
        //    foreach (var itm in array)
        //    {
        //        var path = itm.Value<string>();
        //        var glob = Glob.Parse(path);
        //        // lets collect
        //    }
        //    var currentDir = new DirectoryInfo(".");
        //    // we scan down from here
        //    var rootName = currentDir.FullName;
        //    var referenceUri = new Uri(rootName);
        //    List<string> fileSources = new List<string>();
        //    foreach (var items in currentDir.EnumerateFileSystemInfos("*", SearchOption.AllDirectories))
        //    {
        //        var filePath = items.FullName;
        //        var fileUri = new Uri(filePath);
        //        var relativePath = referenceUri.MakeRelativeUri(fileUri).ToString();

        //        if (fileGlobs.Any(x => x.IsMatch(relativePath))) {
        //            //file matches lets keep it
        //            fileSources.Add(filePath);
        //        }
        //    }
        //    var actions = ActionCreator.GenerateActionsList((source["actions"] as JArray).OfType<JObject>());

        //    foreach (var f in fileSources)
        //    {
        //        yield return new ImageOperationContext()
        //        {
        //            Name = source["name"].ToString(),
        //            SourcePath = f,
        //            Actions = actions
        //        };

        //    }
        //}
        

        public string Name { get; set; }

        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public IList<string> Source { get; set; } = new List<string>();

        public IList<ImageAction> Actions { get; private set; } = new List<ImageAction>();

        internal void Execute(IReporter reporter)
        {
            var sourceFiles = FileSystemGlobber.EnumerateFiles(Source);
            foreach(var f in sourceFiles)
            {
                using (var ctx = new ImageOperationContext(f, this))
                {
                    ctx.Execute(reporter);
                }
            }
        }

    }
}
