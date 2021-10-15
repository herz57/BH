using BH.Api.Controllers.Base;
using BH.Common.Dtos;
using BH.Common.Enums;
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
    public class MachinesController : BaseController
    {
        private readonly IMachinesService _machineService;

        public MachinesController(IMachinesService machineService,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _machineService = machineService;
        }

        [Authorize]
        [HttpPost("lock/{domainType}")]
        public async Task<IActionResult> LockMachineAsync([FromRoute] DomainType domainType)
        {
            var result = await _machineService.LockMachineAsync(CurrentUser.Id, domainType);
            return Ok(new ApiResponse<LockMachineDto>(result));
        }

        [Authorize]
        [HttpPost("unlock/{machineId}")]
        public async Task<IActionResult> UnlockMachineAsync([FromRoute] int machineId)
        {
            await _machineService.UnlockMachineAsync(machineId, CurrentUser.Id);
            return Ok(new ApiResponse());
        }
    }
}
