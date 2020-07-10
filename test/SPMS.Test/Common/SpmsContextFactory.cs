using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;

namespace SPMS.Application.Tests.Common
{
    public class SpmsContextFactory
    {
        public static SpmsContext Create()
        {
            var options = new DbContextOptionsBuilder<SpmsContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new SpmsContext(options);

            context.Database.EnsureCreated();

            // Add Seed Data
            var episodeType = new EpisodeEntryType() {Name = StaticValues.Post};
            context.EpisodeEntryStatus.Add(new EpisodeEntryStatus() { Name = StaticValues.Published });
            context.EpisodeEntryStatus.Add(new EpisodeEntryStatus() { Name = StaticValues.Draft });
            context.EpisodeEntryStatus.Add(new EpisodeEntryStatus() {Name = StaticValues.Archived});
            context.EpisodeEntryStatus.Add(new EpisodeEntryStatus() { Name = StaticValues.Pending });
            context.EpisodeEntryType.Add(episodeType);
            context.EpisodeEntryType.Add(new EpisodeEntryType() { Name = StaticValues.Fiction});
            context.EpisodeEntryType.Add(new EpisodeEntryType() { Name = StaticValues.PersonalLog });
            context.SaveChanges();

            var game = new Game() { SiteTitle = "Test Game", Description = "The test game", Name = "Test Game", Url = new Collection<GameUrl>(){new GameUrl(){ Url = "localhost"}}};
            context.Game.Add(game);
            var series = new Series() {Game = game, Title = "Series 1", IsActive = true};
            context.Series.Add(series);
            var episode = new Episode()
            {
                Title = "Episode 1",
                Series = series,
                Status = new EpisodeStatus() { Name = StaticValues.Published }
            };
            context.Episode.Add(episode);

            // Biography Lookups 
            context.BiographyTypes.Add(new BiographyType() {Name = StaticValues.BioTypePlayer, Default = true, GameId = game.Id});
            context.BiographyTypes.Add(new BiographyType() { Name = StaticValues.BioTypeNpc, Default = false, GameId = game.Id });
            context.BiographyTypes.Add(new BiographyType() { Name = StaticValues.BioTypePoc, Default = false, GameId = game.Id });

            context.SaveChanges();


            return context;
        }

        public static void Destroy(SpmsContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}