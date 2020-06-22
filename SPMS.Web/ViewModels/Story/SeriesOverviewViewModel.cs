﻿using SPMS.Web.ViewModels.Authoring;
using System;
using System.Collections.Generic;

namespace SPMS.Web.ViewModels.Story
{
    public class SeriesOverviewViewModel : ViewModel
    {
        public SeriesOverviewViewModel()
        {
            ForthComingSeries = new List<SeriesViewModel>();
            ForthComingEpisodes = new List<EpisodeViewModel>();
        }

        public List<SeriesViewModel> ForthComingSeries { get; set; }
        public List<EpisodeViewModel> ForthComingEpisodes { get; set; }
        public SeriesViewModel CurrentSeries { get; set; }
        public EpisodeViewModel CurrentEpisode { get; set; }
    }

    public class SeriesViewModel
    {
        public SeriesViewModel()
        {
            Episodes = new List<EpisodeViewModel>();
        }

        public List<EpisodeViewModel> Episodes { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class EpisodeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Banner { get; set; }
        public List<PostViewModel> Story { get; internal set; }
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
        public DateTime PublishedAt { get; set; }
        public List<AuthorViewModel> Authors { get; set; }

    }
}
