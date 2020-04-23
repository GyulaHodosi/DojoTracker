using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace DojoTracker.Controllers
{
    [Route("/solutions")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly DojoTrackerDbContext _context;

        public SolutionController(DojoTrackerDbContext context)
        {
            _context = context;
        }

        [HttpGet ("list")]
        public async Task<IActionResult> GetSolutions([FromQuery] int id)
        {
            List<Solution> solutions = await _context.Solutions.Where(solution => solution.UserId == id).ToListAsync();

            return Ok(solutions);
        }

        [HttpPost]
        public async Task<IActionResult> AddSolution(Solution solution)
        {
            _context.Solutions.Add(solution);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
