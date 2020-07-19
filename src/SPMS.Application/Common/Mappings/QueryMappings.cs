using AutoMapper;
using SPMS.Application.Biography.Query;
using SPMS.Application.Common.Resolvers;
using SPMS.Application.Dtos.Player;
using SPMS.Domain.Models;
using SPMS.ViewModel;

namespace SPMS.Application.Common.Mappings
{
    public class QueryMappings : Profile
    {
        public QueryMappings()
        {
            BiographyMappings();
            WritingPortalMappings();
        }

        private void BiographyMappings()
        {
            CreateMap<BiographyViewModel, BiographyQuery>()
                .ForMember(x => x.Id, o => o.MapFrom<VmToQueryIdResolver>());
        }

        private void WritingPortalMappings()
        {
            //CreateMap<Epis>()

            CreateMap<EpisodeEntry, PostDto>()
                .ForMember(x => x.CreatedAt, o => o.MapFrom(y => y.Created))
                .ForMember(x => x.UpdatedAt, o => o.MapFrom(y => y.LastModified))
                .ForMember(x => x.LastAuthor, o => o.MapFrom(y => y.LastModifiedBy))
                .ForMember(x => x.Authors, o => o.Ignore());
        }

    }
}
