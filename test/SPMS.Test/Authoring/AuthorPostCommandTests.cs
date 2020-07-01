using System.Linq;
using System.Threading;
using MediatR;
using Moq;
using Shouldly;
using SPMS.Application.Authoring.Command.CreatePost;
using SPMS.Application.Tests.Common;
using Xunit;

namespace SPMS.Application.Tests.Authorings
{
    public class AuthorPostCommandTests : CommandTestBase
    {
        [Fact]
        public async void Should_Create_New_Post()
        {
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreatePost.CreatePostHandler(Context);


            var result = await sut.Handle(new CreatePost(), CancellationToken.None);

            result.ShouldBe<int>(1);

            Context.EpisodeEntry.Count().ShouldBe(1);
        }
    }
}
