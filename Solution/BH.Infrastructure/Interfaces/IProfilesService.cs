using BH.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Infrastructure.Interfaces
{
    public interface IProfilesService
    {
        Task<long> GetBalanceAsync(int profileId);

        List<UserStatisticDto> GetUsersStatistics();
    }
}
