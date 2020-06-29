using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.Configuration
{
    public class GameUrlConfiguration : IEntityTypeConfiguration<GameUrl>
    {
        public void Configure(EntityTypeBuilder<GameUrl> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");

        }
    }
}