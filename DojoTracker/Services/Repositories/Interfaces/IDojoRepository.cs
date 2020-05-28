using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoTracker.Models;

namespace DojoTracker.Services.Repositories.Interfaces
{
    public interface IDojoRepository
    {
        Task<IEnumerable<Dojo>> ListDojosByUserIdAsync(string userId);
        Task<Dojo> GetDojoByUserIdAsync(int id, string userId);
        Task<Dojo> GetDojoByIdAsync(int dojoId);
        Task AddDojo(Dojo dojo);
        Task<IEnumerable<Dojo>> ListUserDojosByDojoNameAsync(string userId, string dojoTitle);
    }
}
