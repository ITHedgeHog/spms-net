using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Application.Services;
using SPMS.Common.BaseObjects;

namespace SPMS.Application.Biography.Query
{
    public class BiographyQuery : QueryWithId, IRequest<BiographyDto>
    {
        public class GetBiographyQueryHandler : IRequestHandler<BiographyQuery, BiographyDto>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;
            private readonly ITenantAccessor<TenantDto> _tenant;

            public GetBiographyQueryHandler(ISpmsContext db, IMapper mapper, ITenantAccessor<TenantDto> tenant)
            {
                _db = db;
                _mapper = mapper;
                _tenant = tenant;
            }

            public async Task<BiographyDto> Handle(BiographyQuery request, CancellationToken cancellationToken)
            {
                var game = _tenant.Instance;
                var biography = await _db.Biography
                    .Include(x => x.Player)
                    .Include(x => x.State)
                    .Include(x => x.Posting)
                    .Include(x => x.Status)
                    .Include(x => x.Type)
                    .Where(m => m.Id == request.Id && m.Posting.GameId == game.Id)
                    .ProjectTo<BiographyDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync( cancellationToken: cancellationToken);

                if (biography == null)
                    throw new InvalidOperationException("Biography does not exist.");


                return biography;
            }


        }
    }
}
