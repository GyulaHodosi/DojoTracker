using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DojoTracker.Models;

namespace DojoTracker.Services.Repositories.Interfaces
{
    public interface ISolutionRepository
    {
        public Task<IEnumerable<Solution>> ListSolutionsByUserIdAsync(string userId);
        public Task<Solution> GetSolutionByDojoIdAsync(int id, string userId, string language);
        public void AddSolution(Solution solution, string userId);

        public Task<IEnumerable<int>> ListSolvedDojoIdsByUserIdAsync(string userId);
        Task<DateTime> GetLastCompletedByUserIdAsync(string userId);
    }
}
