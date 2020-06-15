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

        public DbSet<SPMS.Web.Models.Biography> Biography { get; set; }
        public DbSet<BiographyStatus> BiographyStatus { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<EpisodeStatus> EpisodeStatus { get; set; }
        public DbSet<Episode> Episode { get; set; }
        public DbSet<EpisodeEntry> EpisodeEntry { get; set; }
        public DbSet<Posting> Posting { get; set; }
        public DbSet<Player> Player { get; set; }

    }
}
