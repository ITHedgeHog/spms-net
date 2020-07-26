#nullable enable
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SPMS.Common;

namespace SPMS.Application.Common.Interfaces
{
    public interface IDiscordService
    {
        Task<HttpResponseMessage> Send(string webHookUrl, WebhookCall call);
    }
}