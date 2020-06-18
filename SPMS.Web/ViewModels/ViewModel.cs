using System.Runtime.Intrinsics.X86;

namespace SPMS.Web.ViewModels
{
    public class ViewModel
    {
        public bool IsReadOnly { get; set; }

        public string SiteTitle { get; set; }

        public string GameName { get; set; }

        public string SiteDisclaimer { get; set; }
    }
}