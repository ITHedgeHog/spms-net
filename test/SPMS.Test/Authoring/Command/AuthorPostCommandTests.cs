using System;
using System.Linq;
using System.Threading;
using MediatR;
using Moq;
using Shouldly;
using SPMS.Application.Authoring.Command.CreatePost;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Application.Tests.Common;
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

            _db.EpisodeEntry.Count().ShouldBe(1);
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
