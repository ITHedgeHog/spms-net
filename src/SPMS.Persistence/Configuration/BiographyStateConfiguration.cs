using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.Configuration
{
    public class BiographyStateConfiguration : IEntityTypeConfiguration<BiographyState>
    {
        public void Configure(EntityTypeBuilder<BiographyState> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");
        }
    }
}