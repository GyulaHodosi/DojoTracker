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
        public async Task<IActionResult> GetDojos([FromQuery] int id)
        {
            try
            {
                var dojos = await _repository.ListDojosByUserIdAsync(id);
                
                return Ok(dojos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
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
