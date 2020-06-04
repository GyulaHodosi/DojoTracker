using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DojoTracker.Controllers
{
    [Route("/api/solutions")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionRepository _repository;
        private readonly UserManager<User> _userManager;

        public SolutionController(ISolutionRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetSolutions()
        {
            var userId = (await _userManager.GetUserAsync(User)).Id;
            var solutions = await _repository.ListSolutionsByUserIdAsync(userId);

            return Ok(solutions);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSoluitionById(int id, [FromQuery] string language)
        {
            var userId = (await _userManager.GetUserAsync(User)).Id;
            var solution = await _repository.GetUserSolutionByDojoIdAsync(id, userId, language);

            return Ok(solution);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddSolution(Solution solution)
        {
            var userId = (await _userManager.GetUserAsync(User)).Id;
            await _repository.AddSolutionAsync(solution, userId);

            return Ok();
        }
        
        [HttpGet("dojo/{id}/")]
        [Authorize]
        public async Task<IActionResult> GetAllSolutionsByDojoId(int id)
        {
            var solutions = await _repository.ListSolutionsByDojoId(id).ToListAsync();

            return Ok(solutions);
        }
        
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetAllSolutionsByUserId(string userId)
        {
            var solutions = await _repository.ListSolutionsByUserIdAsync(userId);

            return Ok(solutions);
        }
    }
}
