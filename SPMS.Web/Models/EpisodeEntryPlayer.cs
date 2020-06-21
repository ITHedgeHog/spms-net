namespace SPMS.Web.Models
{
    public class EpisodeEntryPlayer
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int EpisodeEntryId { get; set; }
        public EpisodeEntry EpisodeEntry { get; set; }
    }
}