using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SPMS.Web.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string AuthString { get; set; }

        public Collection<PlayerRolePlayer> Roles { get; set; }
    }

    public class PlayerRolePlayer
    {
        [Key]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        [Key]
        public int PlayerRoleId { get; set; }
        public PlayerRole PlayerRole { get; set; }
    }

    public class PlayerRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlayerRolePlayer> PlayerRolePlayer { get; set; }

    }
}