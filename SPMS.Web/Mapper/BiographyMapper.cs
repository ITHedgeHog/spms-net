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
    public class BiographyMapper : Profile
    {

        public BiographyMapper()
        {
            CreateMap<Biography, BiographyViewModel>()
                .ForMember(x => x.Status, opt => opt.MapFrom(y => y.Status.Name))
                .ForMember(x => x.Player, opt => opt.MapFrom(y => y.Player.DisplayName))
                .ForMember(x => x.Posting, opt => opt.MapFrom(y => y.Posting.Name))
                .ForMember(x => x.IsReadOnly, opt => opt.Ignore())
                .ForMember(x => x.SiteDisclaimer, opt => opt.Ignore())
                .ForMember(x => x.SiteTitle, opt => opt.Ignore())
                .ForMember(x => x.GameName, opt => opt.Ignore());

        }
    }
}
