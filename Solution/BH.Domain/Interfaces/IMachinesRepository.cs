using BH.Common.Enums;
using System.Threading.Tasks;

namespace BH.Domain.Interfaces
{
    public interface IMachinesRepository
    {
        Task<int> LockMachineAsync(string userId, DomainType domainType);
    }
}
