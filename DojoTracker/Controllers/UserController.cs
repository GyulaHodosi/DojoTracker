using System.Threading.Tasks;
using DojoTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DojoTracker.Controllers
{
    
    [Route("/api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(GoogleUser gUser)
        {
            var user = await _userManager.FindByEmailAsync(gUser.Email);

            if (user == null)
            {
                var newUser = new User 
                    { UserName = gUser.Email, Email = gUser.Email, 
                        GoogleId = gUser.GoogleId, FirstName = gUser.GivenName, 
                        LastName = gUser.FamilyName, avatarUrl = gUser.ImageUrl};

                var result = await _userManager.CreateAsync(newUser);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, true);

                    return Ok("yolo");
                }
            }
            else
            {
                await _signInManager.SignInAsync(user, true);

                return Ok("swag");
            }

            return BadRequest();
        }
    }
}