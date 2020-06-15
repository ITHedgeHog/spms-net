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

        [NotNull]
        public string Content { get; set; }

        public Collection<Player> Players { get; set; }
    }
}