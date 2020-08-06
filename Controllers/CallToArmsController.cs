using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Zorn.Hubs;
using Zorn.Models;
using Zorn.Repos;

namespace Zorn.Controllers
{

    [ApiController]
    [Route("api/cta")]
    public class CallToArmsController : ControllerBase
    {
        private readonly IHubContext<CtaHub, ICtaHub> hubContext;
        private static ICtaRepo _repo = new CtaRepo();

        public CallToArmsController(IHubContext<CtaHub, ICtaHub> context)
        {
            hubContext = context;
        }

        [HttpGet()]
        public ActionResult GetCtas()
        {
            return new JsonResult(_repo.GetRepoId());
        }

        [HttpGet("{id}")]
        public ActionResult GetCta(Guid id)
        {
            return new JsonResult(_repo.Get(id));
        }

        [HttpPost()]
        public async Task<CallToArms> AddCta([FromBody] CreateCtaPayload payload)
        {
            var cta = new CallToArms();
            cta.Title = payload.Title;
            cta.Setup = payload.Setup;
            cta.BringHammers = payload.BringHammers;
            cta.ExtraSets = payload.ExtraSets;

            _repo.AddCta(cta);

            await hubContext.Clients.All.CtaAdded(cta);
            return cta;
        }

        [HttpPatch()]
        public async Task<ActionResult> PickRole([FromBody] RolePick payload)
        {
            var roles = _repo.PickRole(payload.CtaId, payload.RoleId, payload.Player);
            await hubContext.Clients.Group(payload.CtaId.ToString()).RoleChanged(payload.CtaId, roles);
            return new JsonResult(roles);
        }
    }

    public class CreateCtaPayload
    {
        public string Title { get; set; }
        public string Setup { get; set; }
        public bool BringHammers { get; set; }
        public int ExtraSets { get; set; }
    }

    public class RolePick
    {
        public Guid CtaId { get; set; }
        public Guid RoleId { get; set; }
        public string Player { get; set; }
        public string Guild { get; set; }
    }
}