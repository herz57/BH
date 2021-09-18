using System.Threading.Tasks;
using System.Web;
using BH.Client.Services;
using BH.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace BH.Client.Pages
{
    public partial class Login
    {
        [Inject]
        private AuthStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        public LoginDto LoginData { get; set; } = new LoginDto();


        public async Task LoginAsync()
        {
            string playUrl = "https://localhost:5001/play",
                returnUrl = HttpUtility.ParseQueryString(Navigation.Uri).Get("returnUrl");

            LoginData.UserName = "vasya1";
            await AuthenticationStateProvider.SignInAsync(LoginData);
            Navigation.NavigateTo(returnUrl ?? playUrl, true);
        }
    }
}