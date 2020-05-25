using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IDojoRepository _dojoRepository;

        public StatGenerator(UserManager<User> userManager, ISolutionRepository solutions, IDojoRepository dojoRepository)
        {
            _userManager = userManager;
            _solutions = solutions;
            _dojoRepository = dojoRepository;
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

        public async Task<IEnumerable<DojoStatistics>> ListAllDojoStatisticsAsync()
        {
            var dojoIds = await _solutions.ListAllDojoIdsWithASolution().ToListAsync();
            
            var stats = new List<DojoStatistics>();

            foreach (var id in dojoIds)
            {
                var dojo = await _dojoRepository.GetDojoByIdAsync(id);
                stats.Add(await GenerateDojoStatisticsAsync(dojo));
            }

            return stats;
        }

        private async Task<UserStatistics> GenerateStatisticsForUserAsync(User user)
        {

            var completedDojoIds = (await GetSolvedDojoIdsByUserIdAsync(user.Id)).ToList();
            
            return new UserStatistics
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                LastCompleted = completedDojoIds.Count == 0 ? null : (DateTime?) await GetLastCompletedAsync(user.Id),
                Score = user.Score,
                CompletedDojoIds = completedDojoIds,
                NumOfCompletedDojos = completedDojoIds.Count
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
        
        private async Task<DojoStatistics> GenerateDojoStatisticsAsync(Dojo dojo)
        {
            var mostFrequent = await GetMostFrequentSolutionLanguageByDojoIdAsync(dojo.Id);
            var leastFrequent = await GetLeastFrequentSolutionLanguageByDojoIdAsync(dojo.Id);
            var solvedUserIds = (await ListUserIdsByDojoIdAsync(dojo.Id)).ToList();

            return new DojoStatistics
            {
                Id = dojo.Id,
                Difficulty = dojo.Difficulty,
                Name = dojo.Title,
                SolvedUserIds = solvedUserIds,
                NumOfUsersSolved = solvedUserIds.Count,
                MostFrequentlySolvedInLanguage = new Dictionary<string, double>
                {
                    {mostFrequent, await GetSolutionRatioForDojoByLanguageAsync(dojo.Id, mostFrequent)}
                },
                LeastFrequentlySolvedInLanguage = new Dictionary<string, double>
                {
                    {leastFrequent, await GetSolutionRatioForDojoByLanguageAsync(dojo.Id, leastFrequent)}
                }
            };
        }

        private async Task<IEnumerable<string>> ListUserIdsByDojoIdAsync(int dojoId)
        {
            return await _solutions.ListUserIdsByDojoIdAsync(dojoId);
        }
        
        private async Task<string> GetMostFrequentSolutionLanguageByDojoIdAsync(int dojoId)
        {
            return await _solutions.ListSolutionsByDojoId(dojoId).Select(solution => solution.Language).MaxAsync();
        }
        
        private async Task<string> GetLeastFrequentSolutionLanguageByDojoIdAsync(int dojoId)
        {
            return await _solutions.ListSolutionsByDojoId(dojoId).Select(solution => solution.Language).MinAsync();
        }
        
        private async Task<double> GetSolutionRatioForDojoByLanguageAsync(int dojoId, string language)
        {
            var solutionsByDojoId = _solutions.ListSolutionsByDojoId(dojoId);

            var totalNumberOfSolutions =  await  solutionsByDojoId.CountAsync();

            var numberOfSolutionsByLanguage = await solutionsByDojoId.CountAsync(solution => solution.Language == language);

            return (double) numberOfSolutionsByLanguage / totalNumberOfSolutions;
        }
    }
} 