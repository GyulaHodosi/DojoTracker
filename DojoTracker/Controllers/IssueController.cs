using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Services.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DojoTracker.Controllers
{
    [ApiController]
    [Route("/api/issue")]
    public class IssueController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly UserManager<User> _userManager;


        public IssueController(IEmailService emailService, UserManager<User> userManager)
        {
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> NewIssue(ReportedIssue issue)
        {
            var email = (await _userManager.GetUserAsync(User)).Email;
            
            _emailService.Send("trackthatdojo@gmail.com",  $"{issue.Area} - {issue.Type} - {email}", issue.Issue);

            return Ok();
        }
    }
}