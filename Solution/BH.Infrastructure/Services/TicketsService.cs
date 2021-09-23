using BH.Common.Dtos;
using BH.Infrastructure.Interfaces;
using Domain.Interfaces;
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

        public async Task<TicketDto> GetRandomTicketByMachineIdAsync(int machineId)
        {
            var result = await _ticketRepository.GetRandomTicketByMachineIdAsync(default, machineId, default);
            return new TicketDto
            {
                TicketId = result.TicketId,
                MachineId = result.MachineId,
                Cost = result.Cost,
                Win = result.Win,
                PlayedOut = result.PlayedOut,
                Symbols = result.Symbols,
            };
        }
    }
}
