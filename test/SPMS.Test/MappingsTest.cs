using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using SPMS.Application.Common.Mappings;
using Xunit;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace SPMS.Test
{
    public class MappingsTest : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingsTest(MappingTestsFixture fixture) 
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact(Skip = "Not Implemented")]
        public void ShoudHaveValidConfiguration()
        {

            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }

    public class MappingTestsFixture
    {
        public MappingTestsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GenericMapper>();
                cfg.AddProfile<BiographyMapper>();
                cfg.AddProfile<AuthorProfile>();

            }); Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}
