using System;
using System.Linq;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
using SPMS.Application.Admin.Query;
using SPMS.Application.Character.Query;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Common.Mappings;
using SPMS.Application.Common.Provider;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Admin;
using SPMS.Application.Services;
using SPMS.Application.Tests.Character.Query;
using SPMS.Application.Tests.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;
using Xunit;

namespace SPMS.Application.Tests.Admin.Query
{
    public class SeoQueryTests : IClassFixture<SeoQueryFixture>
    {
        private readonly ISpmsContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public SeoQueryTests(SeoQueryFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _httpContext = fixture.HttpContext;
        }

        [Fact]
        public async void GetSeoStatusQueryTest()
        {
            var accessor = new Mock<ITenantAccessor<TenantDto>>();
            accessor.Setup(x => x.Instance).Returns(new TenantDto()
            {
                Id = 1,
                SiteTitle = "Test Site",
                Author = "Dan Taylor & Evan Scown",
                IsSpiderable = true
            });
            var request = new SeoQuery() { Url = "localhost" };
            var sut = new SeoQuery.SeoQueryHandler(_context,_mapper,accessor.Object);

            var result = await sut.Handle(request, CancellationToken.None);


            result.ShouldBeOfType<SeoDto>();
            result.Author.ShouldBe("Dan Taylor & Evan Scown");
            result.IsSpiderable.ShouldBeTrue();
            
        }

        
    }

    public class SeoQueryFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public SpmsContext Context { get; set; }
        public IHttpContextAccessor HttpContext;

        public SeoQueryFixture()
        {
            Context = TestSpmsContextFactory.Create();

            var mock = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            mock.Setup(_=>_.HttpContext).Returns(context);
            HttpContext = mock.Object;

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