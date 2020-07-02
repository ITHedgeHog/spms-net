using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Common;

namespace SPMS.Application.Biography.Query
{
    public class GetBiographyQuery : IRequest<BiographyDto>
    {
        public int Id { get; set; }
        public class GetBiographyQueryHandler : IRequestHandler<GetBiographyQuery, BiographyDto>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;

            public GetBiographyQueryHandler(ISpmsContext db, IMapper mapper)
            {
                _db = db;
                _mapper = mapper;
            }

            public async Task<BiographyDto> Handle(GetBiographyQuery request, CancellationToken cancellationToken)
            {
                var biography = await _db.Biography.Include(x => x.Player).Include(x => x.State).Include(x => x.Posting).ProjectTo<BiographyDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken);

                if (biography == null)
                    throw new InvalidOperationException("Biography does not exist.");


                return biography;
            }


        }
    }
}
