using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SPMS.Application.Common.Mappings;
using Xunit;

namespace SPMS.Application.Tests.Mapping
{
    public class QueryMappingsTest : IClassFixture<QueryMappingsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public QueryMappingsTest(QueryMappingsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact(DisplayName = "Query Mapping Should Be Valid")]
        public void ShouldHaveValidConfiguration()
        {

            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }

    public class QueryMappingsFixture
    {
        public QueryMappingsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<QueryMappings>(); });
            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}