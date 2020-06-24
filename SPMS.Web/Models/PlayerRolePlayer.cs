using System.ComponentModel.DataAnnotations;

namespace SPMS.Web.Models
{
    public class PlayerRolePlayer
    {
        [Key]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        [Key]
        public int PlayerRoleId { get; set; }
        public PlayerRole PlayerRole { get; set; }
    }
}