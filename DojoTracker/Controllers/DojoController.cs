using DojoTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DojoTracker.Controllers
{
    [Route("/api/dojo")]
    [ApiController]
    public class DojoController : ControllerBase
    {
        private readonly DojoTrackerDbContext _context;

        public DojoController(DojoTrackerDbContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetDojos(int UserId)
        {
            //TODO: Refactor: move business logic
            try
            {
                var dojos = await _context.Dojos.OrderByDescending(d => d.Id).ToListAsync();
                List<int> solvedDojoIds = await _context.Solutions.Where(solution => solution.UserId == UserId).Select(solution => solution.DojoId).ToListAsync();
                dojos = dojos.Where(dojo => solvedDojoIds.Contains(dojo.Id)).Select(dojo => { dojo.IsDone = true; return dojo; }).ToList();
                return Ok(dojos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDojo(Dojo dojo)
        {
            _context.Dojos.Add(dojo);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
