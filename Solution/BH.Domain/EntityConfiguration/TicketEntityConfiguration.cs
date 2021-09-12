using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BH.Domain.Entities;

namespace BH.Domain.EntityConfiguration
{
    class TicketEntityConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.TicketId);

            builder.HasOne(t => t.Machine)
                .WithMany(m => m.Tickets)
                .HasForeignKey(t => t.MachineId);
        }
    }
}
