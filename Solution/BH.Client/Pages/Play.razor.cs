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
        private PlayResponseDto playResponse;
        private List<List<string>> symbolsPathes;
        private bool[] showSymbols = new bool[3];
        private List<int> availableCosts = new List<int>();
        private IEnumerable<DomainType> availableDomainTypes = Enum.GetValues(typeof(DomainType)).Cast<DomainType>();

        private DomainType selectedDomain;
        private int selectedCost;
        private int selectedMachine;

        protected override async Task OnInitializedAsync()
        {
        }

        private async Task OnPlayClick()
        {
            isLoading = true;
            ResetShowSymbolsFlags();
            selectedDomain = DomainType.Third;

            var response = await HttpService.GetTicketAsync(selectedMachine, selectedCost);
            isLoading = false;

            if (!response.IsSuccess)
                return;

            SetSymbolsInfo(response.Content.Symbols);
            playResponse = response.Content;
            HandleSymbolsShowingTimeout();
        }

        private void OnCostChange(ChangeEventArgs e)
        {
            selectedCost = Convert.ToInt32(e.Value);
        }

        private async Task OnDomainChange(ChangeEventArgs e)
        {
            var domainType = (DomainType)Convert.ToInt32(e.Value);
            selectedDomain = domainType;
            await InitMachineAsync(domainType);
        }

        private async Task InitMachineAsync(DomainType domainType)
        {
            if (isLoading)
                return;

            isLoading = true;
            var response = await HttpService.LockMachineAsync(domainType);
            isLoading = false;

            if (!response.IsSuccess)
                return;

            selectedMachine = response.Content.MachineId;
            availableCosts = response.Content.AvailableCosts;
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
            return selectedDomain switch
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
