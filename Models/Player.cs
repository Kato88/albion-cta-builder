using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zorn.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
    }
}