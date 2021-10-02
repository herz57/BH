using BH.Common.Dtos;
using BH.Common.Enums;
using System.Threading.Tasks;

namespace BH.Infrastructure.Interfaces
{
    public interface IMachinesService
    {
        Task<LockMachineDto> LockMachineAsync(string userId, DomainType domainType);
    }
}
