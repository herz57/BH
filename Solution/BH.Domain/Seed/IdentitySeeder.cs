﻿using BH.Common.Consts;
using BH.Domain.Entities;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BH.Domain.Seed
{
    public class IdentitySeeder
    {
        private readonly BhDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeeder(IServiceScope serviceScope)
        {
            _context = serviceScope.ServiceProvider.GetService<BhDbContext>();
            _userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
            _roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
        }

        public async Task SeedAsync()
        {
            if (await _context.Users.AnyAsync())
                return;

            using (var transaction = _context.Database.BeginTransaction())
            {
                var users = await CreateUsersAsync();
                await CreateRolesAsync();
                await AsignRolesToUsersAsync(users);
                await CreateProfilesAsync(users);
                await transaction.CommitAsync();
            }
        }
        private async Task<List<User>> CreateUsersAsync()
        {
            var users = ComposeUsers();
            var password = "Pass123#";

            foreach (var user in users)
            {
                await _userManager.CreateAsync(user, password);
            }

           return await _context.Users.ToListAsync();
        }

        private async Task CreateRolesAsync()
        {
            var roles = ComposeRoles();

            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(role);
            }
        }

        private async Task AsignRolesToUsersAsync(List<User> users)
        {
            var user1 = users.Single(u => u.UserName == "vasya1");
            await _userManager.AddToRoleAsync(user1, Consts.Roles.User);

            var user2 = users.Single(u => u.UserName == "tolya2");
            await _userManager.AddToRoleAsync(user2, Consts.Roles.Admin);
        }

        private async Task CreateProfilesAsync(List<User> users)
        {
            var profiles = ComposeProfiles(users);
            await _context.Profiles.AddRangeAsync(profiles);
        }

        #region Composers

        private ICollection<User> ComposeUsers()
        {
            return new List<User>
            {
                new User 
                { 
                    UserName = "vasya1",
                    Email = "vasyapupkin@gmail.com"
                },
                new User
                {
                    UserName = "tolya2",
                    Email = "tolyapipkin@gmail.com"
                }
            };
        }

        private ICollection<IdentityRole> ComposeRoles()
        {
            return new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = Consts.Roles.Admin
                },
                new IdentityRole
                {
                    Name = Consts.Roles.User
                }
            };
        }

        private ICollection<Profile> ComposeProfiles(List<User> users)
        {
            var profiles = new List<Profile>();

            foreach (var user in users)
            {
                profiles.Add(new Profile
                {
                    UserId = user.Id
                });
            }

            return profiles;
        }

        #endregion
    }
}