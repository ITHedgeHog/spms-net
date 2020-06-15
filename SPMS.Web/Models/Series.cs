using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SPMS.Web.Models
{
    public class Series
    {
        public int Id { get; set; }
        
        [MaxLength(200)]
        public string Title { get; set; }

        public Collection<Episode> Episodes { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
        
    }
}