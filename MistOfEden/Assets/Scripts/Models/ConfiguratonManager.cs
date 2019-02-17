using System.Collections.Generic;

namespace Api
{
    public static class ConfigurationManager
    {
        static ConfigurationManager()
        {
            Heroes = new List<Hero>();
        }

        public static List<Hero> Heroes { get; set; }
    }
}