namespace SPMS.Domain.Models
{
    public class PlayerRolePlayer
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int PlayerRoleId { get; set; }
        public PlayerRole PlayerRole { get; set; }
    }
}