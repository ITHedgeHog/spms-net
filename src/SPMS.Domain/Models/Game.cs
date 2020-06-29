using System.Collections.ObjectModel;

namespace SPMS.Domain.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public Collection<GameUrl> Url { get; set; }
        public string SiteTitle { get; set; }
        public string Disclaimer { get; set; }
        public bool IsReadonly { get; set; }
        public string SiteAnalytics { get; set; }
    }
}