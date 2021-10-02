using BH.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        Task<int> PlayAsync(string userId, int machineId, int ticketCost);

        Task<List<int>> GetAvailableMachineCosts(int machineId);

        Task<Ticket> GetTicketByIdAsync(int ticketId);
    }
}
