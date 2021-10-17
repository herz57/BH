using BH.Client.Interfaces;
using BH.Common.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Client.Pages
{
    public partial class Statistics : ComponentBase
    {
        [Inject]
        private IHttpService HttpService { get; set; }

        public IList<UserStatistic> UsersStatistics { get; set; }

        private bool isLoading;
        private string forDays;

        protected override async Task OnInitializedAsync()
        {
            await GetUsersStatisticsAsync();
        }

        private async Task GetUsersStatisticsAsync()
        {
            
            isLoading = true;
            var response = await HttpService.GetUsersStatisticsAsync(forDays);
            isLoading = false;

            if (!response.IsSuccess)
                return;

            UsersStatistics = response.Content;
        }

        private async Task OnDaysInput(ChangeEventArgs e)
        {
            forDays = e.Value.ToString();
            await GetUsersStatisticsAsync();
        }
    }
}
