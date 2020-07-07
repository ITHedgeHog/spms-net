using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SPMS.Application.Dtos.Story;
using SPMS.Domain.Models;

namespace SPMS.Application.Tests.Mapping
{
    public class StoryMapping : Profile
    {
        public StoryMapping()
        {
            CreateMap<EpisodeEntry, StoryPostDto>()
                .ForMember(x => x.EpisodeEntryPlayer, o => o.MapFrom(y => string.Join(", ", y.EpisodeEntryPlayer.Select( z => z.Player.DisplayName))));
        }
    }
}
