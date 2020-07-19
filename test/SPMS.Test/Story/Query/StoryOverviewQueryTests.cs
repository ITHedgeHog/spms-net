using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NLipsum.Core;
using Shouldly;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Common.Mappings;
using SPMS.Application.Dtos.Story;
using SPMS.Application.Services;
using SPMS.Application.Story.Query;
using SPMS.Application.Tests.Common;
using SPMS.Application.Tests.Mapping;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;
using Xunit;

namespace SPMS.Application.Tests.Story.Query
{
    public class StoryOverviewQueryTests : IClassFixture<StoryOverviewFixture>
    {
        private ISpmsContext _db;
        private IMapper _mapper;

        public StoryOverviewQueryTests(StoryOverviewFixture fixture)
        {
            _db = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ShouldReturnData()
        {
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(x => x.GetGameIdAsync()).ReturnsAsync(1);
            var cmd = new StoryOverviewQuery();
            var sut = new StoryOverviewQuery.StoryOverviewQueryHandler(_db, _mapper, mockGameService.Object);

            var result = await sut.Handle(cmd, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<StoryOverviewDto>();
            result.CurrentSeries.ShouldBeOfType<SeriesDto>();
            result.CurrentSeries.Title.ShouldNotBeNullOrEmpty();
            result.CurrentEpisode.ShouldBeOfType<EpisodeDto>();
            result.CurrentEpisode.Title.ShouldNotBeNullOrEmpty();
        }
    }

    public class StoryOverviewFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public SpmsContext Context { get; set; }
        public StoryOverviewFixture()
        {
            Context = TestSpmsContextFactory.Create();
            var game = Context.Game.First();
            Context.Biography.Add(new Domain.Models.Biography() { Firstname = "Dan", Surname = "Taylor", Player = new Player() { AuthString = "123" }, State = new BiographyState() { Default = false, Name = "State", GameId = game.Id }, Posting = new Posting() { Name = "Starbase Gamma" } });
            Context.SaveChanges();


            var ipsum = new NLipsum.Core.LipsumGenerator();
            var sentences = ipsum.GenerateSentences(3, new Sentence(5, 20));
            var episodeEntry = new EpisodeEntry()
            {
                Title = sentences[0],
                Location = sentences[1],
                Timeline = sentences[2],
                Content = ipsum.GenerateLipsum(5),
                EpisodeId = Context.Episode.First().Id,
                EpisodeEntryTypeId = Context.EpisodeEntryType.First(x => x.Name == StaticValues.Post).Id,
                EpisodeEntryStatusId = Context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Published).Id
            };
            Context.EpisodeEntry.Add(episodeEntry);
            Context.SaveChanges();


            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationMapper>();

            });

            Mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }

    }
}
