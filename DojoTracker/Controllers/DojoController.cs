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
        public async Task<IActionResult> GetDojos(int UserId)
        {
            List<Dojo> dojos = await _context.Dojos.OrderByDescending(d => d.Id).ToListAsync();
            List<int> solvedDojoIds = await _context.Solutions.Where(solution => solution.UserId == UserId).Select(solution => solution.DojoId).ToListAsync();
            dojos = dojos.Where(dojo => solvedDojoIds.Contains(dojo.Id)).Select(dojo => { dojo.IsDone = true; return dojo; }).ToList();
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
