using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntityConfiguration
{
    class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder
                .HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .UsingEntity(j => j.ToTable("UsersRoles"));
        }
    }
}

