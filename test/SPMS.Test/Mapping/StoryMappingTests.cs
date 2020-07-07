using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SPMS.Application.Common.Mappings;
using Xunit;

namespace SPMS.Application.Tests.Mapping
{
    public class StoryMappingTests : IClassFixture<StoryMappingFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public StoryMappingTests(StoryMappingFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact(DisplayName = "StoryMapping Mapper is valid")]
        public void ShouldHaveValidConfiguration()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }

    public class StoryMappingFixture
    {
        public StoryMappingFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StoryMapping>();

            });

            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}
