using System.Net.Http;
using System.Threading.Tasks;
using BH.Client.Interfaces;
using BH.Common.Dtos;
using System.Net.Http.Json;

namespace BH.Client.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TicketDto> GetTicketAsync(int machineId)
        {
            return await _httpClient.GetFromJsonAsync<TicketDto>($"{_httpClient.BaseAddress}/tickets/1");
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginDto dto)
        {
            return await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/accounts/login", dto);
        }
    }
}
