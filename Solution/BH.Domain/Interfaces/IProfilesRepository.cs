using BH.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Domain.Interfaces
{
    public interface IProfilesRepository
    {
        Task<long> GetBalanceAsync(int profileId);

        List<UserStatisticDto> GetUsersStatistics();
    }
}
