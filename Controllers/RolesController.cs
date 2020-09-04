using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zorn.Models;

namespace zergtool.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly ApplicationDbContext _ctaContext;
        public RolesController( ApplicationDbContext ctaContext)
        {
            this._ctaContext = ctaContext;
        }

        [HttpGet]
        public IEnumerable<Role> Get()
        {
            return this._ctaContext.CtaRoles.ToList();
        }
    }
}
