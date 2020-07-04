using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.PostgreSQL.Configuration
{
    public class BiographyTypeConfiguration : IEntityTypeConfiguration<BiographyType>
    {
        public void Configure(EntityTypeBuilder<BiographyType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.GameId).IsRequired(false);
        }
    }
}