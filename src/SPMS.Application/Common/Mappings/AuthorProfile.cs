using System;
using System.Linq;
using AutoMapper;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Authoring;
using SPMS.Application.Dtos.Story;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
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
                .ForMember(x => x.CreatedBy, o => o.Ignore());

            CreateMap<AuthorViewModel, EpisodeEntryPlayer>()
                .ForMember(x => x.PlayerId, o => o.MapFrom(y => y.Id))
                .ForMember(X => X.EpisodeEntryId, o => o.Ignore())
                .ForMember(X => X.Player, o => o.Ignore())
                .ForMember(X => X.EpisodeEntry, o => o.Ignore());

            CreateMap<EpisodeEntry, PostViewModel>()
                .ForMember(x => x.Authors, o => o.MapFrom(x => x.EpisodeEntryPlayer.Select(y => new AuthorViewModel() { Id = y.PlayerId, Name = y.Player.DisplayName })))
                .ForMember(x => x.LastAuthor, o => o.Ignore())
                .ForMember(x => x.CreatedAt, o => o.Ignore())
                .ForMember(x => x.UpdatedAt, o => o.Ignore());

            CreateMap<Player, AuthorToInviteViewModel>()
                .ForMember(x => x.IsSelected, o => o.Ignore())
                .ForMember(x=>x.Name, o => o.MapFrom(y =>y.DisplayName));
        }
    }


}