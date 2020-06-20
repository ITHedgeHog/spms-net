using SPMS.Web.Models;

namespace SPMS.Web.ViewModels.Authoring
{
    public class CreateEpisodeEntryViewModel : EpisodeEntry
    {
        public CreateEpisodeEntryViewModel(in int activeEpisodeId)
        {
            EpisodeId = activeEpisodeId;
        }

        public int EpisodeId { get; set; }
    }
}
