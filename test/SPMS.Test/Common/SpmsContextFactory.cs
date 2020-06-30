using System;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.PostgreSQL;

namespace SPMS.Test.Common
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

            var epoisodeStatus = new EpisodeEntryStatus() {Name = StaticValues.Published};
            var episodeStatusDraft = new EpisodeEntryStatus() {Name = StaticValues.Draft};
            var episodeType = new EpisodeEntryType() {Name = StaticValues.Post};
            context.EpisodeEntryStatus.Add(epoisodeStatus);
            context.EpisodeEntryStatus.Add(episodeStatusDraft);
            context.EpisodeEntryType.Add(episodeType);
            context.SaveChanges();

            var game = new Game() { SiteTitle = "Test Game", Description = "The test game", Name = "Test Game", Url = new Collection<GameUrl>(){new GameUrl(){ Url = "localhost"}}};
            context.Game.Add(game);
            var series = new Series() {Game = game, Title = "Series 1"};
            context.Series.Add(series);
            var episode = new Episode()
            {
                Title = "Episode 1",
                Series = series,
                Status = new EpisodeStatus() { Name = StaticValues.Published }
            };
            context.Episode.Add(episode);

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