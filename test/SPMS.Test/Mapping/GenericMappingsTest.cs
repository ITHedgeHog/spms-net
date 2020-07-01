using AutoMapper;
using SPMS.Application.Common.Mappings;
using Xunit;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace SPMS.Application.Tests.Mapping
{
    public class GenericMappingsTest : IClassFixture<GenericMappingsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public GenericMappingsTest(GenericMappingsFixture fixture) 
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

    public class GenericMappingsFixture
    {
        public GenericMappingsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GenericMapper>();
                //cfg.AddProfile<BiographyMapper>();
                //cfg.AddProfile<AuthorProfile>();

            }); Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}
