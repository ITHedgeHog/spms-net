using System.Collections.Generic;
using SPMS.Application.Dtos.Story;

namespace SPMS.Application.Dtos
{
    public class MyCharactersViewModel : SPMS.Common.ViewModels.ViewModel
    {
        public MyCharactersViewModel()
        {
            Characters = new Dictionary<int, string>();
        }

        public bool IsCreateCharacterEnabled { get; set; }

        public Dictionary<int, string> Characters { get; set; }
        public bool HasEpisode { get; set; }
    }


    public class MyWritingViewModel : SPMS.Common.ViewModels.ViewModel
    {
        public MyWritingViewModel()
        {
            Characters = new Dictionary<int, string>();
            DraftPosts = new List<PostViewModel>();
            PendingPosts = new List<PostViewModel>();
        }

        public bool IsCreateCharacterEnabled { get; set; }

        public List<PostViewModel> DraftPosts { get; set; }

        public Dictionary<int, string> Characters { get; set; }
        public bool HasEpisode { get; set; }
        public List<PostViewModel> PendingPosts { get; set; }
    }

}
