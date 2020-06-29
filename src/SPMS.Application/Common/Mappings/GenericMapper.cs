using System.Linq;
using AutoMapper;
using SPMS.Application.ViewModels;
using SPMS.Application.ViewModels.Biography;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Mappings
{
    public class GenericMapper : Profile
    {
        public GenericMapper()
        {
           
            CreateMap<PlayerRole, PlayerRoleViewModel>().ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id));
            CreateMap<Player, PlayerViewModel>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(y => y.Roles.Select(z => new PlayerRoleViewModel(){ Id = z.PlayerRole.Id, Name = z.PlayerRole.Name})));
            CreateMap<PlayerViewModel, Player>()
                .ForMember(p=>p.Roles, opt=>opt.Ignore())
                .ForMember(p=>p.EpisodeEntries, o => o.Ignore())
                .ForMember(x => x.Connections, o=>o.Ignore())
                .ForMember(x=>x.Email, o=>o.Ignore());
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
