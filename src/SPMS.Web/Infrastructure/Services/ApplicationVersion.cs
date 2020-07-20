using System.Reflection;
using SPMS.Application.Common.Interfaces;

namespace SPMS.Web.Infrastructure.Services
{
    public class ApplicationVersion : IApplicationVersion
    {
        public string Version => Assembly.GetEntryAssembly()?.GetName().Version?.ToString();

        public string FileVersion => Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyFileVersionAttribute>()
            ?.Version ?? "0.0.0";
        public string InformationVersion => Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                                ?.InformationalVersion ?? "0.0.0";
    }
}
