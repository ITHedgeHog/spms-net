using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;

namespace SPMS.Application.Authoring.Command.NotifyDiscord
{
    public class NotifyDiscordCmd : IRequest<int>
    {

        public class NotifyDiscordCmdHandler : IRequestHandler<NotifyDiscordCmd, int>
        {
            private readonly ISpmsContext _db;
            private readonly IMediator _mediator;
            private readonly IBackgroundIdentifierMask _mask;
            private readonly ILogger<NotifyDiscordCmdHandler> _logger;
            private readonly int _notificationDelay;

            public NotifyDiscordCmdHandler(ISpmsContext db, IMediator mediator, IBackgroundIdentifierMask mask, ILogger<NotifyDiscordCmdHandler> logger, IConfiguration configuration)
            {
                _db = db;
                _mediator = mediator;
                _mask = mask;
                _logger = logger;
                _notificationDelay = configuration.GetValue<int>("NotificationDelay", 15);
            }

            public async Task<int> Handle(NotifyDiscordCmd request, CancellationToken cancellationToken)
            {
                
                var itemsSent = 0;
                var itemsToPost = _db.EpisodeEntry.Include(e => e.EpisodeEntryStatus)
                    .Include(e => e.EpisodeEntryType)
                    .Include(e => e.Episode)
                    .ThenInclude(e => e.Series)
                    .ThenInclude(e => e.Game)
                    .ThenInclude(e => e.Url)
                    .Where(x => x.EpisodeEntryStatus.Name == StaticValues.Published 
                                && x.PublishedAt.Value.AddMinutes(_notificationDelay) <= DateTime.UtcNow 
                                && x.IsPostedToDiscord == false 
                                && x.Episode.Series.Game.IsReadonly == false)
                    .OrderBy(x => x.Episode.Series.GameId)
                    .ThenBy(x => x.Timeline)
                    .ToList();


                foreach (var item in itemsToPost)
                {
                    if (string.IsNullOrEmpty(item.Episode.Series.Game.DiscordWebHook) || !item.Episode.Series.Game.Url.Any(x => x.IsPrimary))
                        break;

                    _mask.SetKey(item.Episode.Series.Game.GameKey);
                    var url = "https://" + item.Episode.Series.Game.Url.First(x => x.IsPrimary).Url + "/Story/" + _mask.HideId(item.Id);
                    var cmd = new SendDiscordNotificationCmd()
                    {
                        WebHookUrl = item.Episode.Series.Game.DiscordWebHook,
                        Message = $"New Post {url} by {item.LastModifiedBy} @ {item.PublishedAt}",
                    };
                    _logger.LogInformation($"Created {cmd.WebHookUrl} and {cmd.Message}");

                    try
                    {
                        await _mediator.Publish(cmd, cancellationToken);

                        item.IsPostedToDiscord = true;
                        _db.EpisodeEntry.Update(item);
                        itemsSent++;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }
                }

                await _db.SaveChangesAsync(cancellationToken);

                return itemsSent;
            }
        }

        public class SendDiscordNotificationCmd : INotification
        {
            public string WebHookUrl { get; set; }
            public string Message { get; set; }
            public class SendDiscordNotificationCmdHandler : INotificationHandler<SendDiscordNotificationCmd>
            {
                private readonly IDiscordService _discord;

                public SendDiscordNotificationCmdHandler(IDiscordService discord)
                {
                    _discord = discord;
                }

                public async Task Handle(SendDiscordNotificationCmd notification, CancellationToken cancellationToken)
                {
                    await _discord.Send(notification.WebHookUrl, new WebhookCall() {Content = notification.Message});
                }
            }
        }

    }
}
