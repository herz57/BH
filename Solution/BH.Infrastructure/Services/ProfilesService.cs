using BH.Domain.Interfaces;
using BH.Infrastructure.Exceptions;
using BH.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
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
            try
            {
                var balance = await _profilesRepository.GetBalanceAsync(profileId);
                return balance;
            }
            catch (SqlException ex)
            {
                throw new ApiHandledException(ex.Message);
            }
        }
    }
}
