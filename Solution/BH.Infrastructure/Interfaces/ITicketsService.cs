using BH.Common.Dtos;
using BH.Domain.Entities;
using System.Threading.Tasks;

namespace BH.Infrastructure.Interfaces
{
    public interface ITicketsService
    {
        Task<PlayResponseDto> PlayAsync(User currentUser, int machineId, int ticketCost);
    }
}
