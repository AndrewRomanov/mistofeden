using System;
using System.Collections.Generic;

namespace Api
{
    public static class ConfigurationManager
    {
        static ConfigurationManager()
        {
            Heroes = new List<Hero>();
        }

		public static Action<Hero> HeroAdded;
		static List<Hero> Heroes { get; set; }
		public static void AddHero(Hero hero)
		{
			Heroes.Add(hero);
			HeroAdded?.Invoke(hero);
		}
		public static List<Hero> GetHeroes()
		{
			return Heroes;
		}
    }
}