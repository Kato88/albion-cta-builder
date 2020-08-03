using System;
using System.Collections.Generic;

namespace Zorn.Models {
    public class Role {
        public Guid Id {get; set;}
        public string Title {get; set;}
        public string Category {get; set;}
        public List<Player> Players {get; set;}

        public Role()
        {
            Id = Guid.NewGuid();
            Players = new List<Player>();
        }
    }
}