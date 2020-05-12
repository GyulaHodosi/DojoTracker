using System.Collections.Generic;
using System.Threading.Tasks;
using DojoTracker.Models;

namespace DojoTracker.Services.Statistics.Interfaces
{
    public interface IStatGenerator
    {
        Task<IEnumerable<UserStatistics>> ListAllUserStatisticsAsync();
    }
}