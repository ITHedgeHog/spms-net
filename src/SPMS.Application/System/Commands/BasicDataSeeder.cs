﻿using System;
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

            await SeedBeyondTheDarknessAsync(_db, cancellationToken);
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

        public async Task SeedBeyondTheDarknessAsync(ISpmsContext db, CancellationToken cancellationToken)
        {
            var btdGame = new Game() { Name = StaticValues.DefaultGameName, Description = "BtD Simulation", SiteTitle = "Beyond the Darkness a Star Trek RPG", Disclaimer = "<p>Star Trek, Star Trek TAS, Star Trek: The Next Generation, Star Trek: Deep Space 9, Star Trek: Voyager, Star Trek Enterprise, and all Star Trek Movies are registered trademarks of Paramount Pictures and their respective owners; no copyright violation is intended or desired.</p><p>All material contained within this site is the property of Dan Taylor, Evan Scown &amp; Beyond the Darkness.</p>", SiteAnalytics = @"<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src='https://www.googletagmanager.com/gtag/js?id=UA-167297746-1'></script>
                <script>
                window.dataLayer = window.dataLayer || [];
                function gtag() { dataLayer.push(arguments); }
            gtag('js', new Date());

            gtag('config', 'UA-167297746-1');
                </script> " };
            if (!db.Game.Any(g => g.Name == StaticValues.DefaultGameName))
            {
                await db.Game.AddAsync(btdGame, cancellationToken);

                await db.SaveChangesAsync(cancellationToken);

                // Add Game URL's

                await db.GameUrl.AddAsync(new GameUrl() { GameId = btdGame.Id, Url = "www.beyond-the-darkness.com" }, cancellationToken);
                await db.GameUrl.AddAsync(new GameUrl() { GameId = btdGame.Id, Url = "spms0.beyond-the-darkness.com" }, cancellationToken);
                await db.GameUrl.AddAsync(new GameUrl() { GameId = btdGame.Id, Url = "btd.beyond-the-darkness.com" }, cancellationToken);
            }
            else
            {
                btdGame = await db.Game.FirstAsync(g => g.Name == StaticValues.DefaultGameName, cancellationToken: cancellationToken);

                // Ensure Analyitics is set.

                btdGame.SiteAnalytics = @"<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src='https://www.googletagmanager.com/gtag/js?id=UA-167297746-1'></script>
                <script>
                window.dataLayer = window.dataLayer || [];
                function gtag() { dataLayer.push(arguments); }
            gtag('js', new Date());
            gtag('config', 'UA-167297746-1');
                </script> ";

                db.Game.Update(btdGame);
                
            }

            await db.SaveChangesAsync(cancellationToken);


            // Add Biographies

            // Character
            if (!await db.Biography.AnyAsync(b => b.Firstname == "Marcus" && b.Surname == "Brightstar", cancellationToken: cancellationToken))
                await db.Biography.AddAsync(new Biography()
                {
                    Firstname = "Marcus",
                    Surname = "Brightstar",
                    Born = "Earth",
                    Gender = "Male",
                    Assignment = "Starbase Gamma",
                    PostingId = db.Posting.First(p => p.Name == "Starbase Gamma").Id,
                    Rank = "Captain",
                    DateOfBirth = "Sometime in 2351",
                    PlayerId = db.Player.First(p => p.DisplayName == "Dan Taylor").Id,
                    StatusId = 3
                }, cancellationToken);
            if (!db.Biography.Any(b => b.Firstname == "Jessica" && b.Surname == "Darkly"))
                await db.Biography.AddAsync(new Biography()
                {
                    Firstname = "Jessica",
                    Surname = "Darkly",
                    Born = "Earth",
                    Gender = "Female",
                    Assignment = "Starbase Gamma",
                    PostingId = db.Posting.First(p => p.Name == "Starbase Gamma").Id,
                    Rank = "Admiral",
                    DateOfBirth = "Sometime in 2332",
                    PlayerId = db.Player.First(p => p.DisplayName == "Dan Taylor").Id,
                    StatusId = 3
                }, cancellationToken);
            if (!db.Biography.Any(b => b.Firstname == "Nigel" && b.Surname == "Adisa"))
                await db.Biography.AddAsync(new Biography()
                {
                    Firstname = "Nigel",
                    Surname = "Adisa",
                    Born = "Earth",
                    Gender = "Male",
                    Assignment = "Starbase Gamma",
                    PostingId = db.Posting.First(p => p.Name == "Starbase Gamma").Id,
                    Rank = "Lieutenant",
                    DateOfBirth = "",
                    PlayerId = db.Player.First(p => p.DisplayName == "Dan Taylor").Id,
                    StatusId = 3,
                    History = @"
        A black male of African/British decent, 6'0 in height and weighing in at 196 pounds. He has short cropped black hair and usually wears a short beard.

General Overview		Doctor Adisa, a specialist in neurology possesses a seemingly easygoing manner which he generally uses to mask his borderline OCD issues. He is very witty however his humor can sometimes become overly Sharp. His hobbies include playing various jazz instruments, long distance running, chess, and baking.



Personal History		graduated from Oxford University, attended st. George's medical University and graduated in the top 5% of his class.

Married at the age of 22, and divorced at the age of 30, the union producing one child, a daughter.

He enrolled in Starfleet directly after graduating the University against his father's wishes. who vehemently opposes Starfleet policies."
                }, cancellationToken);

            if (!db.Biography.Any(b => b.Firstname == "Vars" && b.Surname == "Qiratt"))
                await db.Biography.AddAsync(new Biography()
                {
                    Firstname = "Vars",
                    Surname = "Qirat",
                    Born = "Bolia",
                    Species = "Bolian",
                    Gender = "Male",
                    Assignment = "",
                    PostingId = db.Posting.First(p => p.Name == "Starbase Gamma").Id,
                    Rank = "Lieutenant Commander",
                    DateOfBirth = "",
                    PlayerId = db.Player.First(p => p.DisplayName == "Dan Taylor").Id,
                    StatusId = 3,
                    History = @"Vars is best described as carrying extra weight. A rotund Bolian Male with puffy facial features and a noticeable double chin. His height is on the slightly shorter side, coming in at around 5\'9

                    Vars is a vibrant individual living up to the term, \'Jolly Fat Man\'. He has a distinctive laugh that can be considered quite obnoxious, not helped by his flavorful personality. Wearing his emotions like a badge on his sleeve, Vars rarely shy\'s away from expressing his opinion. To the same extent, a withdrawn Vars is often the sign of an insecurity or fear.\n\nHe keeps with him a Hair piece that he wears on \'special occasions\' or on Thursdays. Part of his quirky nature.

                    2383 - Current - USS Ronald Reagan - Chief of Operations(Lt Cmdr)\n2383(Late) - USS Ronald Reagan - Acting Chief of Operations(Lt Cmdr)\n2383(Early) - USS Ronald Reagan - Assistant Chief of Engineering / Acting Chief of Operations(Lt)\n2380 - 2382 - USS Midway - Assistant Chief of Engineering(Lt)\n2379 - USS Midway - Supervising Engineering Officer(Lt JG)\n2376 - 2378 - Starbase 386 - Star Ship Maintenance Engineering Team Lead(Lt JG)\n2374 - 2375 - USS Gettysburg - Engineering Officer / Trainee Supervisor(Lt JG)\n2372 - 2373 - USS Melbourne - Engineering Officer(Ens / Lt JG)\n2368 - 2371 - USS Galloway - Engineering Officer(Ens)\n2363 - 2367 - Starfleet Academy - Officer Cadet[Engineering Stream]"
                }, cancellationToken);


            // Series
            var series = new Series() { Title = "Series 1", GameId = btdGame.Id };
            if (!db.Series.Any(x => x.Title == "Series 1"))
            {
                await db.Series.AddAsync(series, cancellationToken);

                await db.SaveChangesAsync(cancellationToken);
            }
            else
            {
                series = await _db.Series.FirstAsync(x => x.Title == "Series 1" && x.GameId == btdGame.Id,
                    cancellationToken: cancellationToken);
            }

            if (!await db.Episode.AnyAsync(x => x.Title == "Prologue", cancellationToken: cancellationToken))
            {
                EpisodeStatus episodeStatus = await db.EpisodeStatus.AsNoTracking().FirstAsync(x => x.Name == StaticValues.Active, cancellationToken: cancellationToken);
                await db.Episode.AddAsync(new Episode()
                {
                    Title = "Prologue",
                    SeriesId = series.Id,
                    StatusId = episodeStatus.Id
                }, cancellationToken);

                await db.SaveChangesAsync(cancellationToken);
            }


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




            var aquaGame = new Game() { Name = "USS Aqua", Description = "Aqua  Simulation", SiteTitle = "Beyond the Darkness a Star Trek RPG", Disclaimer = "<p>Star Trek, Star Trek TAS, Star Trek: The Next Generation, Star Trek: Deep Space 9, Star Trek: Voyager, Star Trek Enterprise, and all Star Trek Movies are registered trademarks of Paramount Pictures and their respective owners; no copyright violation is intended or desired.</p><p>All material contained within this site is the property of Dan Taylor, Evan Scown &amp; Beyond the Darkness.</p>" };
            if (!context.Game.Any(g => g.Name == "USS Aqua"))
            {
                context.Game.Add(aquaGame);

                context.SaveChanges();

                // Add Game URL's

                context.GameUrl.Add(new GameUrl() { GameId = aquaGame.Id, Url = "aqua.beyond-the-darkness.com" });
                context.GameUrl.Add(new GameUrl() { GameId = aquaGame.Id, Url = "spms1.beyond-the-darkness.com" });
            }


            
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
        }
    }
}