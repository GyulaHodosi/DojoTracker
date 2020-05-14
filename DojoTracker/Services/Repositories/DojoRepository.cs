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

        public async Task<Dojo> GetDojoByUserIdAsync(int id, string userId)
        {
            var dojo = await _context.Dojos.FirstOrDefaultAsync(dojo => dojo.Id == id);

            var isComplete = await
                _context.Solutions.FirstOrDefaultAsync(solution => solution.UserId == userId && solution.DojoId == id) != null;

            dojo.IsDone = isComplete;

            return dojo;

        }

        public Task<Dojo> GetDojoByIdAsync(int dojoId)
        {
            return _context.Dojos.FirstOrDefaultAsync(dojo => dojo.Id == dojoId);
        }

        public void AddDojo(Dojo dojo)
        {
            _context.Dojos.Add(dojo);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Dojo>> ListUserDojosByDojoNameAsync(string userId, string dojoTitle)
        {
            var dojos = await _context.Dojos.Where(dojo => dojo.Title.Contains(dojoTitle)).ToListAsync();
            
            await MarkAsSolved(dojos, userId);

            return dojos;
        }

        public async Task<IEnumerable<Dojo>> ListDojosByUserIdAsync(string userId)
        {
            var dojos = await _context.Dojos.OrderByDescending(d => d.Id).ToListAsync();

            await MarkAsSolved(dojos, userId);

            return dojos;
        }

        private async Task MarkAsSolved(IEnumerable<Dojo> dojos, string userId)
        {
            var solvedDojoIds = await _context.Solutions.Where(solution => solution.UserId == userId).Select(solution => solution.DojoId).ToListAsync();

            foreach (var dojo in dojos.Where(dojo => solvedDojoIds.Contains(dojo.Id)))
            {
                dojo.IsDone = true;
            }
        }
    }
}
