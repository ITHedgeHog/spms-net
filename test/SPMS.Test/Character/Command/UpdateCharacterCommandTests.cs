using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Shouldly;
using SPMS.Application.Character.Command;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Common.Mappings;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.PostgreSQL;
using Xunit;
using SpmsContextFactory = SPMS.Application.Tests.Common.SpmsContextFactory;

namespace SPMS.Application.Tests.Character.Command
{
    public class UpdateCharacterCommandTests : IClassFixture<UpdateCharacterCommandFixture>
    {
        private readonly ISpmsContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UpdateCharacterCommandTests(UpdateCharacterCommandFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _userService = fixture.MockUserService.Object;
        }

        [Fact]
        public async void Updates_Character()
        {
            var request = new UpdateCharacterCommand() { Id = 1, Firstname = "Dean" };
            var sut = new UpdateCharacterCommand.UpdateCharacterHandler(_context, _mapper, _userService);

            var result = await sut.Handle(request, CancellationToken.None);

            _context.Biography.First(x => x.Id == request.Id).Firstname.ShouldBe("Dean");
            result.ShouldBe(UpdateCharacterResponse.Updated);
        }

        
        [Fact]
        public async void CreateCharacterIfNotExist()
        {
            
            var numberOfCharactersAtStartOfTest = _context.Biography.Count();
            var request = new UpdateCharacterCommand() { Id = 1000, Firstname = "Weasley", Surname = "Crusher"};
            var sut = new UpdateCharacterCommand.UpdateCharacterHandler(_context, _mapper, _userService);

            var result = await sut.Handle(request, CancellationToken.None);
            var numberOfCharactersAtEndOfTest = _context.Biography.Count();
            numberOfCharactersAtEndOfTest.ShouldBeGreaterThan(numberOfCharactersAtStartOfTest);
            result.ShouldBe(UpdateCharacterResponse.Created);
            var newCharacter = _context.Biography.Last();

            newCharacter.PlayerId.ShouldNotBe(0);
            newCharacter.StateId.ShouldNotBe(0);

        }
    }

    public class UpdateCharacterCommandFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public SpmsContext Context { get; set; }
        public Mock<IUserService> MockUserService { get; set; }
        public UpdateCharacterCommandFixture()
        {
            Context = SpmsContextFactory.Create();

            Context.Biography.Add(new Domain.Models.Biography() {Firstname = "Dan", Surname = "Taylor", State = new BiographyState(){Name = StaticValues.Published, Default = true}, Player = new Player(){ DisplayName = "Dan"}});
            Context.SaveChanges();


            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CharacterMapperProfile>();

            });

            Mapper = configurationProvider.CreateMapper();


            MockUserService = new Mock<IUserService>();
            MockUserService.Setup(x => x.GetId()).Returns(1);
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
