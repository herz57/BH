using BH.Common.Dtos;
using BH.Common.Models;
using BH.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Infrastructure.Interfaces
{
    public interface ITicketsService
    {
        Task<PlayResponseDto> PlayAsync(User currentUser, int machineId, int ticketCost);

        IList<UserStatistic> GetUsersStatistics(int forDays);
    }
}
