using System;
using System.Collections.Generic;
using Zorn.Models;

namespace Zorn.Repos {
    public interface ICtaRepo
    {
        void AddCta(CallToArms cta);
        Role PickRole(Guid ctaId, Guid roleId, string player);
        List<CallToArms> Get();
        CallToArms Get(Guid id);
    }
}