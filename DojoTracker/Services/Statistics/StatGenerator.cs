using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Services.Repositories.Interfaces;
using DojoTracker.Services.Statistics.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DojoTracker.Services.Statistics
{
    public class StatGenerator : IStatGenerator
    {
        private readonly UserManager<User> _userManager;
        private readonly ISolutionRepository _solutions;

        public StatGenerator(UserManager<User> userManager, ISolutionRepository solutions)
        {
            _userManager = userManager;
            _solutions = solutions;
        }
        
        public async Task<IEnumerable<UserStatistics>> ListAllUserStatisticsAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            
            var stats = new List<UserStatistics>();

            foreach (var user in users)
            {
                stats.Add(await GenerateStatisticsForUserAsync(user));
            }

            return stats;
        }

        private async Task<UserStatistics> GenerateStatisticsForUserAsync(User user)
        {
            return new UserStatistics
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LastCompleted = await GetLastCompletedAsync(user.Id),
                Score = user.Score,
                CompletedDojoIds = await GetSolvedDojoIdsByUserIdAsync(user.Id)
            };
        }

        private async Task<IEnumerable<int>> GetSolvedDojoIdsByUserIdAsync(string userid)
        {
            return await _solutions.ListSolvedDojoIdsByUserIdAsync(userid);
        }

        private async Task<DateTime> GetLastCompletedAsync(string userId)
        {
            return await _solutions.GetLastCompletedByUserIdAsync(userId);
        }

        private async Task<User> GetUserAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}