using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BH.Domain.Entities;
using BH.Common.Dtos;

namespace BH.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, true, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Wrong login or password.");
                }
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}