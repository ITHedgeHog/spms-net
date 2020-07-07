using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace SPMS.ViewModel.Story
{
    public class StoryPostViewModel : SPMS.Common.ViewModels.ViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Timeline { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public string NextPost { get; set; }
        public string LastPost { get; set; }



    }
}
