using BH.Common.Dtos;
using BH.Domain.Entities;
using BH.Domain.Interfaces;
using BH.Infrastructure.Exceptions;
using BH.Infrastructure.Interfaces;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TicketsService : ITicketsService
    {
        private readonly ITicketsRepository _ticketRepository;
        private readonly IProfileRepository _profileRepository;
        
        public TicketsService(ITicketsRepository ticketRepository,
            IProfileRepository profileRepository)
        {
            _ticketRepository = ticketRepository;
            _profileRepository = profileRepository;
        }

        public async Task<PlayResponseDto> PlayAsync(User currentUser, int machineId, int ticketCost)
        {
            try
            {
                var ticketId = await _ticketRepository.PlayAsync(currentUser.Id, machineId, ticketCost);
                var result = await _ticketRepository.GetTicketByIdAsync(ticketId);
                return new PlayResponseDto
                {
                    Win = result.Win,
                    ProfileBalance = await _profileRepository.GetBalance(currentUser.Profile.ProfileId),
                    Symbols = result.Symbols,
                };
            }
            catch (SqlException ex)
            {
                throw new ApiHandledException(ex.Message);
            }
        }
    }
}
