using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPMS.Application.ViewModels;
using SPMS.Common.ViewModels;

namespace SPMS.Web.Areas.Admin.ViewModels
{
    public class PlayerEditViewModel : ViewModel
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string AuthString { get; set; }

        public List<PlayerRoleViewModel> Roles { get; set; }
    }
}
