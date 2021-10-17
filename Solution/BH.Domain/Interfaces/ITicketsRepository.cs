using BH.Common.Models;
using BH.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Domain.Interfaces
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        Task<int> PlayAsync(string userId, int machineId, int ticketCost);

        Task<List<int>> GetAvailableMachineCosts(int machineId);

        Task<Ticket> GetTicketByIdAsync(int ticketId);

        IList<UserStatistic> GetUsersStatistics(int forDays);
    }
}
