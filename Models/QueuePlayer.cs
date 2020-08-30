using System;
using System.Collections.Generic;

namespace Zorn.Models
{
    public class QueuePlayer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CallToArms Cta { get; set; }
        public List<QueueRole> Roles { get; set; }

        public QueuePlayer()
        {
            Roles = new List<QueueRole>();
        }
    }

    public class QueueRole
    {
        public Guid Id { get; set; }
        public QueuePlayer QueuePlayer { get; set; }
        public int Order { get; set; }
        public Role Role { get; set; }
    }
}