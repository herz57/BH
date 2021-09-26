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
using BH.Common.Models;
using BH.Common.Extensions;
using Microsoft.JSInterop;

namespace BH.Client.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;


        public HttpService(HttpClient httpClient,
            IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
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

        public async Task<ApiResponse<PlayResponseDto>> GetTicketAsync(int machineId, int ticketCost)
        {
            var uri = new Uri($"{_httpClient.BaseAddress}/tickets/{machineId}")
                .AddQuery("ticketCost", ticketCost.ToString());

            return await GetAsync<PlayResponseDto>(uri);
        }

        #region private

        private async Task<ApiResponse<TOut>> GetAsync<TOut>(Uri uri) where TOut : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendAsync<TOut>(request);
        }

        private async Task<ApiResponse<TOut>> PostAsync<TInp, TOut>(Uri uri, TInp dto) where TOut : class
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var json = JsonConvert.SerializeObject(dto);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return await SendAsync<TOut>(request);
        }

        private async Task<ApiResponse> PostAsync<TInp>(Uri uri, TInp dto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var json = JsonConvert.SerializeObject(dto);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            return await SendAsync<object>(request);
        }

        private async Task<ApiResponse> PostAsync(Uri uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            return await SendAsync<object>(request);
        }

        private async Task<ApiResponse<TOut>> SendAsync<TOut>(HttpRequestMessage request)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiResponse<ErrorDto>>();

                if (!string.IsNullOrEmpty(error.Content?.Message))
                {
                    await _jsRuntime.InvokeVoidAsync("alert", error.Content.Message);
                }
                return new ApiResponse<TOut>(error.IsSuccess, error.StatusCode);
            }
            else
            {
                return await response.Content.ReadFromJsonAsync<ApiResponse<TOut>>();
            }
        }

        #endregion
    }
}
