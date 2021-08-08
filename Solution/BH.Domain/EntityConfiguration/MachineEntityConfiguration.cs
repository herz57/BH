using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.EntityConfiguration
{
    class MachineEntityConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.HasKey(m => m.MachineId);

            builder.HasOne(m => m.Domain)
                .WithMany(m => m.Machines)
                .HasForeignKey(t => t.DomainType);
        }
    }
}

