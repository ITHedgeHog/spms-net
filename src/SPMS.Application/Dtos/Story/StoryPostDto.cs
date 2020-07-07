using System;
using System.Collections.Generic;
using System.Text;

namespace SPMS.Application.Dtos.Story
{
    public class StoryPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Timeline { get; set; }

        public string Content { get; set; }

        public int EpisodeEntryStatusId { get; set; }
        public string EpisodeEntryStatus { get; set; }

        public List<string> EpisodeEntryPlayer { get; set; }

        public int EpisodeEntryTypeId { get; set; }

        public string EpisodeEntryType { get; set; }

        public int EpisodeId { get; set; }
        public string Episode { get; set; }
        public DateTime? PublishedAt { get; set; }

    }
}
