using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SPMS.Web.Service;

namespace SPMS.Web.Models
{
    public static class Seed
    {
        public static void SeedDefaults(SpmsContext context)
        {
            // Player Role
            if (!context.PlayerRole.Any(r => r.Name == StaticValues.AdminRole))
                context.PlayerRole.Add(new PlayerRole() { Name = StaticValues.AdminRole });
            if (!context.PlayerRole.Any(r => r.Name == StaticValues.PlayerRole))
                context.PlayerRole.Add(new PlayerRole() { Name = StaticValues.PlayerRole });

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
                context.EpisodeStatus.Add(new EpisodeStatus() { Name = "Pending" });
            if (!context.EpisodeStatus.Any(n => n.Name == "Active"))
                context.EpisodeStatus.Add(new EpisodeStatus() { Name = "Active" });
            if (!context.EpisodeStatus.Any(n => n.Name == "Archived"))
                context.EpisodeStatus.Add(new EpisodeStatus() { Name = "Archived" });

            // EpisodeEntryType
            if (!context.EpisodeEntryType.Any(e => e.Name == StaticValues.Post))
                context.EpisodeEntryType.Add(new EpisodeEntryType() { Name = StaticValues.Post });
            if (!context.EpisodeEntryType.Any(e => e.Name == StaticValues.PersonalLog))
                context.EpisodeEntryType.Add(new EpisodeEntryType() { Name = StaticValues.PersonalLog });
            if (!context.EpisodeEntryType.Any(e => e.Name == StaticValues.Fiction))
                context.EpisodeEntryType.Add(new EpisodeEntryType() { Name = StaticValues.Fiction });

            // Posting
            if (!context.Posting.Any(p => p.Name == "Undefined"))
                context.Posting.Add(new Posting() { Name = "Undefined" });
            if (!context.Posting.Any(p => p.Name == "Starbase Gamma"))
                context.Posting.Add(new Posting() { Name = "Starbase Gamma" });
            if (!context.Posting.Any(p => p.Name == "USS Sovereign"))
                context.Posting.Add(new Posting() { Name = "USS Sovereign" });

            // Players
            if (!context.Player.Any(p => p.DisplayName == "Dan Taylor"))
            {
                var danPlayer = new Player()
                { AuthString = "auth0|5ed6862a0889640b8ab94b9f", DisplayName = "Dan Taylor" };
                context.Player.Add(danPlayer);
                context.SaveChanges();
            }

            var roles = context.PlayerRole.ToList();
            var dan = context.Player.Include(p => p.Roles).First(x => x.DisplayName == "Dan Taylor");
            foreach (var role in roles.Where(role => dan.Roles.All(r => r.PlayerRoleId != role.Id)))
            {
                dan.Roles.Add(new PlayerRolePlayer() { PlayerId = dan.Id, PlayerRoleId = role.Id });
            }

            context.SaveChanges();


            if (!context.Player.Any(p => p.DisplayName == "Test Monkey"))
                context.Player.Add(new Player() { AuthString = "google-oauth2|112524236910874285641", DisplayName = "Test Monkey" });
            foreach (var role in roles.Where(role => dan.Roles.All(r => r.PlayerRoleId != role.Id) && role.Name == StaticValues.PlayerRole))
            {
                dan.Roles.Add(new PlayerRolePlayer() { PlayerId = dan.Id, PlayerRoleId = role.Id });
            }

            context.SaveChanges();
        }

        public static void SeedBtd(SpmsContext context)
        {
            var btdGame = new Game() { Name = StaticValues.DefaultGameName, Description = "BtD Simulation", SiteTitle = "Beyond the Darkness a Star Trek RPG", Disclaimer = "<p>Star Trek, Star Trek TAS, Star Trek: The Next Generation, Star Trek: Deep Space 9, Star Trek: Voyager, Star Trek Enterprise, and all Star Trek Movies are registered trademarks of Paramount Pictures and their respective owners; no copyright violation is intended or desired.</p><p>All material contained within this site is the property of Dan Taylor, Evan Scown &amp; Beyond the Darkness.</p>", SiteAnalytics = @"<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src='://www.googletagmanager.com/gtag/js?id=UA-167297746-1'></script>
                <script>
                window.dataLayer = window.dataLayer || [];
                function gtag() { dataLayer.push(arguments); }
            gtag('js', new Date());

            gtag('config', 'UA-167297746-1');
                </script> "};
            if (!context.Game.Any(g => g.Name == StaticValues.DefaultGameName))
            {
                context.Game.Add(btdGame);

                context.SaveChanges();

                // Add Game URL's

                context.GameUrl.Add(new GameUrl() { GameId = btdGame.Id, Url = "www.beyond-the-darkness.com" });
                context.GameUrl.Add(new GameUrl() { GameId = btdGame.Id, Url = "spms0.beyond-the-darkness.com" });
                context.GameUrl.Add(new GameUrl() { GameId = btdGame.Id, Url = "btd.beyond-the-darkness.com" });
            }
            else
            {
                btdGame = context.Game.First(g => g.Name == StaticValues.DefaultGameName);

                // Ensure Analyitics is set.

                btdGame.SiteAnalytics = @"<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src='://www.googletagmanager.com/gtag/js?id=UA-167297746-1'></script>
                <script>
                window.dataLayer = window.dataLayer || [];
                function gtag() { dataLayer.push(arguments); }
            gtag('js', new Date());
            gtag('config', 'UA-167297746-1');
                </script> ";

                context.Update(btdGame);
                context.SaveChanges();
            }



            var aquaGame = new Game() { Name = "USS Aqua", Description = "Aqua  Simulation", SiteTitle = "Beyond the Darkness a Star Trek RPG", Disclaimer = "<p>Star Trek, Star Trek TAS, Star Trek: The Next Generation, Star Trek: Deep Space 9, Star Trek: Voyager, Star Trek Enterprise, and all Star Trek Movies are registered trademarks of Paramount Pictures and their respective owners; no copyright violation is intended or desired.</p><p>All material contained within this site is the property of Dan Taylor, Evan Scown &amp; Beyond the Darkness.</p>" };
            if (!context.Game.Any(g => g.Name == "USS Aqua"))
            {
                context.Game.Add(aquaGame);

                context.SaveChanges();

                // Add Game URL's

                context.GameUrl.Add(new GameUrl() { GameId = aquaGame.Id, Url = "aqua.beyond-the-darkness.com" });
                context.GameUrl.Add(new GameUrl() { GameId = aquaGame.Id, Url = "spms1.beyond-the-darkness.com" });
            }


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
                    PlayerId = context.Player.First(p => p.DisplayName == "Dan Taylor").Id,
                    StatusId = 3
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
                    PlayerId = context.Player.First(p => p.DisplayName == "Dan Taylor").Id,
                    StatusId = 3
                });

            context.SaveChanges();

            // Series
            var series = new Series() { Title = "Series 1", GameId = btdGame.Id };
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
