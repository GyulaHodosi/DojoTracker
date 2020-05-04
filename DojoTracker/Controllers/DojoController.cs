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
        public async Task<IActionResult> GetDojos([FromQuery] int id)
        {
            //TODO: Refactor: move business logic
            try
            {
                var dojos = await _context.Dojos.OrderByDescending(d => d.Id).ToListAsync();
                
                List<int> solvedDojoIds = await _context.Solutions.Where(solution => solution.UserId == id).Select(solution => solution.DojoId).ToListAsync();
                
                foreach (var dojo in dojos.Where(dojo => solvedDojoIds.Contains(dojo.Id)))
                {
                    dojo.IsDone = true;
                }
                
                return Ok(dojos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDojo(int id)
        {
            var dojo = await _context.Dojos.FirstOrDefaultAsync(dojo => dojo.Id == id);

            var isSolved = await _context.Solutions.FirstOrDefaultAsync(solution => solution.DojoId == id) != null;

            dojo.IsDone = isSolved;

            return Ok(dojo);
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
