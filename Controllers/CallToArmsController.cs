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

namespace Zorn.Controllers {

    [ApiController]
    [Route("api/cta")]
    public class CallToArmsController : ControllerBase {
        private readonly IHubContext<CtaHub, ICtaHub> hubContext;
        private static ICtaRepo _repo = new CtaRepo();

        public CallToArmsController(IHubContext<CtaHub, ICtaHub> context) 
        {
            hubContext = context;
        }

        [HttpGet()]
        public IEnumerable GetCtas() {
            return _repo.Get();
        }

        [HttpGet("{id}")]
        public ActionResult GetCta(Guid id) {
            return new JsonResult(_repo.Get(id));
        }

        [HttpPost()]
        public async Task<CallToArms> AddCta([FromBody] CreateCtaPayload payload)
        {
            var cta = new CallToArms();
            cta.Title = payload.Title;

            _repo.AddCta(cta);

            await hubContext.Clients.All.CtaAdded(cta);
            return cta;
        }

        [HttpPatch("{id}/pick")]
        public async Task<ActionResult> UpvoteQuestionAsync([FromBody] RolePick payload)
        {
            var role = _repo.PickRole(payload.CtaId, payload.RoleId, payload.Player);
            await hubContext.Clients.Group(payload.CtaId.ToString()).RoleChanged(payload.CtaId, role);
            return new JsonResult(role);
        }
    }

    public class CreateCtaPayload {
        public string Title {get; set;}
    }

    public class RolePick {
        public Guid CtaId {get; set;}
        public Guid RoleId {get; set;}
        public string Player {get; set;}
    }
}