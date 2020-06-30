using System;
using System.Collections.ObjectModel;
using SPMS.Domain.Common;

namespace SPMS.Domain.Models
{
    public class EpisodeEntry //: AuditableEntity
    {
        public EpisodeEntry()
        {
            EpisodeEntryPlayer = new Collection<EpisodeEntryPlayer>();
            Title = string.Empty;
            Location = string.Empty;
            Timeline = string.Empty;
            Content = string.Empty;
        }


        //[Key]
        public int Id { get; set; }
      //  [NotNull, MaxLength(200)]
        public string Title { get; set; }


        //[NotNull, MaxLength(200)]
        public string Location { get; set; }

       // [NotNull]
      //  [MaxLength(200)]
        public string Timeline { get; set; }

      //  [NotNull]
        public string Content { get; set; }

        public int EpisodeEntryStatusId { get; set; }
        public EpisodeEntryStatus EpisodeEntryStatus { get; set; }

        public Collection<EpisodeEntryPlayer> EpisodeEntryPlayer { get; set; }

        public int EpisodeEntryTypeId { get; set; }

        public EpisodeEntryType EpisodeEntryType { get; set; }

        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}