using System;
using System.Linq;
using System.Threading;
using MediatR;
using Moq;
using Shouldly;
using SPMS.Application.Authoring.Command.UnpublishPost;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;
using Xunit;
using SpmsContextFactory = SPMS.Application.Tests.Common.SpmsContextFactory;

namespace SPMS.Application.Tests.Authoring.Command
{
    public class UnpublishPostCommandTests : IClassFixture<UnpublishPostFixture>
    {
        private readonly SpmsContext Context;
        public UnpublishPostCommandTests(UnpublishPostFixture fixture)
        {
            Context = fixture.Context;
        }

        [Fact]
        public async void Should_Mark_As_Draft_And_Unpublish()
        {
            var mediatorMock = new Mock<IMediator>();
            var sut = new UnpublishPostCommand.UnpublishPostHandler(Context);

            var result = await sut.Handle(new UnpublishPostCommand() { Id = 1 }, CancellationToken.None);

            result.ShouldBeTrue();
            Context.EpisodeEntry.Count(x =>
                x.EpisodeEntryStatusId == Context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Draft).Id).ShouldBe(1);

            Context.EpisodeEntry.First(x => x.Id == 1).PublishedAt.ShouldBeNull();
        }

        [Fact]
        public async void Should_Not_Find_Post_And_return_false()
        {

            var mediatorMock = new Mock<IMediator>();
            var sut = new UnpublishPostCommand.UnpublishPostHandler(Context);

            var result = await sut.Handle(new UnpublishPostCommand() { Id = 120 }, CancellationToken.None);

            result.ShouldBeFalse();
        }
    }

    public class UnpublishPostFixture : IDisposable
    {
        public SpmsContext Context { get; set; }
        public UnpublishPostFixture()
        {
            Context = SpmsContextFactory.Create();
            Context.EpisodeEntry.Add(new EpisodeEntry()
            {
                Title = "Post 1",
                Location = "Location",
                Timeline = "Timeline",
                Content = "A post!",
                Created = DateTime.UtcNow,
                CreatedBy = "Dan Taylor",
                LastModified = DateTime.UtcNow,
                LastModifiedBy = "Dan Taylor",
                EpisodeEntryStatusId = Context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Published).Id,
                EpisodeEntryTypeId = Context.EpisodeEntryType.First(x => x.Name == StaticValues.Post).Id
            });

            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
