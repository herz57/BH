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
    public partial class Play : IDisposable
    {
        [Inject]
        private IHttpService HttpService { get; set; }

        protected bool IsDisabled { get; set; } = true;

        private bool isLoading;
        private bool[] showSymbols;
        private List<List<string>> symbolsPathes;
        private List<int> availableCosts;
        private IEnumerable<DomainType> availableDomainTypes;
        private PlayResponseDto playResponse;
        private int? selectedMachine;
        private long profileBalance;

        public DomainType SelectedDomain { get; set; }

        public int SelectedCost { get; set; }

        public Play()
        {
            availableDomainTypes = Enum.GetValues(typeof(DomainType)).Cast<DomainType>();
            availableCosts = new List<int>();
            showSymbols = new bool[3];
        }

        public async void Dispose()
        {
            await UnlockMachineAsync();
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
            if (!selectedMachine.HasValue || SelectedCost == default)
                return;

            isLoading = true;
            ResetShowSymbolsFlags();

            var response = await HttpService.GetTicketAsync((int)selectedMachine, SelectedCost);
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

        private async Task UnlockMachineAsync()
        {
            isLoading = true;
            await HttpService.UnlockMachineAsync((int)selectedMachine);
            isLoading = false;
        }

        private async Task OnDomainChange(ChangeEventArgs e)
        {
            var oldDomain = SelectedDomain;
            SelectedDomain = (DomainType)Enum.Parse(typeof(DomainType), e.Value.ToString());
            if (oldDomain != default && oldDomain != SelectedDomain && selectedMachine.HasValue)
            {
                await UnlockMachineAsync();
            }
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
                DomainType.First => $"../css/img/star wars/{symbol}.png",
                DomainType.Second => $"../css/img/futurama/{symbol}.png",
                DomainType.Third => $"../css/img/naruto/{symbol}.png",
                _ => throw new ArgumentException(),
            };
        }

        private async Task HandleSymbolsShowingTimeout()
        {
            await HandleTimeout(0);
            await HandleTimeout(1);
            await HandleTimeout(2);

            async Task HandleTimeout(int index)
            {
                await Task.Delay(300);
                showSymbols[index] = true;
                StateHasChanged();
            }
        }

        private void ResetShowSymbolsFlags()
        {
            for (int i = 0; i < showSymbols.Count(); i++)
                showSymbols[i] = false;
        }
    }
}
