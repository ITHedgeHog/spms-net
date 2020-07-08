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
using SPMS.Application.Tests.Mapping;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.PostgreSQL;
using Xunit;
using SpmsContextFactory = SPMS.Application.Tests.Common.SpmsContextFactory;

namespace SPMS.Application.Tests.Story.Query
{
    public class StoryPostQueryTests : IClassFixture<StoryPostQueryFixture>
    {
        private ISpmsContext _db;
        private IMapper _mapper;

        public StoryPostQueryTests(StoryPostQueryFixture fixture)
        {
            _db = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ReturnPostById()
        {
            var key = Sodium.SecretBox.GenerateKey();
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(x => x.GetGameKey(key)).Returns(key);
            var maskingMock = new Mock<IIdentifierMask>();
            maskingMock.Setup(x => x.HideId(1)).Returns("123");
            maskingMock.Setup(x => x.RevealId("123")).Returns(1);
            var masking = maskingMock.Object;
            var id = masking.HideId(1);
            var cmd = new StoryPostQuery() {Id = id};
            var sut = new StoryPostQuery.StoryPostQueryHandler(_db, _mapper, masking);

            var result = await sut.Handle(cmd, CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<StoryPostDto>();
        }


        //[Fact]
        //public async Task ReturnInvalidPostById()
        //{
        //    var cmd = new StoryPostQuery() { Id = 1001 };
        //    var sut = new StoryPostQuery.StoryPostQueryHandler(_db, _mapper);

        //    var result = await sut.Handle(cmd, CancellationToken.None);

        //    result.ShouldBeNull();
        //}
    }


    public class StoryPostQueryFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public SpmsContext Context { get; set; }
        public StoryPostQueryFixture()
        {
            Context = SpmsContextFactory.Create();
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
                EpisodeEntryTypeId =  Context.EpisodeEntryType.First(x => x.Name == StaticValues.Post).Id,
                EpisodeEntryStatusId = Context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Published).Id
            };
            Context.EpisodeEntry.Add(episodeEntry);
            Context.SaveChanges();


            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StoryMapping>();
                //cfg.AddProfile<BiographyMapperProfile>();

            });

            Mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
