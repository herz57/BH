using System.Threading.Tasks;
using System.Web;
using BH.Client.Const;
using BH.Client.Services;
using BH.Common.Dtos;
using Microsoft.AspNetCore.Components;

namespace BH.Client.Pages
{
    public partial class Login : ComponentBase
    {
        [Inject]
        private AuthStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        public Common.Dtos.Login LoginData { get; set; } = new Common.Dtos.Login();

        protected override void OnInitialized()
        {
            AuthenticationStateProvider.SetAnonymous();
            base.OnInitialized();
        }

        public async Task LoginAsync()
        {
            var navigateUrl = HttpUtility.ParseQueryString(Navigation.Uri).Get("returnUrl") ?? Consts.Pages.Play;
            await AuthenticationStateProvider.SignInAsync(LoginData);
            Navigation.NavigateTo(navigateUrl);
            StateHasChanged();
        }
    }
}