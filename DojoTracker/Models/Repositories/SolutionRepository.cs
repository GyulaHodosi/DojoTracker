using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DojoTracker.Models.Repositories
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

        public async Task<IEnumerable<Solution>> GetSolutions(int id)
        {
           return await _context.Solutions.Where(solution => solution.UserId == id)
                                          .ToListAsync();
        }

        public async Task<IEnumerable<Solution>> GetSolutionsById(int id, int userId)
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
