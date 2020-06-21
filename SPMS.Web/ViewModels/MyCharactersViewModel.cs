﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Web.ViewModels
{
    public class MyCharactersViewModel : ViewModel
    {
        public MyCharactersViewModel()
        {
            Characters = new Dictionary<int, string>();
        }

        public bool IsCreateCharacterEnabled { get; set; }

        public Dictionary<int, string> Characters { get; set; }
        public bool HasEpisode { get; set; }
    }


    public class MyWritingViewModel : ViewModel
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

    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<string> Authors { get; set; }
    }
}
