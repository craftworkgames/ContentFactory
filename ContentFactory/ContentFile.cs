namespace ContentFactory
{
    // TODO: Create sensible defaults.
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
}