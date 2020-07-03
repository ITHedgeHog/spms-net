using System.Collections.Generic;
using SPMS.Application.Dtos.Common;

namespace SPMS.Application.Dtos
{
    public class EditBiographyDto : SPMS.Common.ViewModels.ViewModel
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

        public PlayerDto Player { get; set; }
        public string History { get; set; }

        public List<ListItemDto> Postings { get; set; }
        public List<ListItemDto> Statuses { get; set; }
        public int StatusId { get; set; }
        public int StateId { get; set; }
        public List<ListItemDto> States { get; set; }
    }
}