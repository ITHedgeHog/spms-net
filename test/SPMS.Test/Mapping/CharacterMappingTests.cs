using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SPMS.Application.Common.Mappings;
using Xunit;

namespace SPMS.Application.Tests.Mapping
{
    public class CharacterMappingsTest : IClassFixture<CharacterMappingsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public CharacterMappingsTest(CharacterMappingsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact()]
        public void CharacterMappingsShouldHaveValidConfiguration()
        {

            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }

    public class CharacterMappingsFixture
    {
        public CharacterMappingsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CharacterProfile>();

            }); Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}
