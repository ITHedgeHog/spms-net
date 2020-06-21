using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SPMS.Web.ViewModels.Authoring
{
    public class AuthorPostViewModel : ViewModel
    {
        public AuthorPostViewModel(in int activeEpisodeId)
        {
            EpisodeId = activeEpisodeId;
            Authors = new List<int>();
            PostTypes = new List<SelectListItem>();
        }

        public AuthorPostViewModel()
        {
            Authors = new List<int>();
        }

        public List<int> Authors { get; set; }
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
        public IQueryable<SelectListItem> Statuses { get; set; }
    }
}