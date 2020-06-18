using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SPMS.Web.Models
{
    public class EpisodeEntry
    {
        [Key]
        public int Id { get; set; }
        [NotNull, MaxLength(200)]
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; }

        [NotNull, MaxLength(200)]
        public string Location { get; set; }

        [NotNull]
        [MaxLength(200)]
        public string Timeline { get; set; }

        [NotNull]
        public string Content { get; set; }

        public Collection<Player> Players { get; set; }

        public int EpisodeEntryTypeId { get; set; }

        public EpisodeEntryType EpisodeEntryType { get; set; }

        public Series Series { get; set; }
        public int SeriesId { get; set; }

    }

        public class EpisodeEntryType
        {
            [Key]
            public int Id { get; set; }

            [MaxLength(150)]
            public string Name { get; set; }
        }
}