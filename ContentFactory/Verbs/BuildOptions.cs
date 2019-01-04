using CommandLine;

namespace ContentFactory.Verbs
{
    [Verb("build", HelpText = "Builds the content.json file.")]
    public class BuildOptions
    {
        [Value(0, MetaName = "path", Default = ".", HelpText = "The directory containing the content.json file.")]
        public string ContentPath { get; set; }
    }
}