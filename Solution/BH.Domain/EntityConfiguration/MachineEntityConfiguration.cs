using BH.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BH.Domain.EntityConfiguration
{
    class MachineEntityConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.HasKey(m => m.MachineId);

            builder.HasOne(m => m.Domain)
                .WithMany(m => m.Machines)
                .HasForeignKey(t => t.DomainType);

            builder.HasOne(m => m.User)
                .WithOne(m => m.LockedMachine)
                .HasForeignKey<Machine>(p => p.LockedByUserId);
        }
    }
}

