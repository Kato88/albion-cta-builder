using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zorn.Models
{
    public class CallToArms
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Setup { get; set; }
        public bool BringHammers { get; set; }
        public int ExtraSets { get; set; }
        public List<Party> Parties { get; set; }
        public List<QueuePlayer> Queue { get; set; }
        public List<CtaAdmin> Admins { get; set; }

        public CallToArms()
        {
            Parties = new List<Party>();
            Queue = new List<QueuePlayer>();
            Admins = new List<CtaAdmin>();
        }
    }
}