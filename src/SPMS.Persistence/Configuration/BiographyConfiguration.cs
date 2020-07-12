using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.Configuration
{
    public class BiographyConfiguration : IEntityTypeConfiguration<Biography>
    {
        public void Configure(EntityTypeBuilder<Biography> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");
            builder.Property(x => x.PlayerId).IsRequired(false);
        }
    }
}
