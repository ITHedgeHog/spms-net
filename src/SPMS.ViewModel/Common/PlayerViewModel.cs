using System;
using System.Collections.Generic;
using System.Text;

namespace SPMS.ViewModel.Common
{
    public class PlayerViewModel
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string AuthString { get; set; }
        public List<PlayerRoleViewModel> Roles { get; set; }
    }

    public class PlayerRoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
