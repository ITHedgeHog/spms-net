using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SPMS.Web.ViewModels.Authoring
{
    public class AuthorPostViewModel : ViewModel
    {
        public AuthorPostViewModel(in int activeEpisodeId)
        {
            EpisodeId = activeEpisodeId;
            Authors = new List<int>();
        }

        public AuthorPostViewModel()
        {
            Authors = new List<int>();
        }

        public List<int> Authors { get; set; }

        public int TypeId { get; set; }

        public int EpisodeId { get; set; }

        [MaxLength(200), Required(ErrorMessage = "You must provide a title")]
        public string PostTitle { get; set; }
        [MaxLength(200)]
        public string PostLocation { get; set; }
        [MaxLength(200)]
        public string PostTimeline { get; set; }

        public List<SelectListItem> Types =>
            new List<SelectListItem>()
            {
                new SelectListItem("Mission Post", "1"),
                new SelectListItem("Personal Log", "2"),
                new SelectListItem("Fiction Chapter", "3")
            };

        public int Id { get; set; }
    }
}