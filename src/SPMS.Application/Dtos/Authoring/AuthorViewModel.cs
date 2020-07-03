namespace SPMS.Application.Dtos.Authoring
{
    public class AuthorViewModel
    {

        public AuthorViewModel()
        {

        }
        public AuthorViewModel(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public AuthorViewModel(int id, int episodeId, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
            EpisodeEntryId = episodeId;
        }

        public int Id { get; set; }
        public int EpisodeEntryId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}