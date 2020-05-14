using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DojoTracker.Services.Repositories
{
    public class SolutionRepository : ISolutionRepository
    {
        private readonly DojoTrackerDbContext _context;
        private readonly UserManager<User> _userManager;

        public SolutionRepository(DojoTrackerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddSolutionAsync(Solution solution, string userId)
        {
            solution.UserId = userId;
            solution.SubmissionDate = DateTime.Now;

            var result = FindSolution(solution).Result;

            if (result != null)
            {
                result.Code = solution.Code;
            }
            else
            {
                _context.Solutions.Add(solution);

                var dojoScore = await GetScoreByDojoIdAsync(solution.DojoId);
                await UpdateUserScoreOnSubmitAsync(dojoScore, userId);
            }

            await _context.SaveChangesAsync();
        }

        public IQueryable<Solution> ListSolutionsByDojoId(int dojoId)
        {
            return  _context.Solutions.Where(solution => solution.DojoId == dojoId);
        }

        public async Task<IEnumerable<Solution>> ListSolutionsByUserIdAsync(string userId)
        {
            return await _context.Solutions.Where(solution => solution.UserId == userId)
                .ToListAsync();
        }

        public async Task<Solution> GetUserSolutionByDojoIdAsync(int id, string userId, string language)
        {
            return await _context.Solutions.FirstOrDefaultAsync(solution => solution.UserId == userId &&
                                                                            solution.DojoId == id &&
                                                                            solution.Language == language);

        }

        public async Task<IEnumerable<int>> ListSolvedDojoIdsByUserIdAsync(string userId)
        {
            return await _context.Solutions.Where(solution => solution.UserId == userId)
                .Select(solution => solution.DojoId).Distinct().ToListAsync();
        }

        public async Task<DateTime> GetLastCompletedByUserIdAsync(string userId)
        {
            return await _context.Solutions.Where(solution => solution.UserId == userId)
                .Select(solution => solution.SubmissionDate).MaxAsync();
        }

        public async Task<IEnumerable<string>> ListUserIdsByDojoIdAsync(int dojoId)
        {
            return await ListSolutionsByDojoId(dojoId).Select(solution => solution.UserId).Distinct().ToListAsync();
        }

        public IQueryable<int> ListAllDojoIdsWithASolution()
        {
            return _context.Solutions.Select(solution => solution.DojoId).Distinct();
        }


        private async Task<Solution> FindSolution(Solution solution)
        {
            return await _context.Solutions.SingleOrDefaultAsync(s => s.UserId == solution.UserId &&
                                                                      s.DojoId == solution.DojoId &&
                                                                      s.Language == solution.Language);
        }

        private async Task UpdateUserScoreOnSubmitAsync(int score, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.Score += score;

            await _userManager.UpdateAsync(user);
        }

        private async Task<int> GetScoreByDojoIdAsync(int dojoId)
        {
            return (await _context.Dojos.FirstOrDefaultAsync(dojo => dojo.Id == dojoId)).Difficulty;
        }
        
    }
}
