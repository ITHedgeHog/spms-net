using System.Collections.Generic;
using SPMS.Application.Dtos.Authoring;

namespace SPMS.Application.Dtos
{
    public class InviteAuthorViewModel : SPMS.Common.ViewModels.BaseViewModel
    {
        public InviteAuthorViewModel()
        {
            ExistingAuthors = new List<AuthorViewModel>();
            Authors = new List<AuthorToInviteViewModel>();
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public string nextaction { get; set; }

        public List<AuthorViewModel> ExistingAuthors { get; set; }
        public List<AuthorToInviteViewModel> Authors { get; set; }
    }

    public class AuthorToInviteViewModel
    {
        public AuthorToInviteViewModel()
        {
            
        }

        public AuthorToInviteViewModel(int id, string name, string email, bool selected)
        {
            Id = id;
            Name = name;
            Email = email;
            IsSelected = selected;
        }

        public bool IsSelected { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
