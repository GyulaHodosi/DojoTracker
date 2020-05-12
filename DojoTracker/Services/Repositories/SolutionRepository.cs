using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DojoTracker.Services.Repositories
{
    public class SolutionRepository : ISolutionRepository
    {
        private readonly DojoTrackerDbContext _context;

        public SolutionRepository(DojoTrackerDbContext context)
        {
            _context = context;
        }

        public void AddSolution(Solution solution, string userId)
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
            }

            _context.SaveChanges();
        }

        public async Task<IEnumerable<Solution>> ListSolutionsByUserIdAsync(string userId)
        {
           return await _context.Solutions.Where(solution => solution.UserId == userId)
                                          .ToListAsync();
        }

        public async Task<Solution> GetSolutionByDojoIdAsync(int id, string userId, string language)
        {
            return await _context.Solutions.FirstOrDefaultAsync(solution => solution.UserId == userId &&
                                                                            solution.DojoId == id && solution.Language == language);

        }

        public async Task<IEnumerable<int>> ListSolvedDojoIdsByUserIdAsync(string userId)
        {
            return await _context.Solutions.Where(solution => solution.UserId == userId)
                .Select(solution => solution.DojoId).ToListAsync();
        }

        public async Task<DateTime> GetLastCompletedByUserIdAsync(string userId)
        {
            return await _context.Solutions.Where(solution => solution.UserId == userId)
                .Select(solution => solution.SubmissionDate).MaxAsync();
        }

        private async Task<Solution> FindSolution(Solution solution)
        {
            Solution result = await _context.Solutions.SingleOrDefaultAsync(s => s.UserId == solution.UserId &&
                                                                                 s.DojoId == solution.DojoId && 
                                                                                 s.Language == solution.Language);

            return result;
        }
    }
}
