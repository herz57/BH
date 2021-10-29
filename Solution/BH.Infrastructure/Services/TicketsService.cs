using BH.Common.Dtos;
using BH.Domain.Entities;
using BH.Domain.Interfaces;
using BH.Infrastructure.Interfaces;
using System.Threading.Tasks;
using BH.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly ITicketsRepository _ticketRepository;
        private readonly IProfilesRepository _profileRepository;
        private readonly IHubContext<StatisticHub> _statisticHub;
        private readonly ILoggerService _logger;

        public TicketsService(ITicketsRepository ticketRepository,
            IProfilesRepository profileRepository,
            IHubContext<StatisticHub> statisticHub,
            ILoggerService logger)
        {
            _ticketRepository = ticketRepository;
            _profileRepository = profileRepository;
            _statisticHub = statisticHub;
            _logger = logger;
        }

        public async Task<PlayResponseDto> PlayAsync(User currentUser, int machineId, int ticketCost)
        {
            var ticketId = await _ticketRepository.PlayAsync(currentUser.Id, machineId, ticketCost);
            var ticket = await _ticketRepository.GetTicketByIdAsync(ticketId);
            var profileBalance = await _profileRepository.GetBalanceAsync(currentUser.Profile.ProfileId);
            _logger.LogInfo($"Ticket {ticket.TicketId} has been played out", ticket.TicketId, nameof(Ticket), currentUser.Id);
            SendTicketLogToSubscribers(ticket.TicketId, currentUser.Id);

            return new PlayResponseDto
            {
                Win = ticket.Win,
                ProfileBalance = profileBalance,
                Symbols = ticket.Symbols,
            };
        }

        private void SendTicketLogToSubscribers(int ticketId, string userId)
        {
            var ticketLog = _ticketRepository.GetTicketLog(ticketId, userId);
            var serialized = JsonConvert.SerializeObject(ticketLog);
            _statisticHub.Clients.All.SendAsync("ticket_log", serialized);
        }
    }
}
