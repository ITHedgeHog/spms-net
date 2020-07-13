using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Character.Query;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Admin;
using SPMS.Application.Dtos.Common;
using SPMS.Application.Services;
using SPMS.Common;

namespace SPMS.Application.Admin.Query
{
    public class SeoQuery : IRequest<SeoDto>
    {
        public string Url { get; set; }

        public class SeoQueryHandler : IRequestHandler<SeoQuery, SeoDto>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;
            private readonly ITenantProvider _tenant;

            public SeoQueryHandler(ISpmsContext db, IMapper mapper, ITenantProvider tenant)
            {
                _db = db;
                _mapper = mapper;
                _tenant = tenant;
            }

            public async Task<SeoDto> Handle(SeoQuery request, CancellationToken cancellationToken)
            {

                // Get Matching Game
                var game = await _tenant.GetTenantAsync(cancellationToken);


                var seoDto = _mapper.Map<SeoDto>(game);
                
                //var seoDto = new SeoDto();
                //seoDto.Author = game.Author;
                //seoDto.Description = game.Description;
                //seoDto.IsSpiderable = game.IsSpiderable ?? true;
                //seoDto.Keywords = game.Keywords;
                //seoDto.RobotsText = game.RobotsText;

                return seoDto;
            }
        }
    }
}