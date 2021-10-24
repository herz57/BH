using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BH.Domain.Entities;

namespace BH.Domain.EntityConfiguration
{
    class LogEntityConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.HasKey(e => e.LogId);
            builder.Property(e => e.Date).HasDefaultValueSql("getutcdate()");
            builder.HasIndex(e => e.EntityDiscriminator);
        }
    }
}

