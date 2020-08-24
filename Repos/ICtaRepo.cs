using System;
using System.Collections.Generic;
using Zorn.Models;

namespace Zorn.Repos {
    public interface ICtaRepo
    {

        Guid GetRepoId();
        void AddCta(CallToArms cta);
        List<CallToArms> Get();
        CallToArms Get(Guid id);
    }
}