using System.Collections.Generic;
using System.Threading.Tasks;
using DojoTracker.Models;

namespace DojoTracker.Services.Repositories.Interfaces
{
    public interface ISolutionRepository
    {
        public Task<IEnumerable<Solution>> ListSolutionsByUserIdAsync(int id);
        public Task<IEnumerable<Solution>> GetSolutionByDojoIdAsync(int id, int userId);
        public void AddSolution(Solution solution);
    }
}
