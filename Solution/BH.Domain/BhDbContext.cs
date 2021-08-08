using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class BhDbContext : DbContext
    {
        public BhDbContext(DbContextOptions<BhDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Entities.Domain> Domains { get; set; }

        public DbSet<Machine> Machines { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
