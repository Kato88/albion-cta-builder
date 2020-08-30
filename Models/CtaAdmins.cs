using System;

namespace Zorn.Models {
    public class CtaAdmin {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public CallToArms CallToArms { get; set; }
    }
}