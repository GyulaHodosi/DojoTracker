using DojoTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DojoTracker.Controllers
{
    [Route("/api/dojo")]
    [ApiController]
    public class DojoController : ControllerBase
    {
        private readonly IDojoRepository _repository;
        private readonly UserManager<User> _userManager;

        public DojoController(IDojoRepository repository, UserManager<User> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetDojosByUser()
        {
            try
            {
                var userId = (await _userManager.GetUserAsync(User)).Id;
                var dojos = await _repository.ListDojosByUserIdAsync(userId);
                
                return Ok(dojos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> GetUserDojosByTitle([FromQuery] string title)
        {
            try
            {
                var userId = (await _userManager.GetUserAsync(User)).Id;
                var dojos = await _repository.ListUserDojosByDojoNameAsync(userId, title);
                
                return Ok(dojos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDojo(int id)
        {
            try
            {
                var userId = (await _userManager.GetUserAsync(User)).Id;
                var dojo = await _repository.GetDojoByUserIdAsync(id, userId);
                return Ok(dojo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Data);
            }
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddDojo(Dojo dojo)
        {
            await _repository.AddDojo(dojo);
            return Ok();
        }

        [HttpGet("{id}/status")]
        public async Task<IActionResult> CheckCompletionStatus(int id)
        {
            var userId = (await _userManager.GetUserAsync(User)).Id;
            var isDone = await  _repository.IsDojoComplete(id, userId);
            return Ok(isDone);
        }
    }
}
