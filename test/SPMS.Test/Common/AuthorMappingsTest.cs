using AutoMapper;
using SPMS.Application.Common.Mappings;
using Xunit;

namespace SPMS.Application.Tests.Common
{
    public class AuthorMappingsTest : IClassFixture<AuthorMappingsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public AuthorMappingsTest(AuthorMappingsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact()]
        public void ShoudHaveValidConfiguration()
        {

            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }

    public class AuthorMappingsFixture
    {
        public AuthorMappingsFixture()
        {
            ConfigurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AuthorProfile>();

            }); 
            
            Mapper = ConfigurationProvider.CreateMapper();
        }

        public IConfigurationProvider ConfigurationProvider { get; }

        public IMapper Mapper { get; }
    }
}