using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SPMS.Web.Models
{
    public class Episode
    {
        [Key] public int Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }

        public int? StatusId { get; set; }
        public EpisodeStatus Status { get; set; }

        public Collection<EpisodeEntry> Entries { get; set; }

        public int? SeriesId { get; set; }
        public Series Series { get; set; }
    }
}