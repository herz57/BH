using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BH.Domain.Entities;

namespace BH.Domain.EntityConfiguration
{
    class TicketHistoryEntityConfiguration : IEntityTypeConfiguration<TicketHistory>
    {
        public void Configure(EntityTypeBuilder<TicketHistory> builder)
        {
            builder.HasKey(th => new { th.TicketId, th.PlayedOutDate, th.PlayedOutByUserId });

            builder.HasOne(th => th.Ticket)
                .WithMany(t => t.TicketHistories)
                .HasForeignKey(th => th.TicketId);
        }
    }
}
