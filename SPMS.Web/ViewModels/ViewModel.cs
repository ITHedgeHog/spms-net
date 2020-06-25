namespace SPMS.Web.ViewModels
{
    public class ViewModel
    {
        public ViewModel()
        {
            IsReadOnly = false;
            UseAnalytics = false;
        }

        public string gravatar { get; set; }
        
        public bool IsReadOnly { get; set; }

        public string SiteTitle { get; set; }

        public string GameName { get; set; }

        public string SiteDisclaimer { get; set; }
        public string SiteAnalytics { get; set; }

        public bool UseAnalytics { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPlayer { get; set; }
        public string CommitShaLink { get; set; }
        public string CommitSha { get; set; }
    }
}