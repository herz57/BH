using BH.Api.Controllers.Base;
using BH.Common.Enums;
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
        public Task<ApiResponse> LockMachineAsync([FromRoute] DomainType domainType)
        {
            return Handle(
                async () =>
                {
                    var result = await _machineService.LockMachineAsync(CurrentUser.Id, domainType);
                    return result;
                },
                nameof(LockMachineAsync));
        }

        [Authorize]
        [HttpPost("unlock/{machineId}")]
        public Task<ApiResponse> UnlockMachineAsync([FromRoute] int machineId)
        {
            return Handle(
                async () =>
                {
                    await _machineService.UnlockMachineAsync(machineId, CurrentUser.Id);
                    return new ApiResponse();
                },
                nameof(UnlockMachineAsync),
                nameof(Machine),
                machineId);
        }
    }
}
