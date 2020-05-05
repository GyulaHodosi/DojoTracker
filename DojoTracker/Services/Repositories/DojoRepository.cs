using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DojoTracker.Services.Repositories
{
    public class DojoRepository : IDojoRepository
    {
        private readonly DojoTrackerDbContext _context;
        public DojoRepository(DojoTrackerDbContext context)
        {
            _context = context;
        }
        public void AddDojo(Dojo dojo)
        {
            _context.Dojos.Add(dojo);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Dojo>> ListDojosByUserIdAsync(int id)
        {
            var dojos = await _context.Dojos.OrderByDescending(d => d.Id).ToListAsync();

            List<int> solvedDojoIds = await _context.Solutions.Where(solution => solution.UserId == id).Select(solution => solution.DojoId).ToListAsync();

            foreach (var dojo in dojos.Where(dojo => solvedDojoIds.Contains(dojo.Id)))
            {
                dojo.IsDone = true;
            }

            return dojos;
        }
    }
}
