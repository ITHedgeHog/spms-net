using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Interfaces
{
    public interface ISpmsContext
    {
        DbSet<Domain.Models.Biography> Biography { get; set; }
        DbSet<BiographyState> BiographyState { get; set; }
        DbSet<BiographyStatus> BiographyStatus { get; set; }
        DbSet<BiographyType> BiographyTypes { get; set; }
        DbSet<Game> Game { get; set; }
        DbSet<GameUrl> GameUrl { get; set; }
        DbSet<Series> Series { get; set; }
        DbSet<EpisodeStatus> EpisodeStatus { get; set; }
        DbSet<Episode> Episode { get; set; }
        DbSet<EpisodeEntry> EpisodeEntry { get; set; }
        DbSet<EpisodeEntryType> EpisodeEntryType { get; set; }
        DbSet<Posting> Posting { get; set; }
        DbSet<Player> Player { get; set; }
        DbSet<PlayerConnection> PlayerConnection { get; set; }
        DbSet<PlayerRole> PlayerRole { get; set; }
        DbSet<EpisodeEntryStatus> EpisodeEntryStatus { get; set; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        [Obsolete("Should be writing async code peeps")]
        int SaveChanges();

    }
}