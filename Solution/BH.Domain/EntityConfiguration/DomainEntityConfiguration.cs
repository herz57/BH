using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BH.Domain.EntityConfiguration
{
    class DomainEntityConfiguration : IEntityTypeConfiguration<Entities.Domain>
    {
        public void Configure(EntityTypeBuilder<Entities.Domain> builder)
        {
            builder.HasKey(m => m.DomainType);
        }
    }
}