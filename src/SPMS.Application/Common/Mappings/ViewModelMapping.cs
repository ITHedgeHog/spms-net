using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SPMS.Application.Character.Command;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Common;
using SPMS.ViewModel;
using SPMS.ViewModel.character;
using SPMS.ViewModel.Common;

namespace SPMS.Application.Common.Mappings
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<ListItemDto, SelectListItem>()
                .ForMember(x => x.Group, o => o.Ignore())
                .ForMember(x => x.Disabled, o => o.Ignore());
            CreateMap<SPMS.Application.Dtos.BiographyDto, BiographyViewModel>()
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
            CreateMap<PlayerRoleDto, PlayerRoleViewModel>();
            CreateMap<PlayerDto, PlayerViewModel>();
            CreateMap<EditBiographyDto, EditCharacterViewModel>()
                .ForMember(x => x.Types, o=>o.Ignore())
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
                //.ForMember(x => x.Statuses, o => o.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                ;
        }
    }
}
