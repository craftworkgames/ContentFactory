using CommandLine;
using ContentFactory.Features.TexturePacker;

namespace ContentFactory
{
    [Verb("build", HelpText = "Builds the content.json file.")]
    public class BuildOptions
    {
        [Value(0, Default = ".", HelpText = "The directory to build content.")]
        public string TargetPath { get; set; }
    }

    [Verb("new", HelpText = "Creates a new content.json file.")]
    public class NewOptions
    {
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
            return 0;
        }

        private static int Build(BuildOptions options)
        {
            var packer = new TexturePacker(options.TargetPath, "packed.png", "packed.json");
            packer.Pack();
            return 0;
        }
    }
}
