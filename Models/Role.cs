using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zorn.Models {
    public class Role {
        public Guid Id {get; set;}
        public string Title {get; set;}
        public string Category {get; set;}
        public string InternalName {get; set;}
        public string IconUrl { get; set; }
    }
}