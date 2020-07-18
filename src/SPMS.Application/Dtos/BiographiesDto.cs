using System.Collections.Generic;
using SPMS.Domain.Models;

namespace SPMS.Application.Dtos
{
    public class BiographiesDto : SPMS.Common.ViewModels.BaseViewModel
    {
        public List<Posting> Postings { get; set; }

        public List<BiographyDto> Biographies { get; set; }
    }
}
