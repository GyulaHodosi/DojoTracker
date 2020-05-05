using DojoTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Services.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DojoTracker.Controllers
{
    [Route("/api/dojo")]
    [ApiController]
    public class DojoController : ControllerBase
    {
        private readonly IDojoRepository _repository;

        public DojoController(IDojoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetDojosByUser([FromQuery] int userId)
        {
            try
            {
                var dojos = await _repository.ListDojosByUserIdAsync(userId);
                
                return Ok(dojos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDojo(int id,[FromQuery] int userId)
        {
            try
            {
                var dojo = await _repository.GetDojoByIdAsync(id, userId);
                return Ok(dojo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Data);
            }
        }

        [HttpPost("add")]
        public IActionResult AddDojo(Dojo dojo)
        {
            _repository.AddDojo(dojo);
            return Ok();
        }
    }
}
