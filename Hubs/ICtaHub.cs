using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zorn.Models;

namespace Zorn.Hubs {
    public interface ICtaHub {
        Task CtaAdded(CallToArms cta);
        Task RoleChanged(Guid ctaId, List<Role> role);
        Task SendAll(List<CallToArms> Ctas);
    }
}