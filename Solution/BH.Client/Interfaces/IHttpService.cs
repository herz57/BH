using BH.Common.Dtos;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BH.Client.Interfaces
{
    public interface IHttpService
    {
        Task<TicketDto> GetTicketAsync(int machineId);

        Task<HttpResponseMessage> LoginAsync(LoginDto dto);

        Task<List<Claim>> GetClaimsAsync();
    }
}
