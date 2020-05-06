using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DojoTracker.Controllers
{
    [Route("/api/solutions")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionRepository _repository; 

        public SolutionController(ISolutionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetSolutions([FromQuery] int id)
        {
            var solutions = await _repository.ListSolutionsByUserIdAsync(id);

            return Ok(solutions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSoluitionById(int id, [FromQuery] int userId, [FromQuery] string language)
        {
            var solution = await _repository.GetSolutionByDojoIdAsync(id, userId, language);

            return Ok(solution);
        }

        [HttpPost]
        public IActionResult AddSolution(Solution solution)
        {
            _repository.AddSolution(solution);

            return Ok();
        }
    }
}
