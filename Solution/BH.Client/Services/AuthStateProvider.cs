using BH.Client.Interfaces;
using BH.Common.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BH.Client.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _user = new ClaimsPrincipal(new ClaimsIdentity());

        private readonly IHttpService _httpService;

        public AuthStateProvider(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return await Task.FromResult(new AuthenticationState(_user));
        }

        public async Task SignInAsync(LoginDto dto)
        {
            var result = await _httpService.LoginAsync(dto);
            if (!result.IsSuccessStatusCode)
                return;

            //await FetchUserAsync();
        }

        public void SetAnonymous()
        {
            _user = new ClaimsPrincipal(new ClaimsIdentity());
        }

        private async Task FetchUserAsync()
        {
            var result = await _httpService.GetClaimsAsync();
            var identity = new ClaimsIdentity(result);
            _user = new ClaimsPrincipal(identity);
        }
    }
}
