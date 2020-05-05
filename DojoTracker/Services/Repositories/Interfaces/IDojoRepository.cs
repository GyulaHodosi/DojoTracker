using System.Collections.Generic;
using System.Threading.Tasks;
using DojoTracker.Models;

namespace DojoTracker.Services.Repositories.Interfaces
{
    public interface IDojoRepository
    {
        public Task<IEnumerable<Dojo>> ListDojosByUserIdAsync(int userId);
        public Task<Dojo> GetDojoByIdAsync(int id, int userId);
        public void AddDojo(Dojo dojo);
    }
}
