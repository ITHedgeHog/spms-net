using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SPMS.Application.ViewModels.Biography;
using SPMS.Web.ViewModel;

namespace SPMS.Web.Mapping
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {
            CreateMap<BiographyDto, BiographyViewModel>();
        }
    }
}
