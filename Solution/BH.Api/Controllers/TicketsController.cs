using BH.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace BH.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsService _ticketsService;

        public TicketsController(ITicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }

        //[Authorize]
        [HttpGet("{machineId}")]
        public async Task<IActionResult> GetRandomTicketAsync([FromRoute]int machineId)
        {
            var result = await _ticketsService.GetRandomTicketByMachineIdAsync(machineId);
            return Ok(result);
        }
    }
}
