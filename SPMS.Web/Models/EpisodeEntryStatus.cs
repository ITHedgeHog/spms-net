using System.ComponentModel.DataAnnotations;

namespace SPMS.Web.Models
{
    public class EpisodeEntryStatus
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
    }
}