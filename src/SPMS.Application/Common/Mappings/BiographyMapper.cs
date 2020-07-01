using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SPMS.Application.ViewModels;
using SPMS.Application.ViewModels.Biography;
using SPMS.Domain.Models;
using BiographyDto = SPMS.Application.ViewModels.Biography.BiographyDto;

namespace SPMS.Application.Common.Mappings
{
    public class BiographyMapper : Profile
    {

        public BiographyMapper()
        {
            CreateMap<CreateBiographyViewModel, Biography>()
                .ForMember(x => x.State, opt => opt.Ignore())
                .ForMember(x => x.Player, opt => opt.Ignore())
                .ForMember(x => x.Posting, opt => opt.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.State, o => o.Ignore());
            CreateMap<EditBiographyViewModel, Biography>()
                .ForMember(x => x.State, opt => opt.Ignore())
                .ForMember(x => x.Player, opt => opt.Ignore())
                .ForMember(x => x.Posting, opt => opt.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.State, o => o.Ignore());


            CreateMap<Biography, CreateBiographyViewModel>()
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
                .ForMember(x => x.Player, o => o.MapFrom(y => new PlayerViewModel() { Id = y.Player.Id, AuthString = y.Player.AuthString, DisplayName = y.Player.DisplayName, Roles = y.Player.Roles.Select(z => new PlayerRoleViewModel() { Id = z.PlayerRoleId, Name = z.PlayerRole.Name }).ToList() }))
                .ForMember(x => x.States, o => o.Ignore());

            CreateMap<Biography, EditBiographyViewModel>()
                .ForMember(x => x.Posting, opt => opt.MapFrom(y => y.Posting.Name))
                .ForMember(x => x.Player,
                    opt => opt.MapFrom(y => new PlayerViewModel()
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
                .ForMember(x => x.States, o => o.Ignore());


            CreateMap<Biography, BiographyDto>()
                .ForMember(x => x.Status, opt => opt.MapFrom(y => y.State.Name))
                .ForMember(x => x.Player, opt => opt.MapFrom(y => y.Player.DisplayName))
                .ForMember(x => x.Posting, opt => opt.MapFrom(y => y.Posting.Name))
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
                .ForMember(x => x.CommitShaLink, o => o.Ignore());


            CreateMap<Biography, ViewModels.BiographyDto>();

        }
    }
}
