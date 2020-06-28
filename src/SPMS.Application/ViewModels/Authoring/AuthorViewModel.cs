namespace SPMS.Application.ViewModels.Authoring
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

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}