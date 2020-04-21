using DojoTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DojoTracker.Controllers
{
    [Route("/dojos")]
    [ApiController]
    public class DojoController : ControllerBase
    {
        private readonly DojoTrackerDbContext _context;

        public DojoController(DojoTrackerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDojos()
        {
            List<Dojo> dojos = await _context.Dojos.OrderByDescending(d => d.Id).ToListAsync();
            return Ok(dojos);
        }

        [HttpPost]
        public async Task<IActionResult> AddDojo(Dojo dojo)
        {
            _context.Dojos.Add(dojo);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
