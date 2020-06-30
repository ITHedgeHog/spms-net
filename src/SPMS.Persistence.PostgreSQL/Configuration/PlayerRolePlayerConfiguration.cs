using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.PostgreSQL.Configuration
{
    public class PlayerRolePlayerConfiguration : IEntityTypeConfiguration<PlayerRolePlayer>
    {
        public void Configure(EntityTypeBuilder<PlayerRolePlayer> builder)
        {
            builder.HasKey(e => new { e.PlayerId, e.PlayerRoleId});

            builder.HasOne(p => p.Player).WithMany(p => p.Roles).HasForeignKey(bc => bc.PlayerId);

            builder.HasOne(bc => bc.PlayerRole).WithMany(c => c.PlayerRolePlayer).HasForeignKey(bc => bc.PlayerRoleId);
        }
    }
}