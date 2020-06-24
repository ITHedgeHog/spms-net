using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using SPMS.Web.Mapper;
using SPMS.Web.Models;
using SPMS.Web.Service;
using Xunit;

namespace SPMS.Test
{
    public class AuthoringServiceTest
    {
        [Fact(Skip = "true")]
        public async Task HasActiveEpisode()
        {
            var mockDb = new Mock<SpmsContext>();
            ///mockDb
            var mockGame = new Mock<IGameService>();
            var userServiceMock = new Mock<IUserService>();

            var myProfile = new AuthorProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);
            var authoringServer = new AuthoringService(mockDb.Object, mapper, mockGame.Object, userServiceMock.Object);

            var hasActiveEpisode = await authoringServer.HasActiveEpisodeAsync();

            Assert.True(hasActiveEpisode);

        }
    }
}
