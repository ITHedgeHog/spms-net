using System.ComponentModel.DataAnnotations;

namespace SPMS.Web.Models
{
    public class EpisodeEntryType
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }
    }
}