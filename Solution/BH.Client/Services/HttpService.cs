using System.Net.Http;
using System.Threading.Tasks;
using BH.Client.Interfaces;
using BH.Common.Dtos;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Text;

namespace BH.Client.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> LoginAsync(LoginDto dto)
        {
            var uri = new Uri($"{_httpClient.BaseAddress}/accounts/login");
            return await PostAsync(uri, dto);
        }

        public async Task<List<Claim>> GetClaimsAsync()
        {
            var uri = new Uri($"{_httpClient.BaseAddress}/accounts/claims");
            return await GetAsync<List<Claim>>(uri);
        }

        public async Task<TicketDto> GetTicketAsync(int machineId)
        {
            var uri = new Uri($"{_httpClient.BaseAddress}/tickets/1");
            return await GetAsync<TicketDto>(uri);
        }

        private async Task<HttpResponseMessage> PostAsync<T>(Uri uri, T dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var request = new HttpRequestMessage(HttpMethod.Post, uri) { Content = new StringContent(json, Encoding.UTF8, "application/json") };
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            return await _httpClient.SendAsync(request);
        }

        private async Task<T> GetAsync<T>(Uri uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var result = await _httpClient.SendAsync(request);
            return await result.Content.ReadFromJsonAsync<T>();
        }
    }
}
