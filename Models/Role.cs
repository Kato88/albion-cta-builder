using System;
using System.Collections.Generic;

namespace Zorn.Models {
    public class Role {
        public Guid Id {get; set;}
        public string Title {get; set;}
        public string Category {get; set;}
        public string InternalName {get; set;}

        public Role()
        {
            Id = Guid.NewGuid();
        }
    }
}