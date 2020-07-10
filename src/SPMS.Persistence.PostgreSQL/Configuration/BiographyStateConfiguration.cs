using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.PostgreSQL.Configuration
{
    public class BiographyStateConfiguration : IEntityTypeConfiguration<BiographyState>
    {
        public void Configure(EntityTypeBuilder<BiographyState> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.GameId).IsRequired(false);
        }
    }
}