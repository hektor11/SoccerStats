using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoccerStats.Models.ViewModels
{
    public class PlayerList
    {
        public Links _links { get; set; }
        public int count { get; set; }
        public List<Player> players { get; set; }

        public class Self
        {
            public string href { get; set; }
        }

        public class Team
        {
            public string href { get; set; }
        }

        public class Links
        {
            public Self self { get; set; }
            public Team team { get; set; }
        }

        public class Player
        {
            public string name { get; set; }
            public string position { get; set; }
            public string jerseyNumber { get; set; }
            public string dateOfBirth { get; set; }
            public string nationality { get; set; }
            public string contractUntil { get; set; }
            public object marketValue { get; set; }
        }
    }
}