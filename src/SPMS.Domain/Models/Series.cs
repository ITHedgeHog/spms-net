using System.Collections.Generic;

namespace SPMS.Domain.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Episode> Episodes { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
        
    }
}