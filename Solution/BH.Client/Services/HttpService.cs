using System.Net.Http;
using System.Threading.Tasks;
using BH.Client.Interfaces;
using BH.Common.Dtos;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Text;
using BH.Client.Models;
using BH.Common.Models;

namespace BH.Client.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse> LoginAsync(LoginDto dto)
        {
            var uri = new Uri($"{_httpClient.BaseAddress}/accounts/login");
            return await PostAsync(uri, dto);
        }

        public async Task<ApiResponse> LogoutAsync()
        {
            var uri = new Uri($"{_httpClient.BaseAddress}/accounts/logout");
            return await PostAsync(uri);
        }

        public async Task<ApiResponse<List<ClaimValue>>> GetClaimsAsync()
        {
            var uri = new Uri($"{_httpClient.BaseAddress}/accounts/claims");
            return await GetAsync<List<ClaimValue>>(uri);
        }

        public async Task<ApiResponse<TicketDto>> GetTicketAsync(int machineId)
        {
            var uri = new Uri($"{_httpClient.BaseAddress}/tickets/1");
            return await GetAsync<TicketDto>(uri);
        }

        #region private

        private async Task<ApiResponse<TOut>> GetAsync<TOut>(Uri uri) where TOut : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            var response = await _httpClient.SendAsync(request);
            var content = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<TOut>() : null;
            return new ApiResponse<TOut>(response.IsSuccessStatusCode, response.StatusCode, content);
        }

        private async Task<ApiResponse<TOut>> PostAsync<TInp, TOut>(Uri uri, TInp dto) where TOut : class
        {
            var response = await PostBaseAsync(uri, dto);
            var content = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<TOut>() : null;
            return new ApiResponse<TOut>(response.IsSuccessStatusCode, response.StatusCode, content);
        }

        private async Task<ApiResponse> PostAsync<TInp>(Uri uri, TInp dto)
        {
            var response = await PostBaseAsync(uri, dto);
            return new ApiResponse(response.IsSuccessStatusCode, response.StatusCode);
        }

        private async Task<ApiResponse> PostAsync(Uri uri)
        {
            var response = await PostBaseAsync<object>(uri, null);
            return new ApiResponse(response.IsSuccessStatusCode, response.StatusCode);
        }

        private async Task<HttpResponseMessage> PostBaseAsync<TInp>(Uri uri, TInp dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            if (dto != null)
            {
                var json = JsonConvert.SerializeObject(dto);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await _httpClient.SendAsync(request);
        }

        #endregion
    }
}
