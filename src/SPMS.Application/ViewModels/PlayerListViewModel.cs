using System.Collections.Generic;
using SPMS.Application.ViewModels.Biography;
using SPMS.Common.ViewModels;

namespace SPMS.Application.ViewModels
{
    public class PlayerListViewModel : ViewModel
    {
        public List<PlayerViewModel> Players { get; set; }
    }

}
