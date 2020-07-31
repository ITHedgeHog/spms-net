using System.Collections.Generic;
using SPMS.Application.Dtos.Story;

namespace SPMS.Application.Dtos
{
    public class MyCharactersViewModel : SPMS.Common.ViewModels.BaseViewModel
    {
        public MyCharactersViewModel()
        {
            Characters = new Dictionary<int, string>();
        }

        public bool IsCreateCharacterEnabled { get; set; }

        public Dictionary<int, string> Characters { get; set; }
        public bool HasEpisode { get; set; }
    }


    public class MyWritingViewModel : SPMS.Common.ViewModels.BaseViewModel
    {
        public MyWritingViewModel()
        {
            new Dictionary<int, string>();
            DraftPosts = new List<PostViewModel>();
            PendingPosts = new List<PostViewModel>();
        }

        public List<PostViewModel> DraftPosts { get; set; }

        public List<PostViewModel> PendingPosts { get; set; }
        public bool CanPost { get; set; }
    }

}
