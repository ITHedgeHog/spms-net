using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using Shouldly;
using SPMS.Application.Authoring.Command.CreatePost;
using SPMS.Test.Common;
using Xunit;


namespace SPMS.Test.Application
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
