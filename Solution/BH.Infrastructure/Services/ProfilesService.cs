using BH.Common.Models;
using BH.Domain.Interfaces;
using BH.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Infrastructure.Services
{
    public class ProfilesService : IProfilesService
    {
        private readonly IProfilesRepository _profilesRepository;

        public ProfilesService(IProfilesRepository profilesRepository)
        {
            _profilesRepository = profilesRepository;
        }

        public async Task<long> GetBalanceAsync(int profileId)
        {
            var balance = await _profilesRepository.GetBalanceAsync(profileId);
            return balance;
        }

        public List<UserStatisticDto> GetUsersStatistics()
        {
            var result = _profilesRepository.GetUsersStatistics();
            return result;
        }
    }
}
