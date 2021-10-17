using BH.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace BH.Domain
{
    public class BhDbContext : IdentityDbContext<User>
    {
        public BhDbContext(DbContextOptions<BhDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<BH.Domain.Entities.Domain> Domains { get; set; }

        public DbSet<Machine> Machines { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<TicketHistory> TicketHistories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typeConfigurations = Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var typeConfiguration in typeConfigurations)
            {
                dynamic configuration = Activator.CreateInstance(typeConfiguration);
                modelBuilder.ApplyConfiguration(configuration);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
