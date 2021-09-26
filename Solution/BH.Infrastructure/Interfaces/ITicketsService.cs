using BH.Common.Dtos;
using System.Threading.Tasks;

namespace BH.Infrastructure.Interfaces
{
    public interface ITicketsService
    {
        Task<PlayResponseDto> GetRandomTicketByMachineIdAsync(int profileId, int machineId, int ticketCost);
    }
}
