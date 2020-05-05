using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DojoTracker.Models.Repositories.Interfaces
{
    public interface ISolutionRepository
    {
        public Task<IEnumerable<Solution>> GetSolutions(int id);
        public Task<IEnumerable<Solution>> GetSolutionsById(int id, int userId);
        public void AddSolution(Solution solution);
    }
}
