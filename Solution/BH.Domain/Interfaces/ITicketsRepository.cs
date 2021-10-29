using BH.Common.Dtos;
using BH.Domain.Entities;
using System.Threading.Tasks;

namespace BH.Domain.Interfaces
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        Task<int> PlayAsync(string userId, int machineId, int ticketCost);

        Task<Ticket> GetTicketByIdAsync(int ticketId);

        TicketLogDto GetTicketLog(int ticketId, string userId);
    }
}
