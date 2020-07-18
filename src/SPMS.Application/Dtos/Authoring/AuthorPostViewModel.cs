using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SPMS.Application.Dtos.Authoring
{
    public class AuthorPostViewModel : SPMS.Common.ViewModels.BaseViewModel
    {
        
        public AuthorPostViewModel(in int activeEpisodeId)
        {
            EpisodeId = activeEpisodeId;
            Authors = new List<AuthorViewModel>();
            PostTypes = new List<SelectListItem>();
            Statuses = new List<SelectListItem>();
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
            Timeline = string.Empty;
        }

        public AuthorPostViewModel()
        {
            Authors = new List<AuthorViewModel>();
            PostTypes = new List<SelectListItem>();
            Statuses = new List<SelectListItem>();
        }

        public List<AuthorViewModel> Authors { get; set; }
        [Display(Name = "Post Type")]

        public int TypeId { get; set; }

        [Display(Name = "Episode Id")]
        public int EpisodeId { get; set; }

        [Display(Name = "Episode Name")]
        public string Episode { get; set; }

        [MaxLength(200), Required(ErrorMessage = "You must provide a title")]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Location { get; set; }
        [MaxLength(200)]
        public string Timeline { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "You must provide content")]
        public string Content { get; set; }

        public List<SelectListItem> PostTypes { get; set; }


        public int Id { get; set; }
        public int StatusId { get; set; }
        public List<SelectListItem> Statuses { get; set; }

        public string submitpost { get; set; }
        public DateTime? PostAt { get; set; }
    }

    public class AuthorPostDraftViewModel : SPMS.Common.ViewModels.BaseViewModel
    {

        public AuthorPostDraftViewModel(in int activeEpisodeId)
        {
            EpisodeId = activeEpisodeId;
            Authors = new List<AuthorViewModel>();
            PostTypes = new List<SelectListItem>();
            Statuses = new List<SelectListItem>();
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
            Timeline = string.Empty;
        }

        public AuthorPostDraftViewModel()
        {
            Authors = new List<AuthorViewModel>();
            PostTypes = new List<SelectListItem>();
            Statuses = new List<SelectListItem>();
        }

        public List<AuthorViewModel> Authors { get; set; }
        [Display(Name = "Post Type")]

        public int TypeId { get; set; }

        [Display(Name = "Episode Id")]
        public int EpisodeId { get; set; }

        [Display(Name = "Episode Name")]
        public string Episode { get; set; }

        [MaxLength(200)] //, Required(ErrorMessage = "You must provide a title")]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Location { get; set; }
        [MaxLength(200)]
        public string Timeline { get; set; }

        [DataType(DataType.MultilineText)]
        //[Required(ErrorMessage = "You must provide content")]
        public string Content { get; set; }

        public List<SelectListItem> PostTypes { get; set; }


        public int Id { get; set; }
        public int StatusId { get; set; }
        public List<SelectListItem> Statuses { get; set; }

        public string submitpost { get; set; }
        public DateTime? PostAt { get; internal set; }
    }
}