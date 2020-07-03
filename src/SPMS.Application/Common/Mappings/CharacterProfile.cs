using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SPMS.Application.Character.Command;
using SPMS.Application.Dtos;

namespace SPMS.Application.Common.Mappings
{
    public class CharacterMapperProfile : Profile
    {
        public CharacterMapperProfile()
        {
            CreateMap<BiographyDto, UpdateCharacterCommand>();

            CreateMap<UpdateCharacterCommand, Domain.Models.Biography>()
                .ForMember(x => x.State, o => o.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.Posting, o => o.Ignore())
                .ForMember(x => x.Player, o => o.Ignore());

            CreateMap<EditBiographyDto, UpdateCharacterCommand>()
                .ForMember(x => x.State, o => o.Ignore())
                .ForMember(x => x.Status, o => o.Ignore())
                .ForMember(x => x.Posting, o => o.Ignore())
                .ForMember(x => x.Player, o => o.Ignore());
        }
    }
}
