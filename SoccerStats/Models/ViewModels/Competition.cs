using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerStats.Models.ViewModels
{
    public class Competition
    {
        public Links _links { get; set; }
        public int id { get; set; }
        public string caption { get; set; }
        public string league { get; set; }
        public string year { get; set; }
        public int currentMatchday { get; set; }
        public int numberOfMatchdays { get; set; }
        public int numberOfTeams { get; set; }
        public int numberOfGames { get; set; }
        public DateTime lastUpdated { get; set; }

        public class Links
        {
            public Self self { get; set; }
            public Teams teams { get; set; }
            public Fixtures fixtures { get; set; }
            public LeagueTable leagueTable { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Teams
        {
            public string href { get; set; }
        }

        public class Fixtures
        {
            public string href { get; set; }
        }

        public class LeagueTable
        {
            public string href { get; set; }
        }
    }
}