using BH.Client.Const;
using BH.Common.Dtos;
using BH.Common.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BH.Client.ApiHandlers
{
    public class StatusCodeHttpMessageHandler : DelegatingHandler
    {
        private readonly NavigationManager _navigation;

        public StatusCodeHttpMessageHandler(NavigationManager navigation)
        {
            _navigation = navigation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    NavigateToLogin();
                }
            }

            return response;
        }

        private void NavigateToLogin()
        {
            var url = new Uri(Consts.Pages.Login).AddQuery("returnUrl", _navigation.Uri).PathAndQuery;
            _navigation.NavigateTo(url);
        }
    }
}
