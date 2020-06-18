using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPMS.Web.ViewModels;
using SPMS.Web.ViewModels.Biography;

namespace SPMS.Web.Areas.Admin.ViewModels
{
    public class PlayerListViewModel : ViewModel
    {
        public List<PlayerViewModel> Players { get; set; }
    }

}
