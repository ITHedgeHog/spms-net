using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SPMS.Application.Authoring.Command.NotifyDiscord;
using SPMS.Application.Authoring.Command.PublishAllPendingPosts;
using SPMS.Application.Common.Interfaces;

namespace SPMS.BackgroundService
{
    public class PublishService : IHostedService, IDisposable
    {
        private int _executionCount = 0;
        private readonly ILogger<PublishService> _logger;
        private Timer _timer;
        private IServiceProvider _services;
        private CancellationToken _stoppingToken;

        public PublishService(IServiceProvider services, ILogger<PublishService> logger)
        {
            _logger = logger;
            _services = services;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _stoppingToken = stoppingToken;
            _logger.LogInformation("Publish Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            var count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation(
                "Publish Service is working. Count: {Count}", count);


            using (var scope = _services.CreateScope())
            {
                var mediator =
                    scope.ServiceProvider
                        .GetRequiredService<IMediator>();

                var result = await mediator.Send(new PublishAllPendingPostsCommand(), _stoppingToken).ConfigureAwait(true);
                _logger.LogInformation("Published posts: " + result);
                var intResult = await mediator.Send(new NotifyDiscordCmd(), _stoppingToken);
                _logger.LogInformation($"{intResult} items posted to Discord.");
            }
             
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Publish Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
