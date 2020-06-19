using System.Runtime.Intrinsics.X86;

namespace SPMS.Web.ViewModels
{
    public class ViewModel
    {
        public ViewModel()
        {
            IsReadOnly = false;
            UseAnalytics = false;
        }

        public bool IsReadOnly { get; set; }

        public string SiteTitle { get; set; }

        public string GameName { get; set; }

        public string SiteDisclaimer { get; set; }
        public string SiteAnalytics { get; set; }

        public bool UseAnalytics { get; set; }        

    }
}