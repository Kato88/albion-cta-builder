using System;
using System.Collections.Generic;
using System.Linq;
using Zorn.Models;

namespace Zorn.Repos {
    public class CtaRepo : ICtaRepo
    {
        public static Dictionary<Guid, CallToArms> callToArms = new Dictionary<Guid, CallToArms>();
        private Guid repoId;

        public CtaRepo()
        {
            repoId = Guid.NewGuid();
        }

        public void AddCta(CallToArms cta)
        {
            callToArms.Add(cta.Id, cta);
        }

        public List<CallToArms> Get()
        {
            return callToArms.Values.ToList();
        }

        public CallToArms Get(Guid id)
        {
            if (callToArms.ContainsKey(id)) {
                return callToArms[id];
            }

            return null;
        }

        public Guid GetRepoId()
        {
            return repoId;
        }
    }
}