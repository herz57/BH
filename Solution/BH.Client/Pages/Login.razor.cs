using System.Threading.Tasks;
using BH.Client.Interfaces;
using BH.Common.Dtos;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace BH.Client.Pages
{
    public partial class Login
    {
        public LoginDto LoginData { get; set; } = new LoginDto();

        [Inject]
        private ILocalStorageService LocalStorage { get; set; }

        [Inject]
        private IHttpService HttpService { get; set; }

        public async Task LoginAsync()
        {
            var token = (await HttpService.LoginAsync(LoginData));
        }
    }
}
