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
        private DojoTrackerDbContext _context;

        public SolutionRepository(DojoTrackerDbContext context)
        {
            _context = context;
        }

        public void AddSolution(Solution solution)
        {
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

        public async Task<IEnumerable<Solution>> ListSolutionsByUserIdAsync(int id)
        {
           return await _context.Solutions.Where(solution => solution.UserId == id)
                                          .ToListAsync();
        }

        public async Task<IEnumerable<Solution>> GetSolutionByDojoIdAsync(int id, int userId)
        {   
            return await _context.Solutions.Where(solution => solution.UserId == userId &&
                                                              solution.DojoId == id)
                                                              .ToListAsync();
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
