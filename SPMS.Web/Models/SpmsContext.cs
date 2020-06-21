using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SPMS.Web.Models;

namespace SPMS.Web.Models
{
    public class SpmsContext : DbContext
    {
        public SpmsContext(DbContextOptions<SpmsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerRolePlayer>()
                .HasKey(p => new {p.PlayerId, p.PlayerRoleId});
            modelBuilder.Entity<PlayerRolePlayer>()
                .HasOne(p => p.Player)
                .WithMany(p => p.Roles)
                .HasForeignKey(bc => bc.PlayerId);
            modelBuilder.Entity<PlayerRolePlayer>()
                .HasOne(bc => bc.PlayerRole)
                .WithMany(c => c.PlayerRolePlayer)
                .HasForeignKey(bc => bc.PlayerRoleId);



            modelBuilder.Entity<EpisodeEntryPlayer>()
                .HasKey(p => new { p.PlayerId, p.EpisodeEntryId });
            modelBuilder.Entity<EpisodeEntryPlayer>()
                .HasOne(p => p.Player)
                .WithMany(p => p.EpisodeEntries)
                .HasForeignKey(bc => bc.PlayerId);
            modelBuilder.Entity<EpisodeEntryPlayer>()
                .HasOne(bc => bc.EpisodeEntry)
                .WithMany(c => c.EpisodeEntryPlayer)
                .HasForeignKey(bc => bc.EpisodeEntryId);
        }

        public DbSet<SPMS.Web.Models.Biography> Biography { get; set; }
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
        public DbSet<PlayerRole> PlayerRole { get; set; }
        public DbSet<EpisodeEntryStatus> EpisodeEntryStatus { get; set; }
    }
}
