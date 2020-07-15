using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SPMS.Application.Common.Interfaces;

namespace SPMS.Web.Service
{
    public class ApplicationVersion : IApplicationVersion
    {
        public string Version => Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "0.0.0";
        public string InformationVersion => Assembly.GetEntryAssembly()!.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion ?? "0.0.0";
    }
}
