using BH.Common.Dtos;
using System.Net.Http;
using System.Threading.Tasks;

namespace BH.Client.Interfaces
{
    public interface IHttpService
    {
        Task<TicketDto> GetTicketAsync(int machineId);

        Task<HttpResponseMessage> LoginAsync(LoginDto dto);
    }
}
