using BH.Common.Dtos;
using BH.Domain.Entities;
using BH.Domain.Interfaces;
using BH.Infrastructure.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using BH.Common.Models;

namespace Infrastructure.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly ITicketsRepository _ticketRepository;
        private readonly IProfilesRepository _profileRepository;
        private readonly ILoggerService _logger;

        public TicketsService(ITicketsRepository ticketRepository,
            IProfilesRepository profileRepository,
            ILoggerService logger)
        {
            _ticketRepository = ticketRepository;
            _profileRepository = profileRepository;
            _logger = logger;
        }

        public async Task<PlayResponseDto> PlayAsync(User currentUser, int machineId, int ticketCost)
        {
            var ticketId = await _ticketRepository.PlayAsync(currentUser.Id, machineId, ticketCost);
            var result = await _ticketRepository.GetTicketByIdAsync(ticketId);
            _logger.LogInfo($"Ticket {result.TicketId} has been played out", result.TicketId, nameof(Ticket), currentUser.Id);

            return new PlayResponseDto
            {
                Win = result.Win,
                ProfileBalance = await _profileRepository.GetBalanceAsync(currentUser.Profile.ProfileId),
                Symbols = result.Symbols,
            };
        }

        public IList<UserStatistic> GetUsersStatistics(int forDays)
        {
            var result = _ticketRepository.GetUsersStatistics(forDays);
            return result;
        }
    }
}
