using BH.Client.Interfaces;
using BH.Client.Models;
using BH.Common.Dtos;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using BH.Common.Enums;
using System;
using System.Linq;

namespace BH.Client.Pages
{
    public partial class Play
    {
        [Inject]
        private IHttpService HttpService { get; set; }

        private bool isLoading;
        private bool[] showSymbols;
        private List<List<string>> symbolsPathes;
        private List<int> availableCosts;
        private IEnumerable<DomainType> availableDomainTypes;
        private PlayResponseDto playResponse;
        private int selectedMachine;
        private long profileBalance;

        public DomainType SelectedDomain { get; set; }

        public int SelectedCost { get; set; }

        public Play()
        {
            availableDomainTypes = Enum.GetValues(typeof(DomainType)).Cast<DomainType>();
            availableCosts = new List<int>();
            showSymbols = new bool[3];
        }

        protected override async Task OnInitializedAsync()
        {
            await InitProfileBalanceAsync();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        private async Task InitProfileBalanceAsync()
        {
            isLoading = true;
            var response = await HttpService.GetProfileBalanceAsync();
            isLoading = false;

            if (!response.IsSuccess)
                return;

            profileBalance = response.Content;
        }

        private async Task OnPlayClick()
        {
            isLoading = true;
            ResetShowSymbolsFlags();

            var response = await HttpService.GetTicketAsync(selectedMachine, SelectedCost);
            isLoading = false;

            if (!response.IsSuccess)
                return;

            SetSymbolsInfo(response.Content.Symbols);
            profileBalance = response.Content.ProfileBalance;
            playResponse = response.Content;
            HandleSymbolsShowingTimeout();
        }

        private async Task InitMachineAsync()
        {
            isLoading = true;
            var response = await HttpService.LockMachineAsync(SelectedDomain);
            isLoading = false;

            if (!response.IsSuccess)
                return;

            selectedMachine = response.Content.MachineId;
            availableCosts = response.Content.AvailableCosts;
        }

        private async Task OnDomainChange(ChangeEventArgs e)
        {
            SelectedDomain = (DomainType)Enum.Parse(typeof(DomainType), e.Value.ToString());
            await InitMachineAsync();
        }

        private void SetSymbolsInfo(string symbolsJson)
        {
            var symbolsInfo = JsonConvert.DeserializeObject<SymbolsInfo>(symbolsJson);
            symbolsInfo.Symbols.ForEach(collection =>
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    collection[i] = GetSymbolPath(collection[i]);
                }
            });
            symbolsPathes = symbolsInfo.Symbols;
        }

        private string GetSymbolPath(string symbol)
        {
            return SelectedDomain switch
            {
                DomainType.First => $"../css/img/futurama/{symbol}.png",
                DomainType.Second => $"../css/img/naruto/{symbol}.png",
                DomainType.Third => $"../css/img/star wars/{symbol}.png",
                _ => throw new ArgumentException(),
            };
        }

        private async Task HandleSymbolsShowingTimeout()
        {
            showSymbols[0] = true;
            //await Task.Delay(1);
            showSymbols[1] = true;
            //await Task.Delay(1);
            showSymbols[2] = true;
        }

        private void ResetShowSymbolsFlags()
        {
            for (int i = 0; i < showSymbols.Count(); i++)
                showSymbols[i] = false;
        }
    }
}
