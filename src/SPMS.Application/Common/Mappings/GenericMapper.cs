using System.Linq;
using AutoMapper;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Admin;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Mappings
{
    public class GenericMapper : Profile
    {
        public GenericMapper()
        {

            CreateMap<Game, SeoDto>();

            CreateMap<PlayerRole, PlayerRoleDto>().ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id));
            CreateMap<Player, PlayerDto>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(y => y.Roles.Select(z => new PlayerRoleDto(){ Id = z.PlayerRole.Id, Name = z.PlayerRole.Name})));
            CreateMap<PlayerDto, Player>()
                .ForMember(p=>p.Roles, opt=>opt.Ignore())
                .ForMember(p=>p.EpisodeEntries, o => o.Ignore())
                .ForMember(x => x.Connections, o=>o.Ignore())
                .ForMember(x=>x.Email, o=>o.Ignore())
                .ForMember(x => x.Firstname, o => o.Ignore())
                .ForMember(x => x.Surname, o => o.Ignore());

        }
    }
}
