using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//TODO: Delete this whole thing and implement JWT(maybe) login & proper authentication

namespace DojoTracker.Controllers
{
    [Route("/user")]
    [ApiController]
    public class DummyUserController : ControllerBase
    {

        private readonly DojoTrackerDbContext _dbContext;

        public DummyUserController(DojoTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetUserInfo([FromQuery] string email)
        {
            var userData = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);

            return Ok(userData);
        }

    }
}