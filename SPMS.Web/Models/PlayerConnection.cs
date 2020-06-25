using System;
using System.ComponentModel.DataAnnotations;

namespace SPMS.Web.Models
{
    public class PlayerConnection
    {
        [Key]
        public string ConnectionId { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
        public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
        public int PlayerId { get; set; }
        public Player Player { get; set; }

    }
}