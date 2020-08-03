using System;
using System.Collections.Generic;
using System.Linq;
using Zorn.Models;

namespace Zorn.Repos {
    public class CtaRepo : ICtaRepo
    {
        public static Dictionary<Guid, CallToArms> callToArms = new Dictionary<Guid, CallToArms>();

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

        public List<Role> PickRole(Guid ctaId, Guid roleId, string player)
        {
            var cta = Get(ctaId);
            var roles = new List<Role>();

            if (cta == null) {
                return null;
            }

            var roleToRemoveFrom = cta.Roles.FirstOrDefault((x) => x.Players.Any((y) => y.Name == player));

            if (roleToRemoveFrom != null) {
                roleToRemoveFrom.Players.Remove(roleToRemoveFrom.Players.FirstOrDefault((x) => x.Name == player));
                roles.Add(roleToRemoveFrom);
            }

            var role = cta.Roles.FirstOrDefault((x) => x.Id == roleId);

            if (role == null) {
                return null;
            }

            role.Players.Add(new Player { Name = player });

            roles.Add(role);

            return roles;
        }
    }
}