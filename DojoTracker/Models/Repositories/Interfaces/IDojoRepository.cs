using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DojoTracker.Models.Repositories.Interfaces
{
    public interface IDojoRepository
    {
        public Task<IEnumerable<Dojo>> GetDojos(int id);
        public void AddDojo(Dojo dojo);
    }
}
