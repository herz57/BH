using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace BH.Client.Services
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        [Inject]
        private ILocalStorageService LocalStorage { get; set; }

        public async Task SetTokenAsync(string token, DateTime expiry = default)
        {
            if (token == null)
            {
                await LocalStorage.RemoveItemAsync("authToken");
                await LocalStorage.RemoveItemAsync("authTokenExpiry");
            }
            else
            {
                await LocalStorage.SetItemAsync("authToken", token);
                await LocalStorage.SetItemAsync("authTokenExpiry", expiry);
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<string> GetTokenAsync()
        {
            var expiry = await LocalStorage.GetItemAsync<object>("authTokenExpiry");
            if (expiry != null)
            {
                if (DateTime.Parse(expiry.ToString()) > DateTime.Now)
                {
                    return await LocalStorage.GetItemAsStringAsync("authToken");
                }
                else
                {
                    await SetTokenAsync(null);
                }
            }
            return null;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await GetTokenAsync();
            var identity = string.IsNullOrEmpty(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
