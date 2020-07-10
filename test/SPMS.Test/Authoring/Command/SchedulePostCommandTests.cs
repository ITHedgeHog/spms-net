using System;
using System.Linq;
using System.Threading;
using MediatR;
using Moq;
using Shouldly;
using SPMS.Application.Authoring.Command.SchedulePost;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;
using Xunit;
using SpmsContextFactory = SPMS.Application.Tests.Common.SpmsContextFactory;

namespace SPMS.Application.Tests.Authoring.Command
{
    public class SchedulePostCommandTests : IClassFixture<SchedulePostFixture>
    {
        private readonly SpmsContext Context;
        public SchedulePostCommandTests(SchedulePostFixture fixture)
        {
            Context = fixture.Context;
        }

        [Fact]
        public async void Should_Mark_As_Pending_And_Set_PublishedAt()
        {
            var publishDate = DateTime.UtcNow;
            var mediatorMock = new Mock<IMediator>();
            var sut = new SchedulePostCommand.SchedulePostHandler(Context);

            var result = await sut.Handle(new SchedulePostCommand() { Id = 1, PublishAt = publishDate }, CancellationToken.None);

            result.ShouldBeTrue();
            Context.EpisodeEntry.Count(x =>
                x.EpisodeEntryStatusId == Context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Pending).Id).ShouldBe(1);

            Context.EpisodeEntry.First(x => x.Id == 1).PublishedAt.ShouldBe(publishDate);
        }

        [Fact]
        public async void Should_Not_Find_Post_And_return_false()
        {

            var mediatorMock = new Mock<IMediator>();
            var sut = new SchedulePostCommand.SchedulePostHandler(Context);

            var result = await sut.Handle(new SchedulePostCommand() { Id = 120 }, CancellationToken.None);

            result.ShouldBeFalse();
        }
    }

    public class SchedulePostFixture : IDisposable
    {
        public SpmsContext Context { get; set; }
        public SchedulePostFixture()
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
                EpisodeEntryStatusId = Context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Draft).Id,
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
