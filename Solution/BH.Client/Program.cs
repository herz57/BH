using BH.Client.Interfaces;
using BH.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BH.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            var services = builder.Services;

            builder.RootComponents.Add<App>("app");

            services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddBlazoredLocalStorage();

            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5005/api") });
            services.AddScoped<IHttpService, HttpService>();
            //services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}
