using BH.Client.ApiHandlers;
using BH.Client.Interfaces;
using BH.Client.Services;
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
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddTransient<StatusCodeHttpMessageHandler>();
            services.AddTransient<IHttpService, HttpService>();

            services.AddHttpClient("ApiClient", (sp, client) =>
            {
                client.BaseAddress = new Uri("https://localhost:5005/api");
            }).AddHttpMessageHandler<StatusCodeHttpMessageHandler>();

            services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));
            services.AddSingleton<AuthStateProvider>();
            services.AddSingleton<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthStateProvider>());

            await builder.Build().RunAsync();
        }
    }
}
