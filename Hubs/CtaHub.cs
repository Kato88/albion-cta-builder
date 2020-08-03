using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Zorn.Models;
using System.Linq;
using Zorn.Repos;
using System.Diagnostics;

namespace Zorn.Hubs
{
    public class CtaHub : Hub<ICtaHub>
    {
        private ICtaRepo _repo;

        public CtaHub(ICtaRepo repo) {
            _repo = repo;
        }

        public async Task Connect() {
            var all = _repo.Get();
            Debug.WriteLine($"someone connected. We currently have {all.Count} open CTAs");
            await Clients.User(Context.ConnectionId).SendAll(all);
        }

        public async Task Join(Guid ctaId)
        {
            var cta = _repo.Get(ctaId);

            if (cta == null) {
                Debug.WriteLine($"CTA with Id '{ctaId}' not found.");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, ctaId.ToString());
        }

        public async Task Leave(Guid ctaId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, ctaId.ToString());
        }

        public async Task CreateCta(string name)
        {
            var cta = new CallToArms();
            cta.Title = name;

            _repo.AddCta(cta);

            await Groups.AddToGroupAsync(Context.ConnectionId, cta.Id.ToString());

            await Clients.All.CtaAdded(cta);
        }
    }
}