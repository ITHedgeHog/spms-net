using System;
using System.Collections.Generic;
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
using SPMS.Application.Dtos.Player;
using SPMS.Application.Tests.Common;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;
using Xunit;

namespace SPMS.Application.Tests.Authoring.Query
{
    public class WritingPortalQueryTests : IClassFixture<WritingPortalQueryFixture>
    {
        private readonly SpmsContext _db;
        private readonly IMapper _mapper;

        public WritingPortalQueryTests(WritingPortalQueryFixture fixture)
        {
            _db = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ShouldReturnWritingPortalDto()
        {
            var mockUserService = new Mock<IUserService>();
            var request = new WritingPortalQuery();
            var sut = new WritingPortalQuery.WritingPortalQueryHandler(_db, mockUserService.Object, _mapper);

            var result = await sut.Handle(request, CancellationToken.None);
             
            result.ShouldBeOfType<WritingPortalDto>();
            result.DraftPosts.ShouldBeOfType<List<PostDto>>();
            result.PendingPosts.ShouldBeOfType<List<PostDto>>();
        }
    }


    public class WritingPortalQueryFixture : IDisposable
    {
        public SpmsContext Context { get; set; }
        public IMapper Mapper { get; set; }
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
                TypeId = Context.BiographyTypes.First().Id
            });
            Context.SaveChanges();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<QueryMappings>();

            });

            Mapper = configurationProvider.CreateMapper();

        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
