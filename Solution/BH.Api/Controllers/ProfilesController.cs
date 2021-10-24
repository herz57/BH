using BH.Api.Controllers.Base;
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
    public class ProfilesController : BaseController
    {
        private readonly IProfilesService _profilesService;

        public ProfilesController(IProfilesService profilesService,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _profilesService = profilesService;
        }

        [Authorize]
        [HttpGet]
        public Task<ApiResponse> GetBalanceAsync()
        {
            return Handle(
                async () =>
                {
                    var result = await _profilesService.GetBalanceAsync(CurrentUser.Profile.ProfileId);
                    return result;
                },
                nameof(GetBalanceAsync));
        }
    }
}

