using System.Threading.Tasks;

namespace BH.Infrastructure.Interfaces
{
    public interface IProfilesService
    {
        Task<long> GetBalanceAsync(int profileId);
    }
}
