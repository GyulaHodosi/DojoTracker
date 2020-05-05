using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Models.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using DojoTracker.Models.Repositories;

namespace DojoTracker.Controllers
{
    [Route("/solutions")]
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
            var solutions = await _repository.GetSolutions(id);

            return Ok(solutions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSoluitionById(int id, [FromQuery] int userId)
        {
            var solutions = await _repository.GetSolutionsById(id, userId);

            return Ok(solutions);
        }

        [HttpPost]
        public IActionResult AddSolution(Solution solution)
        {
            _repository.AddSolution(solution);

            return Ok();
        }
    }
}
