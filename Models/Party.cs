using System;
using System.Collections.Generic;

namespace Zorn.Models {
    public class Party {
        public Guid Id {get; set;}
        public string Name { get; set; }
        public CallToArms Cta {get; set;}
        public List<Player> Players { get; set; }

        public Party()
        {
            Players = new List<Player>();
        }
    }
}