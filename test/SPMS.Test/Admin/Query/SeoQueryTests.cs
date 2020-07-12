using System;
using System.Linq;
using System.Threading;
using AutoMapper;
using Moq;
using Shouldly;
using SPMS.Application.Admin.Query;
using SPMS.Application.Character.Query;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Common.Mappings;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Admin;
using SPMS.Application.Services;
using SPMS.Application.Tests.Character.Query;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;
using Xunit;
using SpmsContextFactory = SPMS.Application.Tests.Common.SpmsContextFactory;

namespace SPMS.Application.Tests.Admin.Query
{
    public class SeoQueryTests : IClassFixture<SeoQueryFixture>
    {
        private readonly ISpmsContext _context;
        private readonly IMapper _mapper;

        public SeoQueryTests(SeoQueryFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void GetSeoStatusQueryTest()
        {
            var request = new SeoQuery() { Url = "localhost" };
            var sut = new SeoQuery.SeoQueryHandler(_context,_mapper);

            var result = await sut.Handle(request, CancellationToken.None);

            //_context.Biography.First(x => x.Id == request.Id).Firstname.ShouldBe("Dean");

            result.ShouldBeOfType<SeoDto>();
            result.Author.ShouldBe("Dan Taylor & Evan Scown");
            result.IsSpiderable.ShouldBeTrue();
            
        }

        
    }

    public class SeoQueryFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public SpmsContext Context { get; set; }
        public SeoQueryFixture()
        {
            Context = SpmsContextFactory.Create();
            
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GenericMapper>();
            });

            Mapper = configurationProvider.CreateMapper();
        }
        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}