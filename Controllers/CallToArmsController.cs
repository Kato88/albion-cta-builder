using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using zergtool;
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
        private ApplicationDbContext _ctaDbContext;
        private static ICtaRepo _repo = new CtaRepo();

        public CallToArmsController(IHubContext<CtaHub, ICtaHub> context, ApplicationDbContext ctaContext)
        {
            hubContext = context;
            _ctaDbContext = ctaContext;
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
            cta.Id = Guid.NewGuid();
            cta.Title = payload.Title;
            cta.Setup = payload.Setup;
            cta.BringHammers = payload.BringHammers;
            cta.ExtraSets = payload.ExtraSets;

            cta.Parties.Add(new Party { Name = "Party 1" });

            await _ctaDbContext.AddAsync(cta);
            await _ctaDbContext.SaveChangesAsync();

            return cta;
        }

        [HttpPost("{id}/join")]
        public async Task<ActionResult> JoinCtaQueue(Guid id, [FromBody] QueuePlayer queuePlayer)
        {
            queuePlayer.Id = Guid.NewGuid();
            queuePlayer.Roles.ForEach(x => x.Id = Guid.NewGuid());

            var cta = await _ctaDbContext.Ctas.FirstOrDefaultAsync(x => x.Id == id);

            if (cta == null)
            {
                return NotFound("CTA not found");
            }

            cta.Queue.Add(queuePlayer);

            await _ctaDbContext.SaveChangesAsync();
            return Ok();
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