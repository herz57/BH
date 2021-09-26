using BH.Api.Controllers.Base;
using BH.Common.Dtos;
using BH.Common.Models;
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
        public async Task<IActionResult> GetRandomTicketAsync([FromRoute]int machineId, [FromQuery] int ticketCost)
        {
            var result = await _ticketsService.GetRandomTicketByMachineIdAsync(CurrentUser.Profile.ProfileId, machineId, ticketCost); 
            return Ok(new ApiResponse<PlayResponseDto>(result));
        }
    }
}
