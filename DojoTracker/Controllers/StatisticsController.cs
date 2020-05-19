using System.Threading.Tasks;
using DojoTracker.Services.Statistics.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DojoTracker.Controllers
{
    [ApiController]
    [Route("api/stats")]
    public class StatisticsController : ControllerBase
    {

        private readonly IStatGenerator _statGenerator;

        public StatisticsController(IStatGenerator statGenerator)
        {
            _statGenerator = statGenerator;
        }

        [HttpGet("users")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUserStatistics()
        {
            var stats = await _statGenerator.ListAllUserStatisticsAsync();

            return Ok(stats);
        }
        
        [HttpGet("dojos")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDojoStatistics()
        {
            var stats = await _statGenerator.ListAllDojoStatisticsAsync();

            return Ok(stats);
        }
    }
}