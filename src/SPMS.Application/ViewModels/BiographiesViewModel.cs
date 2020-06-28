using System.Collections.Generic;
using SPMS.Domain.Models;

namespace SPMS.Application.ViewModels
{
    public class BiographiesViewModel : ViewModel
    {
        public List<Posting> Postings { get; set; }

        public List<Domain.Models.Biography> Biographies { get; set; }
    }
}
