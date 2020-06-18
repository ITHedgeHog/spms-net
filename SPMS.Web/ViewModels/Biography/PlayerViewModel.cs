using System.Collections.Generic;

namespace SPMS.Web.ViewModels.Biography
{
    public class PlayerViewModel
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string AuthString { get; set; }
        public List<string> Roles { get; set; }
    }
}