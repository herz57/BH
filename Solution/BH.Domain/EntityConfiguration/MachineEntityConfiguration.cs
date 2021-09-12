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
            builder.Property(m => m.IsLocked).HasDefaultValueSql("0");

            builder.HasOne(m => m.Domain)
                .WithMany(m => m.Machines)
                .HasForeignKey(t => t.DomainType);
        }
    }
}

