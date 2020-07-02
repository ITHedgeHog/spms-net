using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SPMS.Application.Dtos
{
    public class CreateBiographyViewModel : SPMS.Common.ViewModels.ViewModel
    {
        public CreateBiographyViewModel()
        {
            Status = new BiographyStatusViewModel();
            Statuses = new List<SelectListItem>();
            Postings = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }

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

        public BiographyStatusViewModel Status { get; set; }

        public int PostingId { get; set; }
        public string Posting { get; set; }

        public int PlayerId { get; set; }
        public PlayerViewModel Player { get; set; }
        public string History { get; set; }

        public List<SelectListItem> Postings { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }

        public int StateId { get; set; }
        public IEnumerator<SelectListItem> States { get; set; }
    }

    public class BiographyStatusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class EditBiographyViewModel : SPMS.Common.ViewModels.ViewModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int PlayerId { get; set; }

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

        public string State { get; set; }

        public int PostingId { get; set; }
        public string Posting { get; set; }

        public PlayerViewModel Player { get; set; }
        public string History { get; set; }

        public List<SelectListItem> Postings { get; set; }
        public List<SelectListItem> Statuses { get; set; }
        public int StatusId { get; set; }
        public int StateId { get; set; }
        public IEnumerator<SelectListItem> States { get; set; }
    }
}
