namespace SPMS.Domain.Models
{
    public class LookupTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Default { get; set; }
        public int? GameId { get; set; }
        public virtual Game Game { get; set; }

    }
}