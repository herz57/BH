using BH.Client.Services;
using BH.Common.Extensions;
using Microsoft.AspNetCore.Components;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BH.Client.ApiHandlers
{
    public class AuthorizedHandler : DelegatingHandler
    {
        private readonly AuthStateProvider _authenticationStateProvider;

        private readonly NavigationManager _navigation;

        public AuthorizedHandler(NavigationManager navigation, AuthStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _navigation = navigation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (!authState.User.Identity.IsAuthenticated)
            {
                NavigateToLogin();
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            var responseMessage = await base.SendAsync(request, cancellationToken);
            if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
            {
                //AuthenticationStateProvider.SetAnonymous();
                NavigateToLogin();
            }

            return responseMessage;
        }

        private void NavigateToLogin()
        {
            string logInPage = "https://localhost:5001/login";
            var url = new Uri(logInPage).AddQuery("returnUrl", _navigation.Uri).ToString();
            _navigation.NavigateTo(url, true);
        }
    }
}
