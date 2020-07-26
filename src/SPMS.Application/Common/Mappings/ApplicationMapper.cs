using System.Linq;
using AutoMapper;
using SPMS.Application.Character.Command;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Admin;
using SPMS.Application.Dtos.Authoring;
using SPMS.Application.Dtos.Common;
using SPMS.Application.Dtos.Story;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Mappings
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            GenericMappings();
            AuthorMappings();
            BiographyMapper();
            CharacterMappings();
            StoryMappings();
        }

        public void GenericMappings()
        {
            CreateMap<Game, SeoDto>();
            CreateMap<Game, TenantDto>()
                .ForMember(x => x.GameName, o => o.MapFrom(y => y.Name))
                .ForMember(x => x.gravatar, o => o.Ignore())
                .ForMember(x => x.SiteDisclaimer, o => o.MapFrom(y => y.Disclaimer))
                .ForMember(x => x.UseAnalytics, o => o.MapFrom(y => !string.IsNullOrEmpty(y.SiteAnalytics)))
                .ForMember(x => x.IsAdmin, o => o.Ignore())
                .ForMember(x => x.IsPlayer, o => o.Ignore())
                .ForMember(x => x.CommitShaLink, o => o.Ignore())
                .ForMember(x => x.CommitSha, o => o.Ignore());

            CreateMap<TenantDto, SeoDto>();


            CreateMap<PlayerRole, PlayerRoleDto>().ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id));
            CreateMap<Player, PlayerDto>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(y => y.Roles.Select(z => new PlayerRoleDto() { Id = z.PlayerRole.Id, Name = z.PlayerRole.Name })));
            CreateMap<PlayerDto, Player>()
                .ForMember(p => p.Roles, opt => opt.Ignore())
                .ForMember(p => p.EpisodeEntries, o => o.Ignore())
                .ForMember(x => x.Connections, o => o.Ignore())
                .ForMember(x => x.Email, o => o.Ignore())
                .ForMember(x => x.Firstname, o => o.Ignore())
                .ForMember(x => x.Surname, o => o.Ignore());
        }


        public void AuthorMappings()
        {

            CreateMap<AuthorPostDraftViewModel, AuthorPostViewModel>();

            CreateMap<EpisodeEntry, AuthorPostViewModel>()
                .ForMember(x => x.IsReadOnly, opt => opt.Ignore())
                .ForMember(x => x.SiteDisclaimer, opt => opt.Ignore())
                .ForMember(x => x.SiteTitle, opt => opt.Ignore())
                .ForMember(x => x.GameName, opt => opt.Ignore())
                .ForMember(x => x.UseAnalytics, opt => opt.Ignore())
                .ForMember(x => x.SiteAnalytics, opt => opt.Ignore())
                .ForMember(x => x.IsAdmin, opt => opt.Ignore())
                .ForMember(x => x.IsPlayer, opt => opt.Ignore())
                .ForMember(x => x.PostTypes, opt => opt.Ignore())
                .ForMember(x => x.TypeId, opt => opt.Ignore())
                .ForMember(x => x.Episode, opt => opt.MapFrom(x => x.Episode.Title))
                .ForMember(x => x.Authors, opt => opt.MapFrom(x => x.EpisodeEntryPlayer.Select(y => new AuthorViewModel(y.Player.Id, y.EpisodeEntryId, y.Player.DisplayName, y.Player.Email))))
                .ForMember(x => x.EpisodeId, opt => opt.MapFrom(x => x.EpisodeId))
                .ForMember(x => x.StatusId, o => o.MapFrom(y => y.EpisodeEntryStatusId))
                .ForMember(x => x.Statuses, o => o.Ignore())
                .ForMember(x => x.submitpost, o => o.Ignore())
                .ForMember(x => x.PostAt, o => o.Ignore())
                .ForMember(x => x.gravatar, o => o.Ignore())
                .ForMember(x => x.CommitSha, o => o.Ignore())
                .ForMember(x => x.CommitShaLink, o => o.Ignore());


            CreateMap<AuthorPostViewModel, EpisodeEntry>()
                .ForMember(x => x.EpisodeEntryTypeId, opt => opt.MapFrom(x => x.TypeId))
                .ForMember(x => x.EpisodeEntryType, opt => opt.Ignore())
                .ForMember(x => x.EpisodeEntryPlayer, o => o.MapFrom(y => y.Authors))
                .ForMember(x => x.Episode, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.EpisodeEntryStatusId, o => o.MapFrom(y => y.StatusId))
                .ForMember(x => x.EpisodeEntryStatus, o => o.Ignore())
                .ForMember(x => x.PublishedAt, o => o.Ignore())
                .ForMember(x => x.LastModified, o => o.Ignore())
                .ForMember(x => x.LastModifiedBy, o => o.Ignore())
                .ForMember(x => x.CreatedBy, o => o.Ignore())
                .ForMember(x=>x.IsPostedToDiscord, o=>o.Ignore());

            CreateMap<AuthorViewModel, EpisodeEntryPlayer>()
                .ForMember(x => x.PlayerId, o => o.MapFrom(y => y.Id))
                .ForMember(X => X.EpisodeEntryId, o => o.Ignore())
                .ForMember(X => X.Player, o => o.Ignore())
                .ForMember(X => X.EpisodeEntry, o => o.Ignore());

            CreateMap<EpisodeEntry, PostViewModel>()
                .ForMember(x => x.Authors, o => o.MapFrom(x => x.EpisodeEntryPlayer.Select(y => new AuthorViewModel() { Id = y.PlayerId, Name = y.Player.DisplayName })))
                .ForMember(x => x.LastAuthor, o => o.MapFrom(y => y.LastModifiedBy))
                .ForMember(x => x.CreatedAt, o => o.MapFrom(y => y.Created))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(y => y.LastModified))
                .ForMember(x => x.PublishedAt, o => o.MapFrom(y => y.PublishedAt));

            CreateMap<Player, AuthorToInviteViewModel>()
                .ForMember(x => x.IsSelected, o => o.Ignore())
                .ForMember(x => x.Name, o => o.MapFrom(y => y.DisplayName));
        }

        public void BiographyMapper()
        {
            CreateMap<LookupTable, ListItemDto>()
                .ForCtorParam("value", o => o.MapFrom(y => y.Id.ToString()))
                .ForCtorParam("text", o => o.MapFrom(y => y.Name))
                .ForCtorParam("selected", o => o.MapFrom(y => y.Default))
                .ForAllMembers(x => x.Ignore());

            CreateMap<CreateBiographyViewModel, Domain.Models.Biography>()
                .ForMember(x => x.State, opt => opt.Ignore())
                .ForMember(x => x.Player, opt => opt.Ignore())
                .ForMember(x => x.Posting, opt => opt.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.State, o => o.Ignore())
                .ForMember(x => x.Type, o => o.Ignore())
                .ForMember(x => x.TypeId, o => o.Ignore()); ;
            CreateMap<EditBiographyDto, Domain.Models.Biography>()
                .ForMember(x => x.State, opt => opt.Ignore())
                .ForMember(x => x.Player, opt => opt.Ignore())
                .ForMember(x => x.Posting, opt => opt.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.State, o => o.Ignore())
                .ForMember(x => x.Type, o => o.Ignore())
                .ForMember(x => x.TypeId, o => o.Ignore());



            CreateMap<Domain.Models.Biography, CreateBiographyViewModel>()
                .ForMember(x => x.Posting, opt => opt.MapFrom(y => y.Posting.Name))
                .ForMember(x => x.IsReadOnly, opt => opt.Ignore())
                .ForMember(x => x.SiteDisclaimer, opt => opt.Ignore())
                .ForMember(x => x.SiteTitle, opt => opt.Ignore())
                .ForMember(x => x.Postings, opt => opt.Ignore())
                .ForMember(x => x.Statuses, opt => opt.Ignore())
                .ForMember(x => x.GameName, opt => opt.Ignore())
                .ForMember(x => x.UseAnalytics, opt => opt.Ignore())
                .ForMember(x => x.SiteAnalytics, opt => opt.Ignore())
                .ForMember(x => x.IsAdmin, opt => opt.Ignore())
                .ForMember(x => x.IsPlayer, opt => opt.Ignore())
                .ForMember(x => x.gravatar, o => o.Ignore())
                .ForMember(x => x.CommitSha, o => o.Ignore())
                .ForMember(x => x.CommitShaLink, o => o.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.Player, o => o.MapFrom(y => new PlayerDto() { Id = y.Player.Id, AuthString = y.Player.AuthString, DisplayName = y.Player.DisplayName, Roles = y.Player.Roles.Select(z => new PlayerRoleDto() { Id = z.PlayerRoleId, Name = z.PlayerRole.Name }).ToList() }))
                .ForMember(x => x.States, o => o.Ignore())
                ;

            CreateMap<Domain.Models.Biography, EditBiographyDto>()
                .ForMember(x => x.Posting, opt => opt.MapFrom(y => y.Posting.Name))
                .ForMember(x => x.Player,
                     opt => opt.MapFrom(y => new PlayerDto()
                     { Id = y.Player.Id, AuthString = y.Player.AuthString, DisplayName = y.Player.DisplayName }))
                .ForMember(x => x.Statuses, opt => opt.Ignore())
                .ForMember(x => x.Postings, opt => opt.Ignore())
                .ForMember(x => x.IsReadOnly, opt => opt.Ignore())
                .ForMember(x => x.SiteDisclaimer, opt => opt.Ignore())
                .ForMember(x => x.SiteTitle, opt => opt.Ignore())
                .ForMember(x => x.GameName, opt => opt.Ignore())
                .ForMember(x => x.UseAnalytics, opt => opt.Ignore())
                .ForMember(x => x.SiteAnalytics, opt => opt.Ignore())
                .ForMember(x => x.IsAdmin, opt => opt.Ignore())
                .ForMember(x => x.IsPlayer, opt => opt.Ignore())
                .ForMember(x => x.gravatar, o => o.Ignore())
                .ForMember(x => x.CommitSha, o => o.Ignore())
                .ForMember(x => x.CommitShaLink, o => o.Ignore())
                .ForMember(x => x.State, o => o.MapFrom(y => y.State.Name))
                .ForMember(x => x.States, o => o.Ignore())
                .ForMember(x => x.Types, o => o.Ignore());


            CreateMap<Domain.Models.Biography, BiographyDto>()
                .ForMember(x => x.State, opt => opt.MapFrom(y => y.State.Name))
                .ForMember(x => x.Player, opt => opt.MapFrom(y => y.Player.DisplayName))
                .ForMember(x => x.Posting, opt => opt.MapFrom(y => y.Posting.Name))
                .ForMember(x => x.Status, o => o.MapFrom(y => y.Status.Name));

        }

        public void CharacterMappings()
        {
            CreateMap<BiographyDto, UpdateCharacterCommand>()
                .ForMember(x => x.Type, o => o.Ignore())
                ;

            CreateMap<UpdateCharacterCommand, Domain.Models.Biography>()
                .ForMember(x => x.State, o => o.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.Posting, o => o.Ignore())
                .ForMember(x => x.Player, o => o.Ignore())
                .ForMember(x => x.Type, o => o.Ignore());

            CreateMap<EditBiographyDto, UpdateCharacterCommand>()
                .ForMember(x => x.State, o => o.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.Posting, o => o.Ignore())
                .ForMember(x => x.Player, o => o.Ignore())
                .ForMember(x => x.Type, o => o.Ignore());
        }

        public void StoryMappings()
        {
            CreateMap<EpisodeEntry, StoryPostDto>()
                .ForMember(x => x.EpisodeEntryPlayer, o => o.MapFrom(y => string.Join(", ", y.EpisodeEntryPlayer.Select(z => z.Player.DisplayName))))
                .ForMember(x => x.PostedBy, o => o.MapFrom(y => string.Join(", ", y.EpisodeEntryPlayer.Select(z => z.Player.DisplayName))));

            CreateMap<Episode, EpisodeDto>()
                .ForMember(x => x.Description, o => o.Ignore())
                .ForMember(x => x.Banner, o => o.Ignore())
                .ForMember(x => x.Story, o => o.Ignore());

            CreateMap<Series, SeriesDto>()
                .ForMember(x => x.Description, o => o.Ignore());
        }
    }
}
