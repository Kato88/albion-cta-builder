using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IUserRepo _userRepo;

        public CallToArmsController(IHubContext<CtaHub, ICtaHub> context, ApplicationDbContext ctaContext, IUserRepo userRepo)
        {
            hubContext = context;
            _ctaDbContext = ctaContext;
            _userRepo = userRepo;
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
            var user = await _userRepo.GetUser(this.User);

            if (user == null)
            {
                NotFound();
                return null;
            }

            var cta = new CallToArms();
            cta.Id = Guid.NewGuid();
            cta.Title = payload.Title;
            cta.Setup = payload.Setup;
            cta.BringHammers = payload.BringHammers;
            cta.ExtraSets = payload.ExtraSets;

            cta.Parties.Add(new Party { Name = "Party 1" });
            cta.Admins.Add(new CtaAdmin
            {
                CallToArms = cta,
                UserId = new Guid(user.Id)
            });

            await _ctaDbContext.AddAsync(cta);
            await _ctaDbContext.SaveChangesAsync();

            return cta;
        }

        [HttpPost("{id}/join")]
        public async Task<ActionResult> JoinCtaQueue(Guid id, [FromBody] JoinCtaQueuePayload payload)
        {
            var cta = await _ctaDbContext.Ctas.FirstOrDefaultAsync(x => x.Id == id);

            if (cta == null)
            {
                return NotFound("CTA not found");
            }

            var queuePlayer = new QueuePlayer();
            queuePlayer.Id = Guid.NewGuid();
            queuePlayer.Name = payload.Name;

            var roles = _ctaDbContext.CtaRoles.ToList();

            payload.Roles.ForEach(role =>
            {
                var queueRole = new QueueRole
                {
                    Id = Guid.NewGuid(),
                    Order = role.Order,
                    Role = roles.FirstOrDefault(x => x.InternalName == role.RoleInternalName)
                };

                queuePlayer.Roles.Add(queueRole);
            });

            await _ctaDbContext.QueueRoles.AddRangeAsync(queuePlayer.Roles);
            await _ctaDbContext.QueuePlayers.AddAsync(queuePlayer);
            cta.Queue.Add(queuePlayer);

            await _ctaDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("{id}/leave")]
        public async Task<ActionResult> LeaveCtaQueue(Guid id, [FromBody] LeaveCtaQueuePayload payload)
        {
            var cta = await _ctaDbContext.Ctas.Include(x => x.Queue).FirstOrDefaultAsync(x => x.Id == id);

            if (cta == null)
            {
                return NotFound("CTA not found");
            }

            var queuePlayer = cta.Queue.FirstOrDefault(x => x.Name == payload.Name);

            if (queuePlayer == null)
            {
                return NotFound("You did not even join the queue. There are " + cta.Queue.Count + " players in queue");
            }

            _ctaDbContext.QueuePlayers.Remove(queuePlayer);
            await _ctaDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{id}/move")]
        public async Task<ActionResult> MoveToParty(Guid id, [FromBody] MoveToPartyPayload payload)
        {
            var user = await _userRepo.GetUser(this.User);

            if (user == null)
            {
                return Forbid();
            }

            var cta = await this._ctaDbContext.Ctas.Include(x => x.Admins).Include(x => x.Parties).Include(x => x.Queue).FirstOrDefaultAsync(x => x.Id == id);
            var role = await this._ctaDbContext.CtaRoles.FirstOrDefaultAsync(x => x.Id == payload.Role.Id);

            if (cta == null)
            {
                return Forbid("CTA not found");
            }

            if (role == null) {
                return Forbid("Role not found");
            }

            var queuePlayer = cta.Queue.FirstOrDefault(x => x.Name == payload.Name);
            var party = cta.Parties.FirstOrDefault(x => x.Name == payload.Party);
            var admin = cta.Admins.FirstOrDefault(x => x.UserId == new Guid(user.Id));

            if (queuePlayer != null && party != null && admin != null)
            {
                party.Players.Add(new Player
                {
                    Name = payload.Name,
                    Role = role
                });

                cta.Queue.Remove(queuePlayer);
                await _ctaDbContext.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return Forbid();
            }
        }
    }

    public class MoveToPartyPayload
    {
        public string Name { get; set; }
        public string Party { get; set; }
        public Role Role { get; set; }
    }

    public class LeaveCtaQueuePayload
    {
        public string Name { get; set; }
    }

    public class JoinCtaQueuePayload
    {
        public string Name { get; set; }
        public List<JoinCtaQueueRole> Roles { get; set; }
    }

    public class JoinCtaQueueRole
    {
        public int Order { get; set; }
        public string RoleInternalName { get; set; }
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