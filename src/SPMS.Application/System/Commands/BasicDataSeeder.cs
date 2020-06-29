using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;
using SPMS.Domain.Models;

namespace SPMS.Application.System.Commands
{
    


    public class BasicDataSeeder
    {
        private readonly ISpmsContext _db;

        public BasicDataSeeder(ISpmsContext db)
        {
            _db = db;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (await _db.Player.AnyAsync(cancellationToken))
            {
                return;
            }

            await SeedPlayerRoleAsync(_db, cancellationToken);

            SeedDefaults(_db);
            SeedBtd(_db);
        }

        public async Task SeedPlayerRoleAsync(ISpmsContext db, CancellationToken cancellationToken)
        {
            // Player Role
            if (!db.PlayerRole.Any(r => r.Name == StaticValues.AdminRole))
                await db.PlayerRole.AddAsync(new PlayerRole() { Name = StaticValues.AdminRole }, cancellationToken);
            if (!db.PlayerRole.Any(r => r.Name == StaticValues.PlayerRole))
                await db.PlayerRole.AddAsync(new PlayerRole() { Name = StaticValues.PlayerRole }, cancellationToken);

            await db.SaveChangesAsync(cancellationToken);
        }


        public static void SeedDefaults(ISpmsContext context)
        {
            

            // Biography Status
            if (!context.BiographyStatus.Any(n => n.Name == StaticValues.Draft))
                context.BiographyStatus.Add(new BiographyStatus() { Name = StaticValues.Draft });
            if (!context.BiographyStatus.Any(n => n.Name == StaticValues.Pending))
                context.BiographyStatus.Add(new BiographyStatus() { Name = StaticValues.Pending });
            if (!context.BiographyStatus.Any(n => n.Name == StaticValues.Active))
                context.BiographyStatus.Add(new BiographyStatus() { Name = StaticValues.Active });
            if (!context.BiographyStatus.Any(n => n.Name == StaticValues.Archived))
                context.BiographyStatus.Add(new BiographyStatus() { Name = StaticValues.Archived });

            // Episode Status
            if (!context.EpisodeStatus.Any(n => n.Name == StaticValues.Draft))
                context.EpisodeStatus.Add(new EpisodeStatus() { Name = StaticValues.Draft });
            if (!context.EpisodeStatus.Any(n => n.Name == StaticValues.Pending))
                context.EpisodeStatus.Add(new EpisodeStatus() { Name = StaticValues.Pending });
            if (!context.EpisodeStatus.Any(n => n.Name == StaticValues.Active))
                context.EpisodeStatus.Add(new EpisodeStatus() { Name = StaticValues.Active });
            if (!context.EpisodeStatus.Any(n => n.Name == StaticValues.Archived))
                context.EpisodeStatus.Add(new EpisodeStatus() { Name = StaticValues.Archived });

            // EpisodeEntryType
            if (!context.EpisodeEntryType.Any(e => e.Name == StaticValues.Post))
                context.EpisodeEntryType.Add(new EpisodeEntryType() { Name = StaticValues.Post });
            context.SaveChanges();
            if (!context.EpisodeEntryType.Any(e => e.Name == StaticValues.PersonalLog))
                context.EpisodeEntryType.Add(new EpisodeEntryType() { Name = StaticValues.PersonalLog });
            if (!context.EpisodeEntryType.Any(e => e.Name == StaticValues.Fiction))
                context.EpisodeEntryType.Add(new EpisodeEntryType() { Name = StaticValues.Fiction });

            // Episode Status
            if (!context.EpisodeEntryStatus.Any(n => n.Name == StaticValues.Draft))
                context.EpisodeEntryStatus.Add(new EpisodeEntryStatus() { Name = StaticValues.Draft });
            if (!context.EpisodeEntryStatus.Any(n => n.Name == StaticValues.Pending))
                context.EpisodeEntryStatus.Add(new EpisodeEntryStatus() { Name = StaticValues.Pending });
            if (!context.EpisodeEntryStatus.Any(n => n.Name == StaticValues.Active))
                context.EpisodeEntryStatus.Add(new EpisodeEntryStatus() { Name = StaticValues.Active });
            if (!context.EpisodeEntryStatus.Any(n => n.Name == StaticValues.Archived))
                context.EpisodeEntryStatus.Add(new EpisodeEntryStatus() { Name = StaticValues.Archived });


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

            //context.SaveChanges();
        }

        public static void SeedBtd(ISpmsContext context)
        {
            var btdGame = new Game() { Name = StaticValues.DefaultGameName, Description = "BtD Simulation", SiteTitle = "Beyond the Darkness a Star Trek RPG", Disclaimer = "<p>Star Trek, Star Trek TAS, Star Trek: The Next Generation, Star Trek: Deep Space 9, Star Trek: Voyager, Star Trek Enterprise, and all Star Trek Movies are registered trademarks of Paramount Pictures and their respective owners; no copyright violation is intended or desired.</p><p>All material contained within this site is the property of Dan Taylor, Evan Scown &amp; Beyond the Darkness.</p>", SiteAnalytics = @"<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src='https://www.googletagmanager.com/gtag/js?id=UA-167297746-1'></script>
                <script>
                window.dataLayer = window.dataLayer || [];
                function gtag() { dataLayer.push(arguments); }
            gtag('js', new Date());

            gtag('config', 'UA-167297746-1');
                </script> " };
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
<script async src='https://www.googletagmanager.com/gtag/js?id=UA-167297746-1'></script>
                <script>
                window.dataLayer = window.dataLayer || [];
                function gtag() { dataLayer.push(arguments); }
            gtag('js', new Date());
            gtag('config', 'UA-167297746-1');
                </script> ";

                context.Game.Update(btdGame);
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
                    Assignment = "Starbase Gamma",
                    PostingId = context.Posting.First(p => p.Name == "Starbase Gamma").Id,
                    Rank = "Captain",
                    DateOfBirth = "Sometime in 2351",
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
                    PlayerId = context.Player.First(p => p.DisplayName == "Dan Taylor").Id,
                    StatusId = 3
                });
            context.SaveChanges();

            //var bioCount = context.Biography.Include(x => x.Posting)
            //    .Count(b => b.Posting.Name == "Starbase Gamma");
            //if (bioCount <= 10)
            //{
            //    for (var i = bioCount; i <= 10; i++)
            //    {
            //        var rnd = new Random();
            //        context.Biography.Add(new Biography()
            //        {
            //            Firstname = "Random" + rnd.Next(),
            //            Surname = "Character" + rnd.Next(),
            //            Born = "Earth",
            //            Gender = "Female",
            //            Assignment = "Starbase Gamma",
            //            PostingId = context.Posting.First(p => p.Name == "Starbase Gamma").Id,
            //            Rank = "Admiral",
            //            DateOfBirth = "Sometime in 2332",
            //            PlayerId = context.Player.First(p => p.DisplayName == "Dan Taylor").Id,
            //            StatusId = 3
            //        });
            //    }
            //}
            //context.SaveChanges();

            // Series
            var series = new Series() { Title = "Series 1", GameId = btdGame.Id };
            if (!context.Series.Any(x => x.Title == "Series 1"))
                context.Series.Add(series);

            context.SaveChanges();

            if (!context.Episode.Any(x => x.Title == "Prologue"))
            {
                EpisodeStatus episodeStatus = context.EpisodeStatus.AsNoTracking().First(x => x.Name == StaticValues.Active);
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
