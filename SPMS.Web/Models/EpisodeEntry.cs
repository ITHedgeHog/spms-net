using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SPMS.Web.Models
{
    public class EpisodeEntry
    {
        public EpisodeEntry()
        {
            EpisodeEntryPlayer = new Collection<EpisodeEntryPlayer>();
        }


        [Key]
        public int Id { get; set; }
        [NotNull, MaxLength(200)]
        public string Title { get; set; }


        [NotNull, MaxLength(200)]
        public string Location { get; set; }

        [NotNull]
        [MaxLength(200)]
        public string Timeline { get; set; }

        [NotNull]
        public string Content { get; set; }

        public int EpisodeEntryStatusId { get; set; }
        public EpisodeEntryStatus EpisodeEntryStatus { get; set; }

        public Collection<EpisodeEntryPlayer> EpisodeEntryPlayer { get; set; }

        public int EpisodeEntryTypeId { get; set; }

        public EpisodeEntryType EpisodeEntryType { get; set; }

        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}