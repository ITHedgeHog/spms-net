using System.Collections.ObjectModel;

namespace SPMS.Domain.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int? StatusId { get; set; }
        public EpisodeStatus Status { get; set; }

        public Collection<EpisodeEntry> Entries { get; set; }

        public int? SeriesId { get; set; }
        public Series Series { get; set; }
    }
}