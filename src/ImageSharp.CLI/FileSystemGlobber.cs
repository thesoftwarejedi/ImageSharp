using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DotNet.Globbing;
using Newtonsoft.Json.Linq;

namespace ImageSharp.CLI
{
    public class SystemEntryDetails
    {
        public bool IsFile { get; set; }
        public string RelativePath { get; set; }
        public string FullPath { get; set; }
        public string ParentDirectory { get; set; }
        public string FullParentDirectory { get; set; }
    }
    public static class FileSystemGlobber
    {
        public static IEnumerable<SystemEntryDetails> EnumerateFileSystemInfos(IEnumerable<string> globPatterns)
        {
            var globs = globPatterns.Distinct().Select(x => Glob.Parse(x.TrimStart('/').ToLower())).ToList();
            var currentDir = new DirectoryInfo(".");
            // we scan down from here
            var rootName = currentDir.FullName.TrimEnd('\\','/') + "\\";
            var referenceUri = new Uri(rootName);
            foreach (var items in currentDir.EnumerateFileSystemInfos("*", SearchOption.AllDirectories))
            {
                var filePath = items.FullName;
                var fileUri = new Uri(filePath);
                var relativePath = referenceUri.MakeRelativeUri(fileUri).ToString();
                var relativePathLower = relativePath.ToLower();
                if (globs.Any(g => g.IsMatch(relativePathLower)))
                {
                    //file matches lets keep it
                    yield return new SystemEntryDetails {
                        IsFile = items is FileInfo,
                        FullPath = items.FullName,
                        FullParentDirectory = Path.GetDirectoryName(items.FullName),
                        RelativePath = relativePath,
                        ParentDirectory = Path.GetDirectoryName(relativePath).Replace("\\", "/")
                    };
                }
            }
        }
        public static IEnumerable<SystemEntryDetails> EnumerateFileSystemInfos(string globPattern)
            => EnumerateFileSystemInfos(new[] { globPattern });

        public static IEnumerable<SystemEntryDetails> EnumerateFiles(string globPattern)
            => EnumerateFileSystemInfos(globPattern).Where(x => x.IsFile);

        public static IEnumerable<SystemEntryDetails> EnumerateDirectories(string globPattern)
            => EnumerateFileSystemInfos(globPattern).Where(x => !x.IsFile);

        public static IEnumerable<SystemEntryDetails> EnumerateFiles(IEnumerable<string> globPattern)
            => EnumerateFileSystemInfos(globPattern).Where(x => x.IsFile);

        public static IEnumerable<SystemEntryDetails> EnumerateDirectories(IEnumerable<string> globPattern)
            => EnumerateFileSystemInfos(globPattern).Where(x => !x.IsFile);
    }
}
