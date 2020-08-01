using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using SPMS.Application.Authoring.Command.NotifyDiscord;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Tests.Common;
using SPMS.Common;
using SPMS.Domain.Models;
using SPMS.Persistence.MSSQL;
using Xunit;

namespace SPMS.Application.Tests.Authoring.Command
{
    public class NotifyDiscordCmdTests : IClassFixture<NotifyDiscordCmdFixture>
    {
        private readonly ISpmsContext _db;

        public NotifyDiscordCmdTests(NotifyDiscordCmdFixture fixture)
        {
            _db = fixture.Context;
        }

        [Fact]
        public async Task ShouldReturn1()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                {"NotificationDelay", "15"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            var cmd = new NotifyDiscordCmd();
            var mockMediator = new Mock<IMediator>();
            var mockIdentifierMask = new Mock<IBackgroundIdentifierMask>();
            var mockLogger = new Mock<ILogger<NotifyDiscordCmd.NotifyDiscordCmdHandler>>();

            var sut = new NotifyDiscordCmd.NotifyDiscordCmdHandler(_db, 
                mockMediator.Object, mockIdentifierMask.Object, mockLogger.Object, configuration);

            var result = await sut.Handle(cmd, CancellationToken.None);

            result.ShouldBeGreaterThanOrEqualTo(1);

        }


    }

    public class NotifyDiscordCmdFixture : IDisposable
    {
        public SpmsContext Context { get; set; }

        public NotifyDiscordCmdFixture()
        {
            Context = TestSpmsContextFactory.Create();

            Context.EpisodeEntry.Add(new EpisodeEntry()
            {
                EpisodeId = Context.Episode.First().Id,
                Title = "123 Post",
                IsPostedToDiscord = false,
                EpisodeEntryStatusId = Context.EpisodeEntryStatus.First(x => x.Name == StaticValues.Published).Id,
                PublishedAt = DateTime.UtcNow.AddHours(-1),
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
