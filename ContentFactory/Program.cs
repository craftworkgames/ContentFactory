using System.Collections.Generic;
using System.IO;
using CommandLine;
using ContentFactory.Features.TexturePacker;
using ContentFactory.Verbs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ContentFactory
{
    public class BuildTask
    {
        public string Type { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }

    public class ContentFile
    {
        public string Title { get; set; } = "Background elements";
        public string Description { get; set; } = @"You may use these graphics in personal and commercial projects. Credit (Kenney or www.kenney.nl) would be nice but is not mandatory.";
        public string Author { get; set; } = "Kenney Vleugels";
        public string Website { get; set; } = "www.kenney.nl";
        public string License { get; set; } = "Creative Commons Zero, CC0";
        public string LicenseUrl { get; set; } = "http://creativecommons.org/publicdomain/zero/1.0/";
        public string Copyright { get; set; } = "Public Domain (CC0)";
        public BuildTask[] Tasks { get; set; } = new []{
            new BuildTask {
                Type = "pack",
                Parameters = {
                    { "sourceDirectory", "cake_64" }
                }
            },
            new BuildTask {
                Type = "pack",
                Parameters = {
                    { "sourceDirectory", "cake_128" }
                }
            }
        };
    }

    public class Program
    {
        public static int Main(string[] args)
        {
            using (var commandLineParser = Parser.Default)
            {
                return commandLineParser
                    .ParseArguments<BuildOptions, NewOptions>(args)
                    .MapResult(
                        (BuildOptions options) => Build(options),
                        (NewOptions options) => New(options),
                        errors => 1);
            }
        }

        private static int New(NewOptions options)
        {
            var serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            using (var writer = new StreamWriter("content.json"))
            {
                serializer.Serialize(writer, new ContentFile());
            }
            return 0;
        }

        private static int Build(BuildOptions options)
        {
            var directory = Path.GetFullPath(options.TargetDirectory);
            var directoryName = Path.GetFileName(directory);
            var imagePath = options.ImagePath ?? $"{directoryName}.png";
            var dataPath = options.DataPath ?? $"{directoryName}.json";
            var packer = new TexturePacker(directory, imagePath, dataPath);
            packer.Pack();
            return 0;
        }
    }
}
