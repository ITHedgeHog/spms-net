using System.Collections.Generic;

namespace SPMS.Domain.Models
{
    public class PlayerRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlayerRolePlayer> PlayerRolePlayer { get; set; }

    }
}