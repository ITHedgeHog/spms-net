namespace SPMS.Domain.Models
{
    public class GameUrl {
        public int Id { get; set; }
        public string Url { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; }
        public bool IsPrimary { get; set; }

    }
}