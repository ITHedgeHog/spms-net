using System.ComponentModel.DataAnnotations;

namespace SPMS.Web.Models
{
    public class BiographyStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}