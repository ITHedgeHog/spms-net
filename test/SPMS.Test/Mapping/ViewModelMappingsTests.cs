using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SPMS.Application.Common.Mappings;
using SPMS.Web.Mapping;
using Xunit;

namespace SPMS.Application.Tests.Mapping
{

    public class ViewModelMappingsTests : IClassFixture<ViewModelMappingsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public ViewModelMappingsTests(ViewModelMappingsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact()]
        public void ShouldHaveValidConfiguration()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }

    public class ViewModelMappingsFixture
    {
        public ViewModelMappingsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelMappingProfile>();

            });

            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}
