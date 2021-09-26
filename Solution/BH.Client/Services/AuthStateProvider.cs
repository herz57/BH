using BH.Client.ApiHandlers;
using BH.Client.Const;
using BH.Client.Interfaces;
using BH.Common.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BH.Client.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _user = new ClaimsPrincipal(new ClaimsIdentity());

        private readonly IHttpService _httpService;
        private readonly NavigationManager _navManager;


        public AuthStateProvider(IHttpService httpService, NavigationManager navManager)
        {
            _httpService = httpService;
            _navManager = navManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!_user.Identity.IsAuthenticated && !_navManager.Uri.Contains(Consts.Pages.Login))
            {
                await FetchUserAsync();
            }
            return new AuthenticationState(_user);
        }

        public async Task SignInAsync(LoginDto dto)
        {
            var result = await _httpService.LoginAsync(dto);
            if (!result.IsSuccess)
                return;

            await FetchUserAsync();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void SetAnonymous()
        {
            _user = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task FetchUserAsync()
        {
            var response = await _httpService.GetClaimsAsync();
            if (!response.IsSuccess)
                return;

            var claims = response.Content.Select(c => new Claim(c.Type, c.Value));
            var identity = new ClaimsIdentity(nameof(AuthStateProvider), ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            identity.AddClaims(claims);
            _user = new ClaimsPrincipal(identity);
        }
    }
}
