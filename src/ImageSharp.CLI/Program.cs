using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ImageSharp.CLI.Actions;
using ImageSharp.CLI.Reporters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace ImageSharp.CLI
{
    class Program 
    {
        private readonly string glob;
        private JsonSerializerSettings settings;
        private IReporter reporter;

        public Program(string glob)
        {
            settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.SerializationBinder = new ActionsHandlerBinder();
            settings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            this.glob = glob;

            // If in CI append an appveyor/travis reporter + as tests flag
            this.reporter = new ConsoleReporter();
        }
        

        public int Run()
        {
            var sourceFiles = FileSystemGlobber.EnumerateFiles(glob);

            foreach (var sourceFile in sourceFiles)
            {
                var json = File.ReadAllText(sourceFile.FullPath);
                var context = Newtonsoft.Json.JsonConvert.DeserializeObject<ImageOperation>(json, settings);

                context.Execute(this.reporter);
            }

            if (reporter.ReportedErrors) return -1;
            return 0;
        }

        static int Main(string[] args)
        {
            var glob = "**/*.spec.json";

            if (args.Length > 0) glob = args[0];

            return new Program(glob).Run();
        }
    }
    

    public class ActionsHandlerBinder : ISerializationBinder
    {
        private Dictionary<string, Type> TypeLookup;

        public ActionsHandlerBinder()
        {
            var rootNamespace = typeof(DebugSave).Namespace;

            TypeLookup =    new Dictionary<string, Type>( typeof(DebugSave).GetTypeInfo().Assembly
                .GetTypes()
                .Where(x => x.Namespace.StartsWith(rootNamespace))
                .ToDictionary(x =>
                {
                    var key = x.FullName.Substring(rootNamespace.Length + 1);
                    return key;
                }, x => x), StringComparer.OrdinalIgnoreCase);
        }


        public Type BindToType(string assemblyName, string typeName)
        {
            return TypeLookup[typeName];
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = TypeLookup.Where(x => x.Value == serializedType).Select(x => x.Key).SingleOrDefault();
        }
    }


    class SingleOrArrayConverter<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(List<T>));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<T>>();
            }
            return new List<T> { token.ToObject<T>() };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            List<T> list = (List<T>)value;
            if (list.Count == 1)
            {
                value = list[0];
            }
            serializer.Serialize(writer, value);
        }
    }

}