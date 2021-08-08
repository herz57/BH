using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Infrastructure.Interfaces
{
    public interface ITicketsService
    {
        Task<Ticket> GetRandomTicketByMachineIdAsync(int machineId);
    }
}
