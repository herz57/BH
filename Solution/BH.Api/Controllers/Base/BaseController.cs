using BH.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using BH.Infrastructure.Interfaces;
using BH.Common.Models;
using BH.Common.Dtos;
using System.Net;

namespace BH.Api.Controllers.Base
{
    public abstract class BaseController : ControllerBase
    {
        
        protected readonly ILoggerService _logger;
        protected readonly UserManager<User> _userManager;
        private User _currentUser;

        public User CurrentUser 
        { 
            get => _currentUser ??= _userManager.Users.Include(u => u.Profile).Single(u => u.UserName == User.Identity.Name);
        }

        public BaseController(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            _logger = serviceProvider.GetRequiredService<ILoggerService>();
        }

        protected async Task<ApiResponse> Handle<T>(Func<Task<T>> func,
            string action,
            string entityDiscriminator = null,
            int? entityId = null)
        {
            try
            {
                var result = await func();
                return new ApiResponse<T>(result);
            }
            catch (SqlException ex)
            {
                if (ex.Number < 50000)
                    throw;

                _logger.LogWarning(ex.Message, entityId, entityDiscriminator, CurrentUser.Id);
                return HandleException(ex.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{action} has been failed", ex.Message, entityId, entityDiscriminator, CurrentUser.Id);
                return HandleException("Something went wrong.", HttpStatusCode.InternalServerError);
            }
        }

        private ApiResponse<ErrorDto> HandleException(string msg, HttpStatusCode statusCode)
        {
            Response.StatusCode = (int)statusCode;
            return new ApiResponse<ErrorDto>(false, statusCode, new ErrorDto(msg));
        }
    }
}
