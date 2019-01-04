using System.IO;
using CommandLine;
using ContentFactory.Features.TexturePacker;
using ContentFactory.Verbs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ContentFactory
{
    public class Program
    {
        private const string _contentFileName = "content.json";

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

        private static JsonSerializer CreateSerializer()
        {
            return new JsonSerializer
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        private static int New(NewOptions options)
        {
            var serializer = CreateSerializer();
            using (var streamWriter = new StreamWriter(_contentFileName))
            using (var jsonWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(jsonWriter, new ContentFile());
            }
            return 0;
        }

        private static int Build(BuildOptions options)
        {
            var serializer = CreateSerializer();
            var contentFilePath = Path.Combine(options.ContentPath, _contentFileName);

            using (var streamReader = new StreamReader(contentFilePath))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var contentFile = serializer.Deserialize<ContentFile>(jsonReader);

                foreach (var task in contentFile.Tasks)
                {
                    var sourceDirectory = task.Parameters["sourceDirectory"].ToString();
                    var directory = Path.GetFullPath(Path.Combine(options.ContentPath, sourceDirectory));
                    var directoryName = Path.GetFileName(directory);
                    var imagePath = Path.Combine(options.ContentPath, $"{directoryName}.png");
                    var dataPath = Path.Combine(options.ContentPath, $"{directoryName}.json");
                    var packer = new TexturePacker(directory, imagePath, dataPath);
                    packer.Pack();
                }
            }

            return 0;
        }
    }
}
