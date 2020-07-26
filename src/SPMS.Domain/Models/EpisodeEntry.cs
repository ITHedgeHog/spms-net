using System;
using System.Collections.ObjectModel;
using SPMS.Domain.Common;

namespace SPMS.Domain.Models
{
    public class EpisodeEntry : AuditableEntity
    {
        public EpisodeEntry()
        {
            EpisodeEntryPlayer = new Collection<EpisodeEntryPlayer>();
            Title = string.Empty;
            Location = string.Empty;
            Timeline = string.Empty;
            Content = string.Empty;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Timeline { get; set; }
        public string Content { get; set; }
        public int EpisodeEntryStatusId { get; set; }
        public EpisodeEntryStatus EpisodeEntryStatus { get; set; }
        public Collection<EpisodeEntryPlayer> EpisodeEntryPlayer { get; set; }
        public int EpisodeEntryTypeId { get; set; }
        public EpisodeEntryType EpisodeEntryType { get; set; }
        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
        public DateTime? PublishedAt { get; set; }
        public bool IsPostedToDiscord { get; set; }
    }
}