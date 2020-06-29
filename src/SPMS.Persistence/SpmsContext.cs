using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Domain.Models;
using SPMS.Persistence.Migrations;
using EpisodeEntryPlayer = SPMS.Domain.Models.EpisodeEntryPlayer;
using EpisodeEntryStatus = SPMS.Domain.Models.EpisodeEntryStatus;

namespace SPMS.Persistence
{
    public class SpmsContext : DbContext, ISpmsContext
    {
        public SpmsContext(DbContextOptions<SpmsContext> options)
            : base(options)
        {
        }

        public SpmsContext()
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpmsContext).Assembly);
        }

        public DbSet<Biography> Biography { get; set; }
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
