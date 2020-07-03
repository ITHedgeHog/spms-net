using System.Collections.Generic;
using SPMS.Common.ViewModels;

namespace SPMS.Application.Dtos
{
    public class PlayerListViewModel : SPMS.Common.ViewModels.ViewModel
    {
        public List<PlayerDto> Players { get; set; }
    }

}
