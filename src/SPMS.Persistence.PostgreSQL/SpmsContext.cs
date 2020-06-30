using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Services;
using SPMS.Common;
using SPMS.Domain.Common;
using SPMS.Domain.Models;

namespace SPMS.Persistence.PostgreSQL
{
    public class SpmsContext : DbContext, ISpmsContext
    {
        private readonly IUserService _currentUser;
        private readonly IDateTime _dateTime;

        public SpmsContext(DbContextOptions<SpmsContext> options, IUserService user, IDateTime dateTime)
            : base(options)
        {
            _currentUser = user;
            _dateTime = dateTime;
        }
        public SpmsContext(DbContextOptions<SpmsContext> options)
            : base(options)
        {
        }

        public SpmsContext()
        {
            
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUser.GetName();
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUser.GetName();
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpmsContext).Assembly);
        }

        public DbSet<Biography> Biography { get; set; }
        public DbSet<BiographyState> BiographyState { get; set; }
        public DbSet<BiographyStatus> BiographyStatus { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<GameUrl> GameUrl { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<EpisodeStatus> EpisodeStatus { get; set; }
        public DbSet<Episode> Episode { get; set; }
        public DbSet<EpisodeEntry> EpisodeEntry { get; set; }
        public DbSet<EpisodeEntryType> EpisodeEntryType { get; set; }
        public DbSet<Posting> Posting { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<PlayerConnection> PlayerConnection { get; set; }
        public DbSet<PlayerRole> PlayerRole { get; set; }
        public DbSet<EpisodeEntryStatus> EpisodeEntryStatus { get; set; }
    }
}
