using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SPMS.Application.Dtos;
using SPMS.Domain.Models;
using BiographyDto = SPMS.Application.Dtos.BiographyDto;

namespace SPMS.Application.Common.Mappings
{
    public class BiographyMapperProfile : Profile
    {

        public BiographyMapperProfile()
        {
            CreateMap<CreateBiographyViewModel, Domain.Models.Biography>()
                .ForMember(x => x.State, opt => opt.Ignore())
                .ForMember(x => x.Player, opt => opt.Ignore())
                .ForMember(x => x.Posting, opt => opt.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.State, o => o.Ignore())
                .ForMember(x => x.Type, o=>o.Ignore())
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
    }
}
