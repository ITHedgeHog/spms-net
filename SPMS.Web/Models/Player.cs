using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SPMS.Web.Models
{
    public class Player
    {
        public Player()
        {
            Roles = new List<PlayerRolePlayer>();
            EpisodeEntries = new List<EpisodeEntryPlayer>();
            Connections = new List<PlayerConnection>();
        }

        [Key]
        public int Id { get; set; }

        public string DisplayName { get; set; }

        public string AuthString { get; set; }

        public ICollection<PlayerRolePlayer> Roles { get; set; }

        public ICollection<EpisodeEntryPlayer> EpisodeEntries { get; set; }

        public ICollection<PlayerConnection> Connections { get; set; }
        public string Email { get; set; }
    }
}