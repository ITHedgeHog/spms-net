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


    }
}