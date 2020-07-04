using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SPMS.Application.Dtos
{
    public class CreateBiographyViewModel : SPMS.Common.ViewModels.ViewModel
    {
        public CreateBiographyViewModel()
        {
            Status = new List<SelectListItem>();
            Statuses = new List<SelectListItem>();
            Postings = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }
        public string Species { get; set; }
        public string Homeworld { get; set; }
        public string Gender { get; set; }
        public string Born { get; set; }
        public string Eyes { get; set; }
        public string Hair { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Affiliation { get; set; }
        public string Assignment { get; set; }
        public string Rank { get; set; }
        public string RankImage { get; set; }

        public List<SelectListItem> Status { get; set; }

        [Display(Name = "Posting")]
        public int PostingId { get; set; }
        public string Posting { get; set; }
        [Display(Name = "Played by")]
        public int PlayerId { get; set; }
        public PlayerDto Player { get; set; }
        public string History { get; set; }

        public List<SelectListItem> Postings { get; set; }
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public List<SelectListItem> Statuses { get; set; }
        [Display(Name="State")]
        public int StateId { get; set; }
        public List<SelectListItem> States { get; set; }
    }
}
