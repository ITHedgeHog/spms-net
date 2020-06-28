using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPMS.Application.ViewModels;
using SPMS.Application.ViewModels.Biography;

namespace SPMS.Web.Areas.Admin.ViewModels
{
    public class PlayerListViewModel : ViewModel
    {
        public List<PlayerViewModel> Players { get; set; }
    }

}
