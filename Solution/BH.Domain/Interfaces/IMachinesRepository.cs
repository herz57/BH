using BH.Common.Dtos;
using BH.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Domain.Interfaces
{
    public interface IMachinesRepository
    {
        Task<int> LockMachineAsync(string userId, DomainType domainType);

        Task UnlockMachineAsync(int machineId, string userId);

        Task UnlockMachinesAsync();

        Task<List<int>> GetAvailableMachineCosts(int machineId);

        MachinesStateDto GetMachinesState();
    }
}
