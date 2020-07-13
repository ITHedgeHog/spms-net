using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SPMS.Application.Character.Command;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Common;
using SPMS.Application.Dtos.Story;
using SPMS.Application.Services;
using SPMS.Domain.Models;
using SPMS.ViewModel;
using SPMS.ViewModel.character;
using SPMS.ViewModel.Common;
using SPMS.ViewModel.Story;

namespace SPMS.Application.Common.Mappings
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<TenantDto, SPMS.Common.ViewModels.ViewModel>();

            CreateMap<ListItemDto, SelectListItem>()
                .ForMember(x => x.Group, o => o.Ignore())
                .ForMember(x => x.Disabled, o => o.Ignore());
            CreateMap<Posting, PostingViewModel>();
            CreateMap<SPMS.Application.Dtos.BiographyDto, BiographyViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom<DtoIdHiderResolver>())
                .ForMember(x => x.gravatar, o => o.Ignore())
                .ForMember(x => x.IsReadOnly, o => o.Ignore())
                .ForMember(x => x.SiteAnalytics, o => o.Ignore())
                .ForMember(x => x.SiteDisclaimer, o => o.Ignore())
                .ForMember(x => x.SiteTitle, o => o.Ignore())
                .ForMember(x => x.UseAnalytics, o => o.Ignore())
                .ForMember(x => x.IsPlayer, o => o.Ignore())
                .ForMember(x => x.IsAdmin, o => o.Ignore())
                .ForMember(x => x.CommitSha, o => o.Ignore())
                .ForMember(x => x.CommitShaLink, o => o.Ignore())
                .ForMember(x => x.GameName, o => o.Ignore());
            CreateMap<BiographiesDto, BiographyListViewModel>();

            CreateMap<PlayerRoleDto, PlayerRoleViewModel>();
            CreateMap<PlayerDto, PlayerViewModel>(); 
            CreateMap<EditBiographyDto, EditCharacterViewModel>()
                .ForMember(x => x.TypeId, o => o.MapFrom(y => y.Types.FirstOrDefault(x => x.Selected).Value))
                .ForMember(x => x.PostingId, o => o.MapFrom(y => y.Postings.FirstOrDefault(x => x.Selected).Value))
                .ForMember(x => x.StateId, o => o.MapFrom(y => y.States.FirstOrDefault(x => x.Selected).Value))
                .ForMember(x => x.StatusId, o => o.MapFrom(y => y.Statuses.FirstOrDefault(x => x.Selected).Value))
                .ForMember(x => x.gravatar, o => o.Ignore())
                .ForMember(x => x.IsReadOnly, o => o.Ignore())
                .ForMember(x => x.SiteAnalytics, o => o.Ignore())
                .ForMember(x => x.SiteDisclaimer, o => o.Ignore())
                .ForMember(x => x.SiteTitle, o => o.Ignore())
                .ForMember(x => x.UseAnalytics, o => o.Ignore())
                .ForMember(x => x.IsPlayer, o => o.Ignore())
                .ForMember(x => x.IsAdmin, o => o.Ignore())
                .ForMember(x => x.CommitSha, o => o.Ignore())
                .ForMember(x => x.CommitShaLink, o => o.Ignore())
                .ForMember(x => x.GameName, o => o.Ignore());


            CreateMap<EditCharacterViewModel, UpdateCharacterCommand>()
                .ForMember(x => x.Player, o => o.Ignore())
                .ForMember(x => x.Type, o => o.Ignore())
                .ForMember(x => x.Status, o => o.Ignore());

            CreateMap<StoryPostDto, StoryPostViewModel>()
                .ForMember(x => x.Id, o => o.MapFrom<IdHiderResolver>())
                .ForMember(x => x.NextPost, o => o.Ignore())
                .ForMember(x => x.LastPost, o => o.Ignore())
                .ForMember(x => x.gravatar, o => o.Ignore())
                .ForMember(x => x.IsReadOnly, o => o.Ignore())
                .ForMember(x => x.SiteAnalytics, o => o.Ignore())
                .ForMember(x => x.SiteDisclaimer, o => o.Ignore())
                .ForMember(x => x.SiteTitle, o => o.Ignore())
                .ForMember(x => x.UseAnalytics, o => o.Ignore())
                .ForMember(x => x.IsPlayer, o => o.Ignore())
                .ForMember(x => x.IsAdmin, o => o.Ignore())
                .ForMember(x => x.CommitSha, o => o.Ignore())
                .ForMember(x => x.CommitShaLink, o => o.Ignore())
                .ForMember(x => x.GameName, o => o.Ignore());

            CreateMap<StoryPostViewModel, StoryPostDto>()
                .ForMember(x => x.Id, o => o.MapFrom<IdRevealerResolver>())
                .ForMember(x => x.EpisodeEntryStatusId, o => o.Ignore())
                .ForMember(x => x.EpisodeEntryStatus, o => o.Ignore())
                .ForMember(x => x.EpisodeEntryPlayer, o => o.Ignore())
                .ForMember(x => x.EpisodeEntryTypeId, o => o.Ignore())
                .ForMember(x => x.EpisodeEntryType, o => o.Ignore())
                .ForMember(x => x.EpisodeId, o => o.Ignore())
                .ForMember(x => x.Episode, o => o.Ignore())
                .ForMember(x=>x.PostedBy, o =>o.Ignore());
        }
    }


    public class DtoIdHiderResolver : IValueResolver<DtoWithId, ViewModelWithId, string>
    {
        private readonly IIdentifierMask _masker;

        public DtoIdHiderResolver(IIdentifierMask masker)
        {
            _masker = masker;
        }

        public string Resolve(DtoWithId source, ViewModelWithId destination, string destMember, ResolutionContext context)
        {
            return _masker.HideId(source.Id);
        }
    }

    public class IdHiderResolver : IValueResolver<StoryPostDto, StoryPostViewModel, string>
    {
        private readonly IIdentifierMask _masker;

        public IdHiderResolver(IIdentifierMask masker)
        {
            _masker = masker;
        }

        public string Resolve(StoryPostDto source, StoryPostViewModel destination, string destMember, ResolutionContext context)
        {
            return _masker.HideId(source.Id);
        }
    }

    public class IdRevealerResolver : IValueResolver<StoryPostViewModel, StoryPostDto, int>
    {
        private readonly IIdentifierMask _masker;

        public IdRevealerResolver(IIdentifierMask masker)
        {
            _masker = masker;
        }

        public int Resolve(StoryPostViewModel source,  StoryPostDto destination, int destMember, ResolutionContext context)
        {
            return _masker.RevealId(source.Id);
        }
    }
}
