using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.Configuration
{
    public class PlayerRoleConfiguration : IEntityTypeConfiguration<PlayerRole>
    {
        public void Configure(EntityTypeBuilder<PlayerRole> builder)
        {
            builder.HasKey(e => e.Id);

        }
    }
}