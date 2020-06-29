using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.Configuration
{
    public class EpisodeEntryTypeConfiguration : IEntityTypeConfiguration<EpisodeEntryType>
    {
        public void Configure(EntityTypeBuilder<EpisodeEntryType> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");

            builder.Property(x => x.Name).HasMaxLength(150);
        }
    }
}