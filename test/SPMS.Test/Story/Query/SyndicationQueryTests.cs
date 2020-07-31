using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Common.Mappings;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Player;
using SPMS.Application.Story.Query;
using SPMS.Application.Tests.Common;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;
using Xunit;

namespace SPMS.Application.Tests.Story.Query
{
    public class SyndicationQueryTests : IClassFixture<SyndicationQueryFixture>
    {
        private readonly ISpmsContext _db;
        private IMapper _mapper;

        public SyndicationQueryTests(SyndicationQueryFixture fixture)
        {
            _db = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task ShouldReturnObject()
        {
            var mockTenantAccessor = new Mock<ITenantAccessor<TenantDto>>();
            mockTenantAccessor.Setup(x => x.Instance).Returns(new TenantDto() {Id = 1});
            var query = new SyndicationQuery();
            var sut = new SyndicationQuery.SyndicationQueryHandler(_db, mockTenantAccessor.Object, _mapper);

            var result = await sut.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<List<PostDto>>();
            result.Count.ShouldBeGreaterThanOrEqualTo(1);

        }


    }

    public class SyndicationQueryFixture : IDisposable
    {
        public SpmsContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public SyndicationQueryFixture()
        {
            Context = TestSpmsContextFactory.Create();

            Context.EpisodeEntry.Add(new EpisodeEntry()
            {
                EpisodeEntryStatusId = Context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Published).Id,
                EpisodeEntryTypeId = Context.EpisodeEntryType.First(x => x.Name == StaticValues.Post).Id,
                Title = "First Post",
                Location = "Everywhere",
                Timeline = "MD0-09:00",
                PublishedAt = DateTime.UtcNow.AddDays(-1),
                EpisodeId = Context.Episode.Include(x => x.Series).Include(x => x.Status).First(x => x.Series.GameId == 1 && x.Status.Name == StaticValues.Published).Id,
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
