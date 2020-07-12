using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPMS.Domain.Models;

namespace SPMS.Persistence.Configuration
{
    public class PostingConfiguration : IEntityTypeConfiguration<Posting>
    {
        public void Configure(EntityTypeBuilder<Posting> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn().HasColumnName("Id");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.GameId).IsRequired(false);
            builder.Property(x => x.IsPlayable).HasDefaultValue(false);
        }
    }
}