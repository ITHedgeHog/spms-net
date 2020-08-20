using System;
using System.Linq;
using System.Threading;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using SPMS.Application.Authoring.Command.CreatePost;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Application.Tests.Common;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;
using Xunit;

namespace SPMS.Application.Tests.Authoring.Command
{
    public class AuthorPostCommandTests : IClassFixture<AuthorPostCommandTestsFixture>
    {
        private readonly ISpmsContext _db;
        private readonly ITenantAccessor<TenantDto> _tenant;
        private readonly IUserService _userService;
        public AuthorPostCommandTests(AuthorPostCommandTestsFixture fixture)
        {
            _db = fixture.Context;
            _tenant = fixture.TenantAccessor;
            _userService = fixture.UserService;
        }

        [Fact]
        public async void Should_Create_New_Post()
        {
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreatePost.CreatePostHandler(_db, _tenant,  _userService);

            var result = await sut.Handle(new CreatePost(), CancellationToken.None);

            result.ShouldBe<int>(1);

            _db.EpisodeEntry
                .Include(x => x.Episode)
                .ThenInclude(x => x.Series)
                .Count(x => x.Episode.Series.GameId == _tenant.Instance.Id).ShouldBe(1);
        }

        [Fact]
        public async void ShouldCreateNewPostForGame15()
        {
            var mockTenantAccessor = new Mock<ITenantAccessor<TenantDto>>();
            mockTenantAccessor.Setup(x => x.Instance).Returns(new TenantDto() { Id = 15, GameName = "Test Game" });
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetId()).Returns(1);
            // Add Series && Episode for Game 15
            await _db.Series.AddAsync(new Series() {Title = "Game 15", IsActive = true, GameId = 15});
            await _db.SaveChangesAsync(CancellationToken.None);
            await _db.Episode.AddAsync(new Episode()
            {
                SeriesId = _db.Series.First(x => x.GameId == 15).Id,
                Title = "Episode 1",
                StatusId = _db.EpisodeStatus.First(x => x.Name == StaticValues.Published).Id,
            });
            await _db.SaveChangesAsync(CancellationToken.None);
            
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreatePost.CreatePostHandler(_db, mockTenantAccessor.Object, mockUserService.Object);

            var result = await sut.Handle(new CreatePost(), CancellationToken.None);

            result.ShouldBe<int>(2);

            _db.EpisodeEntry
                .Include(x => x.Episode)
                .ThenInclude(x => x.Series)
                .Count(x => x.Episode.Series.GameId == 15).ShouldBeGreaterThanOrEqualTo(1);

             _db.EpisodeEntry
                .Include(x => x.Episode)
                .ThenInclude(x => x.Series)
                .Include(x => x.EpisodeEntryPlayer)
                .Any(x => x.EpisodeEntryPlayer.Any()).ShouldBeTrue();
        }

    }
    public class AuthorPostCommandTestsFixture : IDisposable
    {
        public SpmsContext Context { get; set; }
        public ITenantAccessor<TenantDto> TenantAccessor { get; set; }
        public IUserService UserService { get; set; }

        public AuthorPostCommandTestsFixture()
        {
            Context = TestSpmsContextFactory.Create();

            var mockTenantAccessor = new Mock<ITenantAccessor<TenantDto>>();
            mockTenantAccessor.Setup(x => x.Instance).Returns(new TenantDto() {Id = 1, GameName = "Test Game"});
            TenantAccessor = mockTenantAccessor.Object;

            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(x => x.GetId()).Returns(1);
            UserService = mockUserService.Object;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
