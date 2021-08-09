using BH.Client.Interfaces;
using BH.Common.Dtos;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BH.Client.Components
{
    public partial class Main
    {
        [Inject]
        private IHttpService HttpService { get; set; }

        private TicketDto ticket;

        protected override async Task OnInitializedAsync()
        {
            ticket = await HttpService.GetTicketAsync(1);
        }
    }
}
