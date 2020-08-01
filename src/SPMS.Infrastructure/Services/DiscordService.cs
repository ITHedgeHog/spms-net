using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;

namespace SPMS.Infrastructure.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<DiscordService> _logger;

        public DiscordService(IHttpClientFactory clientFactory, ILogger<DiscordService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }


        public async Task<HttpResponseMessage> Send(string webHookUrl, WebhookCall call)
        {
            try
            {
                var payload = JsonSerializer.Serialize(call, new JsonSerializerOptions()
                {
                    //PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var httpClient = _clientFactory.CreateClient();
                var response = await httpClient.PostAsync(webHookUrl, content);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new HttpResponseMessage(statusCode:HttpStatusCode.InternalServerError);
            }
        }
    }
}
