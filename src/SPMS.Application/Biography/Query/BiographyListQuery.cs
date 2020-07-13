using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Domain.Models;

namespace SPMS.Application.Biography.Query
{
    public class BiographyListQuery : IRequest<BiographiesDto>
    {
        public string Url { get; set; }

        public class BiographyListQueryHandler : IRequestHandler<BiographyListQuery, BiographiesDto>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;
            private readonly ITenantProvider _tenant;

            public BiographyListQueryHandler(ISpmsContext db, IMapper mapper, ITenantProvider tenant)
            {
                _db = db;
                _mapper = mapper;
                _tenant = tenant;
            }

            public async Task<BiographiesDto> Handle(BiographyListQuery request, CancellationToken cancellationToken)
            {
                var game = await _tenant.GetTenantAsync(request.Url, cancellationToken);
                //var bio = await _db.Biography.Include(x => x.State).Include(x => x.Status).ToListAsync(cancellationToken: cancellationToken);
                //var bioDto = await _db.Biography.Include(x => x.State)
                //    .Include(x => x.Status)
                //    .Where(x => x.Posting.GameId == game.Id)
                //    .ProjectTo<Application.Dtos.BiographyDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken: cancellationToken);
                var dto = new BiographiesDto
                {
                    Postings = await _db.Posting.Where(x => x.Name != "Undefined").OrderBy(x => x.Name).ToListAsync(cancellationToken: cancellationToken),
                    Biographies = await _db.Biography.Include(x => x.State).Include(x => x.Status).Include(x => x.Posting).Where(x => x.Posting.GameId == game.Id).ProjectTo<Application.Dtos.BiographyDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken: cancellationToken)
                };

                return dto;
            }
        }
    }
}
