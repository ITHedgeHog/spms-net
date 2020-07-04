using System.Collections.Generic;

namespace SPMS.Application.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string AuthString { get; set; }
        public List<PlayerRoleDto> Roles { get; set; }
    }
}