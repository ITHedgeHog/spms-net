using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Shouldly;
using SPMS.Application.Character.Command;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Common.Mappings;
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

        public UpdateCharacterCommandTests(UpdateCharacterCommandFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void Updates_Character()
        {
            var request = new UpdateCharacterCommand() { Id = 1, Firstname = "Dean" };
            var sut = new UpdateCharacterCommand.UpdateCharacterHandler(_context, _mapper);

            var result = await sut.Handle(request, CancellationToken.None);

            _context.Biography.First(x => x.Id == request.Id).Firstname.ShouldBe("Dean");
        }

        [Fact]
        public async void Return_False_If_Character_Does_Not_Exist()
        {
            var request = new UpdateCharacterCommand() { Id = 1000, Firstname = "Dean" };
            var sut = new UpdateCharacterCommand.UpdateCharacterHandler(_context, _mapper);

            var result = await sut.Handle(request, CancellationToken.None);

            result.ShouldBeFalse();
        }
    }

    public class UpdateCharacterCommandFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public SpmsContext Context { get; set; }
        public UpdateCharacterCommandFixture()
        {
            Context = SpmsContextFactory.Create();

            Context.Biography.Add(new Domain.Models.Biography() {Firstname = "Dan", Surname = "Taylor"});
            Context.SaveChanges();


            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CharacterProfile>();

            });

            Mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
