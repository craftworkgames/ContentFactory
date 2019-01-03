using CommandLine;

namespace ContentFactory.Verbs
{
    [Verb("build", HelpText = "Builds the content.json file.")]
    public class BuildOptions
    {
        [Value(0, MetaName = "path", Default = ".", HelpText = "The directory of images to pack.")]
        public string TargetDirectory { get; set; }

        [Option(HelpText = "The path to the output PNG image.")]
        public string ImagePath { get; set; }

        [Option(HelpText = "The path to the output JSON data file.")]
        public string DataPath { get; set; }
    }
}