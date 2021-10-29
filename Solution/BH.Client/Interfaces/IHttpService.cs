using BH.Client.Models;
using BH.Common.Dtos;
using BH.Common.Enums;
using BH.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Client.Interfaces
{
    public interface IHttpService
    {
        Task<ApiResponse<PlayResponseDto>> GetTicketAsync(int machineId, int ticketCost);

        Task<ApiResponse<IList<UserStatisticDto>>> GetUsersStatisticsAsync(string forDays);

        Task<ApiResponse> LoginAsync(Login dto);

        Task<ApiResponse> LogoutAsync();

        Task<ApiResponse<List<ClaimValue>>> GetClaimsAsync();

        Task<ApiResponse<LockMachineDto>> LockMachineAsync(DomainType domainType);

        Task<ApiResponse> UnlockMachineAsync(int machineId);

        Task<ApiResponse<long>> GetProfileBalanceAsync();
    }
}
