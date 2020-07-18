using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using AutoMapper;
using SPMS.Application.Biography.Query;
using SPMS.Application.Common.Resolvers;
using SPMS.Application.Dtos;
using SPMS.ViewModel;

namespace SPMS.Application.Common.Mappings
{
    public class QueryMappings : Profile
    {
        public QueryMappings()
        {
            BiographyMappings();
        }

        private void BiographyMappings()
        {
            CreateMap<BiographyViewModel, BiographyQuery>()
                .ForMember(x => x.Id, o => o.MapFrom<VmToQueryIdResolver>());
        }

    }
}
