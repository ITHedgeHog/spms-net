using System;
using System.Collections.ObjectModel;

namespace SPMS.Domain.Models
{
    public class Game
    {
        public int Id { get; set; }
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Collection<GameUrl> Url { get; set; }
        public string SiteTitle { get; set; }
        public string Disclaimer { get; set; }
        public bool IsReadonly { get; set; }
        public string SiteAnalytics { get; set; }
        public byte[] GameKey { get; set; }
        public bool IsTest { get; set; }
        public bool? IsSpiderable { get; set; }
        public string Author { get; set; }
        public string Keywords { get; set; }
        public string RobotsText { get; set; }
        public string Theme { get; set; }
        public string DiscordWebHook { get; set; }
    }
}