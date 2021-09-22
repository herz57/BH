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

        private TicketDto ticket;
        private List<List<string>> symbolsPathes;

        private bool isLoading;
        private DomainType selectedDomain;
        private bool[] showSymbols = new bool[3];

        //protected override async Task OnInitializedAsync()
        //{
        //}

        private async Task OnPlayClick()
        {
            isLoading = true;
            ResetShowSymbolsFlags();
            selectedDomain = DomainType.Third;

            var response = await HttpService.GetTicketAsync(1);
            ticket = response.Entity;

            SetSymbolsInfo(ticket.Symbols);
            HandleSymbolsShowingTimeout();
            isLoading = false;
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
