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
        public class GetBiographyQueryHandler : IRequestHandler<GetBiographyQuery, BiographyDto>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;
            private readonly IdentifierMasking _identifierMasker;

            public GetBiographyQueryHandler(ISpmsContext db, IMapper mapper, IdentifierMasking identifierMasker)
            {
                _db = db;
                _mapper = mapper;
                _identifierMasker = identifierMasker;
            }

            public async Task<BiographyDto> Handle(GetBiographyQuery request, CancellationToken cancellationToken)
            {
                var id = _identifierMasker.RevealId(request.Id);
                var biography = await _db.Biography.Include(x => x.Player).Include(x => x.State).Include(x => x.Posting).Include(x => x.Status).ProjectTo<BiographyDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(m => m.Id == id, cancellationToken: cancellationToken);

                if (biography == null)
                    throw new InvalidOperationException("Biography does not exist.");


                return biography;
            }


        }
    }
}
