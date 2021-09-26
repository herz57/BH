using System.Threading.Tasks;

namespace BH.Domain.Interfaces
{
    public interface IProfileRepository
    {
        Task<long> GetBalance(int profileId);
    }
}
