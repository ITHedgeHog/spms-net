using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SPMS.Web.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public Collection<GameUrl> Url { get; set; }
        [Required]
        public string SiteTitle { get; set; }
        [Required]
        public string Disclaimer { get; set; }
        public bool IsReadonly { get; set; }
        public string SiteAnalytics { get; set; }
    }

    public class GameUrl {
        [Key]
        public int Id { get; set; }
        [Required, DataType(DataType.Url)]
        public string Url { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }

    }
}