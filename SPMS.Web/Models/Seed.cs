using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SPMS.Web.Models
{
    public static class Seed
    {
        public static void SeedDefaults(SpmsContext context)
        {
            // Biography Status
            if (!context.BiographyStatus.Any(n => n.Name == "Draft"))
                context.BiographyStatus.Add(new BiographyStatus() { Name = "Draft" });
            if (!context.BiographyStatus.Any(n => n.Name == "Pending"))
                context.BiographyStatus.Add(new BiographyStatus() { Name = "Pending" });
            if (!context.BiographyStatus.Any(n => n.Name == "Active"))
                context.BiographyStatus.Add(new BiographyStatus() { Name = "Active" });
            if (!context.BiographyStatus.Any(n => n.Name == "Archived"))
                context.BiographyStatus.Add(new BiographyStatus() { Name = "Archived" });

            // Episode Status
            if (!context.EpisodeStatus.Any(n => n.Name == "Draft"))
                context.EpisodeStatus.Add(new EpisodeStatus() { Name = "Draft" });
            if (!context.EpisodeStatus.Any(n => n.Name == "Pending"))
                context.EpisodeStatus.Add(new EpisodeStatus() { Name =  "Pending" });
            if (!context.EpisodeStatus.Any(n => n.Name == "Active"))
                context.EpisodeStatus.Add(new EpisodeStatus() { Name = "Active" });
            if (!context.EpisodeStatus.Any(n => n.Name == "Archived"))
                context.EpisodeStatus.Add(new EpisodeStatus() { Name = "Archived" });

            // Posting
            if (!context.Posting.Any(p => p.Name == "Undefined"))
                context.Posting.Add(new Posting() {Name = "Undefined"});
            if (!context.Posting.Any(p => p.Name == "Starbase Gamma"))
                context.Posting.Add(new Posting() { Name = "Starbase Gamma" });
            if (!context.Posting.Any(p => p.Name == "USS Sovereign"))
                context.Posting.Add(new Posting() { Name = "USS Sovereign" });
            
            // Players
            if (!context.Player.Any(p => p.DisplayName == "Dan Taylor"))
                context.Player.Add(new Player() {AuthString = "auth0|5ed6862a0889640b8ab94b9f", DisplayName = "Dan Taylor"});
            if (!context.Player.Any(p => p.DisplayName == "Test Monkey"))
                context.Player.Add(new Player() { AuthString = "google-oauth2|112524236910874285641", DisplayName = "Test Monkey" });


            context.SaveChanges();
        }

        public static void SeedDevelopment(SpmsContext context)
        {
            var btdGame = new Game() {Name = "Beyond the Darkness", Description = "BtD Simulation"};
            if (!context.Game.Any(g => g.Name == "Beyond the Darkness"))
                context.Game.Add(btdGame);

            context.SaveChanges();
            
            // Character
            if (!context.Biography.Any(b => b.Firstname == "Marcus" && b.Surname == "Brightstar"))
                context.Biography.Add(new Biography()
                {
                    Firstname = "Marcus",
                    Surname = "Brightstar",
                    Born = "Earth",
                    Gender = "Male",
                    Assignment = "USS Sovereign",
                    PostingId = context.Posting.First(p => p.Name == "USS Sovereign").Id,
                    Rank = "Captain",
                    DateOfBirth = "Sometime in 2351",
                    Owner = "auth0|5ed6862a0889640b8ab94b9f",
                    PlayerId = context.Player.First(p => p.DisplayName == "Dan Taylor").Id
                });
            if (!context.Biography.Any(b => b.Firstname == "Jessica" && b.Surname == "Darkly"))
                context.Biography.Add(new Biography()
                {
                    Firstname = "Jessica",
                    Surname = "Darkly",
                    Born = "Earth",
                    Gender = "Female",
                    Assignment = "Starbase Gamma",
                    PostingId = context.Posting.First(p => p.Name == "Starbase Gamma").Id,
                    Rank = "Admiral",
                    DateOfBirth = "Sometime in 2332",
                    Owner = "auth0|5ed6862a0889640b8ab94b9f",
                    PlayerId = context.Player.First(p => p.DisplayName == "Dan Taylor").Id
                });

            context.SaveChanges();

            // Series
            var series = new Series() {Title = "Series 1", GameId = btdGame.Id};
            if (!context.Series.Any(x => x.Title == "Series 1"))
                context.Series.Add(series);

            context.SaveChanges();

            if (!context.Episode.Any(x => x.Title == "Prologue"))
            {
                EpisodeStatus episodeStatus = context.EpisodeStatus.AsNoTracking().First(x => x.Name == "Active");
                context.Episode.Add(new Episode()
                {
                    Title = "Prologue",
                    SeriesId = series.Id,
                    StatusId = episodeStatus.Id
                });
            }

            context.SaveChanges();
        }
    }
}
