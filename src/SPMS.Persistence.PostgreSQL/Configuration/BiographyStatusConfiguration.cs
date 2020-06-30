using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.PostgreSQL.Configuration
{
    public class BiographyStatusConfiguration : IEntityTypeConfiguration<BiographyStatus>
    {
        public void Configure(EntityTypeBuilder<BiographyStatus> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");
        }
    }
}