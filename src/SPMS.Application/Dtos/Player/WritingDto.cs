using System;
using System.Collections.Generic;
using System.Text;

namespace SPMS.Application.Dtos.Player
{
    public class WritingPortalDto
    {
        public WritingPortalDto()
        {
            DraftPosts = new List<PostDto>();
            PendingPosts = new List<PostDto>();
        }

        public bool IsCreateCharacterEnabled { get; set; }

        public List<PostDto> DraftPosts { get; set; }

        public bool HasEpisode { get; set; }
        public List<PostDto> PendingPosts { get; set; }
    }


    public class PostDto
    {
        public PostDto()
        {
            Authors = new List<AuthorDto>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Timeline { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string LastAuthor { get; set; }
        public List<AuthorDto> Authors { get; set; }
        public string Type { get; set; }
    }

    public class AuthorDto
    {

        public AuthorDto()
        {

        }
        public AuthorDto(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public AuthorDto(int id, int episodeId, string name, string email)
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
