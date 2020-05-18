using System;
using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Services.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DojoTracker.Controllers
{
    
    [Route("/api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IProfileManager _profileManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService, IProfileManager profileManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _profileManager = profileManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(GoogleUser gUser)
        {
            var user = await _userManager.FindByEmailAsync(gUser.Email);

            if (user == null)
            {
                var confirmationLink = Url.Action("Register",
                    "User", new
                    {
                        email = gUser.Email, firstName = gUser.GivenName,
                        lastName = gUser.FamilyName,
                        imageUrl = gUser.ImageUrl, id = gUser.GoogleId
                    },
                    HttpContext.Request.Scheme);

                _emailService.Send("trackthatdojo@gmail.com", "new user", confirmationLink);

                return Ok(new {status = "newUser"});

            }

            await _signInManager.SignInAsync(user, true);

            var publicUser = await _profileManager.GeneratePublicProfileAsync(user);

            return Ok(publicUser);
            
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();

            return Ok();

        }
        
        [HttpGet("register")]
        public async Task<IActionResult> Register(string email, string firstName, string lastName, string imageUrl, string id)
        {
            var newUser = new User 
                
            { UserName = email, Email = email, 
                GoogleId = id, FirstName = firstName, 
                LastName = lastName, avatarUrl = imageUrl};

            var result = await _userManager.CreateAsync(newUser);

            var confirmationEmail =
                $"Dear {firstName} {lastName},\n Your registration has been approved and you can now sign in at http://localhost:3000" +
                "\n \n Best regards,\n The Dojo Tracker Team";
            
            _emailService.Send(email, "Dojo Tracker registration", confirmationEmail);

            if (!result.Succeeded) return BadRequest();

            return Ok("Thanks for confirming this user, they can now log in.");
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetLoggedUser()
        {
            var user = await  _userManager.GetUserAsync(User);
            var publicUser = await _profileManager.GeneratePublicProfileAsync(user);

            return Ok(publicUser);
        }
    }
}