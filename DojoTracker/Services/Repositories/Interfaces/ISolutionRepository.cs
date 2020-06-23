using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models;

namespace DojoTracker.Services.Repositories.Interfaces
{
    public interface ISolutionRepository
    {
        Task<IEnumerable<Solution>> ListSolutionsByUserIdAsync(string userId);
        IQueryable<Solution> ListSolutionsByDojoId(int dojoId);
        Task<Solution> GetUserSolutionByDojoIdAsync(int id, string userId, string language);
        Task AddSolutionAsync(Solution solution, string userId);
        Task<IEnumerable<int>> ListSolvedDojoIdsByUserIdAsync(string userId);
        Task<DateTime> GetLastCompletedByUserIdAsync(string userId);
        Task<IEnumerable<string>> ListUserIdsByDojoIdAsync(int dojoId);
        IQueryable<int> ListAllDojoIdsWithASolution();
        Task DeleteSolution(int dojoId, string language, string userId);
        Task<IEnumerable<Solution>> ListAllSolutionsForDojoByUserIdAsync(string userId, int dojoId);
    }
}
