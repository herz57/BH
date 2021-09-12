using BH.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITicketsRepository : IBaseRepository<Ticket>
    {
        Task<Ticket> GetRandomTicketByMachineIdAsync(int machineId);
    }
}
