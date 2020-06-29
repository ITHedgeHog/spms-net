using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.PostgreSQL.Configuration
{
    public class EpisodeEntryPlayerConfiguration : IEntityTypeConfiguration<EpisodeEntryPlayer>
    {
        public void Configure(EntityTypeBuilder<EpisodeEntryPlayer> builder)
        {
            builder.HasKey(p => new { p.PlayerId, p.EpisodeEntryId });
            builder.HasOne(p => p.Player)
                .WithMany(p => p.EpisodeEntries)
                .HasForeignKey(bc => bc.PlayerId);
            builder.HasOne(bc => bc.EpisodeEntry)
                .WithMany(c => c.EpisodeEntryPlayer)
                .HasForeignKey(bc => bc.EpisodeEntryId);
        }
    }
}