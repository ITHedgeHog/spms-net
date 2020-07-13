using System;
using System.Collections.Generic;
using SPMS.Application.Dtos.Authoring;
using SPMS.Common.ViewModels;
using SPMS.ViewModel.Story;

namespace SPMS.Application.Dtos.Story
{
    public class StoryOverviewDto : SPMS.Common.ViewModels.ViewModel
    {
        public StoryOverviewDto()
        {
            ForthComingSeries = new List<SeriesDto>();
            ForthComingEpisodes = new List<EpisodeDto>();
        }

        public List<SeriesDto> ForthComingSeries { get; set; }
        public List<EpisodeDto> ForthComingEpisodes { get; set; }
        public SeriesDto CurrentSeries { get; set; }
        public EpisodeDto CurrentEpisode { get; set; }
        public int SeriesNumber { get; set; }
        public int EpisodeNumber { get; set; }
    }

    public class SeriesDto
    {
        public SeriesDto()
        {
            Episodes = new List<EpisodeDto>();
        }

        public List<EpisodeDto> Episodes { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Banner { get; set; }
        public List<StoryPostDto> Story { get; set; }
    }

    public class PostViewModel
    {
        public PostViewModel()
        {
            Authors = new List<AuthorViewModel>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Timeline { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string LastAuthor { get; set; }
        public List<AuthorViewModel> Authors { get; set; }

    }
}
