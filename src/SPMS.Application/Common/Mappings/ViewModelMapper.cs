using AutoMapper;
using SPMS.ViewModel;

namespace SPMS.Application.Common.Mappings
{
    public class WebMapping : Profile
    {
        public WebMapping()
        {
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
                .ForMember(x => x.GameName, o => o.Ignore())
                ;
        }
    }
}
