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

        Task<ApiResponse> LoginAsync(LoginDto dto);

        Task<ApiResponse> LogoutAsync();

        Task<ApiResponse<List<ClaimValue>>> GetClaimsAsync();

        Task<ApiResponse<LockMachineDto>> LockMachineAsync(DomainType domainType);
    }
}
