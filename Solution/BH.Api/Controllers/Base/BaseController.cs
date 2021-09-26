using BH.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BH.Api.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly UserManager<User> _userManager;
        private User _currentUser;

        public User CurrentUser 
        { 
            get => _currentUser ??= _userManager.Users.Include(u => u.Profile).Single(u => u.UserName == User.Identity.Name);
        }

        public BaseController(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        }
    }
}
