using BH.Api.Controllers.Base;
using BH.Common.Models;
using BH.Domain.Entities;
using BH.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace BH.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : BaseController
    {
        private readonly ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _ticketsService = ticketsService;
        }

        [Authorize]
        [HttpGet("{machineId}")]
        public Task<ApiResponse> PlayAsync([FromRoute]int machineId, [FromQuery] int ticketCost)
        {
            return Handle(
                async () =>
                {
                    var result = await _ticketsService.PlayAsync(CurrentUser, machineId, ticketCost);
                    return result;
                },
                nameof(PlayAsync),
                nameof(Machine),
                machineId);
        }

        [Authorize]
        [HttpGet("statistics")]
        public Task<ApiResponse> GetUsersStatistics([FromQuery] int forDays = 30)
        {
            return Handle(
                async () =>
                {
                    var result = _ticketsService.GetUsersStatistics(forDays);
                    return result;
                },
                nameof(GetUsersStatistics));
        }
    }
}
