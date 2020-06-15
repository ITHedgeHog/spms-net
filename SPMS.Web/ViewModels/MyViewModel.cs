using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPMS.Web.ViewModels
{
    public class MyViewModel : ViewModel
    {
        public MyViewModel()
        {
            Characters = new Dictionary<int, string>();
        }

        public bool IsCreateCharacterEnabled { get; set; }

        public Dictionary<int, string> Characters { get; set; }
    }
}
