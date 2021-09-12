using BH.Common.Dtos;
using System.Threading.Tasks;

namespace BH.Infrastructure.Interfaces
{
    public interface ITicketsService
    {
        Task<TicketDto> GetRandomTicketByMachineIdAsync(int machineId);
    }
}
