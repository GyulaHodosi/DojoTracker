using System.Collections.Generic;
using System.Threading.Tasks;
using DojoTracker.Models;

namespace DojoTracker.Services.Repositories.Interfaces
{
    public interface IDojoRepository
    {
        public Task<IEnumerable<Dojo>> ListDojosByUserIdAsync(int id);
        public void AddDojo(Dojo dojo);
    }
}
