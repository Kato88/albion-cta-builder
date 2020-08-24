using System;
using System.Collections.Generic;

namespace Zorn.Models
{
    public class CallToArms
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Setup { get; set; }
        public bool BringHammers {get; set;}
        public int ExtraSets {get; set;}
        public List<Player> Players { get; set; }

        public CallToArms()
        {
            Id = Guid.NewGuid();
            Players = new List<Player>();
        }

    }
}