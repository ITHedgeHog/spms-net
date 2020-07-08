using System.Linq;
using AutoMapper;
using SPMS.Application.Dtos.Story;
using SPMS.Domain.Models;

namespace SPMS.Application.Common.Mappings
{
    public class StoryMapping : Profile
    {
        public StoryMapping()
        {
            CreateMap<EpisodeEntry, StoryPostDto>()
                .ForMember(x => x.EpisodeEntryPlayer, o => o.MapFrom(y => string.Join(", ", y.EpisodeEntryPlayer.Select(z => z.Player.DisplayName))))
                .ForMember(x => x.PostedBy, o => o.MapFrom(y => string.Join(", ", y.EpisodeEntryPlayer.Select(z => z.Player.DisplayName))));

            CreateMap<Episode, EpisodeDto>()
                .ForMember(x => x.Description, o => o.Ignore())
                .ForMember(x => x.Banner, o => o.Ignore())
            .ForMember(x => x.Story, o => o.Ignore());

            CreateMap<Series, SeriesDto>()
                .ForMember(x => x.Description, o => o.Ignore());
        }
    }
}
