using BH.Common.Dtos;
using BH.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Client.Pages
{
    public partial class Statistics : ComponentBase
    {
        public IList<UserStatisticDto> UsersStatistics { get; set; } = new List<UserStatisticDto>();

        public IList<TicketLogDto> TicketsStatistics { get; set; } = new List<TicketLogDto>();

        public MachinesStateDto MachinesState { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await ConnectToServer();
        }

        string url = "https://localhost:5005/statistic-hub";
        HubConnection _connection = null;
        bool isConnected = false;
        string connectionStatus = "Closed";

        private async Task ConnectToServer()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            await _connection.StartAsync();
            isConnected = true;
            connectionStatus = "Connected";

            _connection.Closed += async (s) =>
            {
                connectionStatus = "Disconnected";
            };

            _connection.On<string>("statistic", m =>
            {
                var result = JsonConvert.DeserializeObject<StatisticDto>(m);
                UsersStatistics = result.UserStatistics;
                MachinesState = result.MachinesState;
                StateHasChanged();
            });

            _connection.On<string>("ticket_log", m =>
            {
                var ticketStatistic = JsonConvert.DeserializeObject<TicketLogDto>(m);
                if (TicketsStatistics.Count < 5)
                {
                    TicketsStatistics.Add(ticketStatistic);
                } 
                else
                {
                    TicketsStatistics.RemoveAt(0);
                    TicketsStatistics.Add(ticketStatistic);
                }
                StateHasChanged();
            });
        }
    }
}
