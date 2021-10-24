using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BH.Domain.Entities;
using BH.Common.Dtos;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BH.Common.Models;
using BH.Api.Controllers.Base;
using System;

namespace BH.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : BaseController
    {
        private readonly SignInManager<User> _signInManager;

        public AccountsController(SignInManager<User> signInManager,
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public Task<ApiResponse> Login([FromBody]LoginDto dto)
        {
            return Handle(
               async () =>
               {
                   if (ModelState.IsValid)
                   {
                       var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, true, false);
                       if (!result.Succeeded)
                       {
                           ModelState.AddModelError(string.Empty, "Wrong login or password.");
                       }
                   }
                   return new ApiResponse();
               },
               nameof(GetUserClaims));
        }

        [Authorize]
        [HttpPost("logout")]
        public Task<ApiResponse> Logout()
        {
            return Handle(
              async () =>
              {
                  await _signInManager.SignOutAsync();
                  return new ApiResponse();
              },
              nameof(GetUserClaims));
        }

        [Authorize]
        [HttpGet("claims")]
        public Task<ApiResponse> GetUserClaims()
        {
            return Handle(
               async () =>
               {
                   var result = GetAllowedUserClaims();
                   return result;
               },
               nameof(GetUserClaims));
        }

        private List<ClaimValue> GetAllowedUserClaims()
        {
            var allowedClaims = new string[] 
            {
                ClaimTypes.Name,
                ClaimTypes.Email,
                ClaimTypes.Role
            };

            return User
                .Claims
                .Where(c => allowedClaims.Contains(c.Type))
                .Select(c => new ClaimValue(c.Type, c.Value))
                .ToList();
        }
    }
}