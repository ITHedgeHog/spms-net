using System.Collections.Generic;

namespace SPMS.ViewModel
{
    public class BiographyListViewModel : SPMS.Common.ViewModels.BaseViewModel
    {
        public List<PostingViewModel> Postings { get; set; }

        public List<BiographyViewModel> Biographies { get; set; }
    }

    public class PostingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Default { get; set; }
        public int? GameId { get; set; }
        public string Game { get; set; }
        public bool IsPlayable { get; set; }
    }
}