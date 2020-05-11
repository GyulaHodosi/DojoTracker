using System.Collections.Generic;
using System.Threading.Tasks;
using DojoTracker.Models;

namespace DojoTracker.Services.Repositories.Interfaces
{
    public interface IDojoRepository
    {
        public Task<IEnumerable<Dojo>> ListDojosByUserIdAsync(string userId);
        public Task<Dojo> GetDojoByIdAsync(int id, string userId);
        public void AddDojo(Dojo dojo);
        public Task<IEnumerable<Dojo>> ListUserDojosByDojoNameAsync(string userId, string dojoTitle);
    }
}
