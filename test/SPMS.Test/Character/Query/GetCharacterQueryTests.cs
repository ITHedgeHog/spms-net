using System;
using System.Linq;
using System.Threading;
using AutoMapper;
using Moq;
using Shouldly;
using SPMS.Application.Character.Query;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Common.Mappings;
using SPMS.Application.Dtos;
using SPMS.Application.Services;
using SPMS.Domain.Models;
using SPMS.Persistence.PostgreSQL;
using Xunit;
using SpmsContextFactory = SPMS.Application.Tests.Common.SpmsContextFactory;

namespace SPMS.Application.Tests.Character.Query
{
    public class GetCharacterQueryTests : IClassFixture<GetCharacterQueryFixture>
    {
        private readonly ISpmsContext _context;
        private readonly IMapper _mapper;

        public GetCharacterQueryTests(GetCharacterQueryFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void GetCharacterBiography()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(u => u.GetAuthId()).Returns("123");
            mockUserService.Setup(u => u.IsAdmin()).Returns(false);
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(x => x.GetGameIdAsync()).ReturnsAsync(1);
            var request = new GetCharacterQuery() { Id = 1 };
            var sut = new GetCharacterQuery.GetCharacterQueryHandler(_context, _mapper, mockUserService.Object, mockGameService.Object);

            var result = await sut.Handle(request, CancellationToken.None);

            //_context.Biography.First(x => x.Id == request.Id).Firstname.ShouldBe("Dean");

            result.ShouldBeOfType<EditBiographyDto>();
            result.Firstname.ShouldBe("Dan");
            result.Surname.ShouldBe("Taylor");
            result.Types.ShouldNotBeEmpty();
        }

        [Fact]
        public async void ReturnEmptyDtoIfCharacterDoesNotExist()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(u => u.GetAuthId()).Returns("123");
            mockUserService.Setup(u => u.IsAdmin()).Returns(false);
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(x => x.GetGameIdAsync()).ReturnsAsync(1);
            var request = new GetCharacterQuery() { Id = 1001 };
            var sut = new GetCharacterQuery.GetCharacterQueryHandler(_context, _mapper, mockUserService.Object, mockGameService.Object);

            var result = await sut.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<EditBiographyDto>();
            result.Firstname.ShouldBeNullOrEmpty();
            result.Surname.ShouldBeNullOrEmpty();

            result.Types.ShouldNotBeEmpty();
        }

        [Fact]
        public async void ReturnAnyCharacterIfAdmin()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(u => u.GetAuthId()).Returns("562");
            mockUserService.Setup(u => u.IsAdmin()).Returns(true);
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(x => x.GetGameIdAsync()).ReturnsAsync(1);
            var request = new GetCharacterQuery() { Id = 1 };
            var sut = new GetCharacterQuery.GetCharacterQueryHandler(_context, _mapper, mockUserService.Object, mockGameService.Object);

            var result = await sut.Handle(request, CancellationToken.None);

            result.ShouldBeOfType<EditBiographyDto>();
            result.Firstname.ShouldBe("Dan");
            result.Surname.ShouldBe("Taylor");

            result.Types.ShouldNotBeEmpty();
        }

        [Fact]
        public async void ReturnNullIfPlayerDoesNotOwnCharacter()
        {
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(u => u.GetAuthId()).Returns("562");
            mockUserService.Setup(u => u.IsAdmin()).Returns(false);
            var mockGameService = new Mock<IGameService>();
            mockGameService.Setup(x => x.GetGameIdAsync()).ReturnsAsync(1);
            var request = new GetCharacterQuery() { Id = 1 };
            var sut = new GetCharacterQuery.GetCharacterQueryHandler(_context, _mapper, mockUserService.Object, mockGameService.Object);

            var result = await sut.Handle(request, CancellationToken.None);

            result.ShouldBeNull();
        }
    }

    public class GetCharacterQueryFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public SpmsContext Context { get; set; }
        public GetCharacterQueryFixture()
        {
            Context = SpmsContextFactory.Create();
            var game = Context.Game.First();
            Context.Biography.Add(new Domain.Models.Biography() { Firstname = "Dan", Surname = "Taylor", Player = new Player() { AuthString = "123" }, State = new BiographyState() { Default = false, Name = "State", GameId = game.Id }, Posting = new Posting(){Name = "Starbase Gamma"}});
            Context.SaveChanges();


            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CharacterMapperProfile>();
                cfg.AddProfile<BiographyMapperProfile>();

            });

            Mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}