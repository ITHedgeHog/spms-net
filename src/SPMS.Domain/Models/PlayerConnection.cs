using System;

namespace SPMS.Domain.Models
{
    public class PlayerConnection
    {
        public string ConnectionId { get; set; }
        public string UserAgent { get; set; }
        public bool Connected { get; set; }
        public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
        public int PlayerId { get; set; }
        public Player Player { get; set; }

    }
}