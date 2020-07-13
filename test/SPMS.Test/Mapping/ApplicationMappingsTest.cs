using AutoMapper;
using SPMS.Application.Common.Mappings;
using Xunit;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace SPMS.Application.Tests.Mapping
{
    public class ApplicationMappingsTest : IClassFixture<ApplicationMappingsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public ApplicationMappingsTest(ApplicationMappingsFixture fixture) 
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact(DisplayName = "Application Mapping Should Be Valid")]
        public void ShouldHaveValidConfiguration()
        {

            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }

    public class ApplicationMappingsFixture
    {
        public ApplicationMappingsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationMapper>();
                //cfg.AddProfile<BiographyMapper>();
                //cfg.AddProfile<AuthorProfile>();

            }); Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}
