using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;

namespace SPMS.Infrastructure.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly IHttpClientFactory _clientFactory;

        public DiscordService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<HttpResponseMessage> Send(string webHookUrl, WebhookCall call)
        {
            var payload = JsonSerializer.Serialize(call, new JsonSerializerOptions()
            {
                //PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpClient = _clientFactory.CreateClient();
            return await httpClient.PostAsync(webHookUrl, content);
        }
    }
}
