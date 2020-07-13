using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Application.Services;

namespace SPMS.Application.Biography.Query
{
    public class GetBiographyQuery : IRequest<BiographyDto>
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public class GetBiographyQueryHandler : IRequestHandler<GetBiographyQuery, BiographyDto>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;
            private readonly ITenantProvider _tenant;

            public GetBiographyQueryHandler(ISpmsContext db, IMapper mapper, ITenantProvider tenant)
            {
                _db = db;
                _mapper = mapper;
                _tenant = tenant;
            }

            public async Task<BiographyDto> Handle(GetBiographyQuery request, CancellationToken cancellationToken)
            {
                var game = await _tenant.GetTenantAsync(cancellationToken);
                var id = await _tenant.UnprotectAsync(request.Id, cancellationToken);
                var biography = await _db.Biography.Include(x => x.Player).Include(x => x.State).Include(x => x.Posting).Include(x => x.Status).ProjectTo<BiographyDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(m => m.Id == id, cancellationToken: cancellationToken);

                if (biography == null)
                    throw new InvalidOperationException("Biography does not exist.");


                return biography;
            }


        }
    }
}
