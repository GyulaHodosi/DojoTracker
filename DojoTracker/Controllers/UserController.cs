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
        private readonly IAccountManager _accountManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService, IAccountManager accountManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _accountManager = accountManager;
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

                var mailbody = "<p>A new user would like to register:<p>" +
                               $"<p>Name: {gUser.GivenName} {gUser.FamilyName}</p>"+
                               $"<p>Email: {gUser.Email}</p>"+
                               $"<p><a href='{confirmationLink}'>Confirm user</a></p>";

                _emailService.Send("trackthatdojo@gmail.com", "new user", mailbody);

                return Ok(new {status = "newUser"});

            }

            await _signInManager.SignInAsync(user, true);

            var publicUser = await _accountManager.GeneratePublicProfileAsync(user);

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

            var user = await _userManager.FindByEmailAsync(email);

            await _accountManager.AssignRoles(user);

            var confirmationEmail =
                $"<p>Dear {firstName} {lastName},</p> <p> Your registration has been approved and you can now sign in at <a href='https://track-that-dojo.herokuapp.com'>Dojo Tracker</a></p>" +
                "<p>Best regards,</p> <p> The Dojo Tracker Team</p>";
            
            _emailService.Send(email, "Dojo Tracker registration", confirmationEmail);

            if (!result.Succeeded) return BadRequest();

            return Ok("Thanks for confirming this user, they can now log in.");
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await  _userManager.GetUserAsync(User);
            var publicUser = await _accountManager.GeneratePublicProfileAsync(user);

            return Ok(publicUser);
        }

        [HttpGet("user/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var publicUser = await _accountManager.GeneratePublicProfileAsync(user);

            return Ok(publicUser);
        }
    }
}