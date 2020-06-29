using System.Collections.Generic;

namespace SPMS.Application.ViewModels.Biography
{
    public class PlayerViewModel
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string AuthString { get; set; }
        public List<PlayerRoleViewModel> Roles { get; set; }
    }
}