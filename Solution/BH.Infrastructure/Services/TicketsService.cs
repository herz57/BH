using BH.Infrastructure.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly ITicketsRepository _ticketRepository;

        public TicketsService(ITicketsRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket> GetRandomTicketByMachineIdAsync(int machineId)
        {
            return await _ticketRepository.GetRandomTicketByMachineIdAsync(machineId);
        }
    }
}
