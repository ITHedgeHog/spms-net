using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SPMS.Web.Models;
using SPMS.Web.ViewModels;
using SPMS.Web.ViewModels.Biography;

namespace SPMS.Web.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Biography, CreateBiographyViewModel>()
                .ForMember(x => x.Posting, opt => opt.MapFrom(y => y.Posting.Name))
                .ForMember(x => x.IsReadOnly, opt => opt.Ignore())
                .ForMember(x => x.SiteDisclaimer, opt => opt.Ignore())
                .ForMember(x => x.SiteTitle, opt => opt.Ignore())
                .ForMember(x => x.Postings, opt => opt.Ignore())
                .ForMember(x => x.Statuses, opt => opt.Ignore())
                .ForMember(x => x.GameName, opt => opt.Ignore()); 
            CreateMap<Biography, EditBiographyViewModel>()
                .ForMember(x => x.Posting, opt => opt.MapFrom(y => y.Posting.Name))
                .ForMember(x => x.Player,
                    opt => opt.MapFrom(y => new PlayerViewModel()
                        {Id = y.Player.Id, AuthString = y.Player.AuthString, DisplayName = y.Player.DisplayName}))
                .ForMember(x => x.Statuses, opt => opt.Ignore())
                .ForMember(x => x.Postings, opt => opt.Ignore())
                .ForMember(x => x.IsReadOnly, opt => opt.Ignore())
                .ForMember(x => x.SiteDisclaimer, opt => opt.Ignore())
                .ForMember(x => x.SiteTitle, opt => opt.Ignore())
                .ForMember(x => x.GameName, opt => opt.Ignore());
            
            CreateMap<PlayerRole, PlayerRoleViewModel>().ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id));
            CreateMap<Player, PlayerViewModel>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(y => y.Roles.Select(z => new PlayerRoleViewModel(){ Id = z.PlayerRole.Id, Name = z.PlayerRole.Name})));
            CreateMap<PlayerViewModel, Player>()
                .ForMember(p=>p.Roles, opt=>opt.Ignore());
            CreateMap<CreateBiographyViewModel, Biography>()
                .ForMember(x=>x.Status, opt => opt.Ignore())
                .ForMember(x => x.Player, opt=>opt.Ignore())
                .ForMember(x=>x.Posting, opt => opt.Ignore());
            CreateMap<EditBiographyViewModel, Biography>()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Player, opt => opt.Ignore())
                .ForMember(x => x.Posting, opt => opt.Ignore());


        }
    }
}
