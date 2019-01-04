using System.Collections.Generic;

namespace ContentFactory
{
    public class BuildTask
    {
        public string Type { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    }
}