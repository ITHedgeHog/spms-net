using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Shouldly;
using SPMS.Application.Authoring.Query;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Common.Mappings;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Player;
using SPMS.Application.Tests.Common;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;
using SPMS.Web.Infrastructure.Services;
using Xunit;

namespace SPMS.Application.Tests.Authoring.Query
{
    public class WritingPortalQueryTests : IClassFixture<WritingPortalQueryFixture>
    {
        private readonly SpmsContext _db;
        private readonly IMapper _mapper;
        private ITenantAccessor<TenantDto> _tenantAccessor;

        public WritingPortalQueryTests(WritingPortalQueryFixture fixture)
        {
            _db = fixture.Context;
            _mapper = fixture.Mapper;
            _tenantAccessor = fixture.TenantAccessor;
        }

        [Fact]
        public async Task ShouldReturnWritingPortalDto()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetId()).Returns(1);
            
            var request = new WritingPortalQuery();
            var sut = new WritingPortalQuery.WritingPortalQueryHandler(_db, mockUserService.Object, _mapper, _tenantAccessor);

            var result = await sut.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<WritingPortalDto>();
            result.DraftPosts.ShouldBeOfType<List<PostDto>>();
            result.DraftPosts.Count.ShouldBe(1);
            result.PendingPosts.ShouldBeOfType<List<PostDto>>();


        }

        [Fact]
        public async Task ShouldAllowNewPost()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetId()).Returns(1);
            var request = new WritingPortalQuery();
            var sut = new WritingPortalQuery.WritingPortalQueryHandler(_db, mockUserService.Object, _mapper, _tenantAccessor);

            var result = await sut.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<WritingPortalDto>();
            result.CanPost.ShouldBeTrue();
        }
    }


    public class WritingPortalQueryFixture : IDisposable
    {
        public SpmsContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public ITenantAccessor<TenantDto> TenantAccessor { get; set; }
        public WritingPortalQueryFixture()
        {
            Context = TestSpmsContextFactory.Create();
            Context.Biography.Add(new Domain.Models.Biography()
            {
                Firstname = "Lionel",
                Surname = "Blair",
                Affiliation = "Starfleet",
                Assignment = "USS Voyager",
                Born = "Earth",
                StateId = Context.BiographyState.First().Id,
                StatusId = Context.BiographyStatus.First().Id,
                TypeId = Context.BiographyTypes.First().Id,
                PlayerId = 1
            });
            Context.SaveChanges();

            Context.EpisodeEntry.Add(new EpisodeEntry()
            {
                EpisodeId = Context.Episode.First().Id,
                Title = "post",
                Content = "Blargh",
                EpisodeEntryPlayer = new Collection<EpisodeEntryPlayer>()
                {
                    new EpisodeEntryPlayer() { PlayerId = 1}
                },
                EpisodeEntryStatusId = Context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Draft).Id,
                EpisodeEntryTypeId = Context.EpisodeEntryType.First().Id
            });
            Context.EpisodeEntry.Add(new EpisodeEntry()
            {
                EpisodeId = Context.Episode.First().Id,
                Title = "post",
                Content = "Blargh",
                EpisodeEntryPlayer = new Collection<EpisodeEntryPlayer>()
                {
                    new EpisodeEntryPlayer() { PlayerId = 2}
                },
                EpisodeEntryStatusId = Context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Draft).Id,
                EpisodeEntryTypeId = Context.EpisodeEntryType.First().Id
            });
            Context.SaveChanges();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<QueryMappings>();

            });

            Mapper = configurationProvider.CreateMapper();

            var mockTenantAccessor = new Mock<ITenantAccessor<TenantDto>>();
            mockTenantAccessor.Setup(x => x.Instance).Returns(new TenantDto(){ Id = 1, GameName = "Test Game"});
            TenantAccessor = mockTenantAccessor.Object;

        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
