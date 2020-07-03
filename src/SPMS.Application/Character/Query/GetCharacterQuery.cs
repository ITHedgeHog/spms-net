using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Dtos;
using SPMS.Application.Dtos.Common;

namespace SPMS.Application.Character.Query
{
    public class GetCharacterQuery : IRequest<EditBiographyDto>
    {
        public int Id { get; set; }

        public class GetCharacterQueryHandler : IRequestHandler<GetCharacterQuery, EditBiographyDto>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;
            private readonly IUserService _userService;


            public GetCharacterQueryHandler(ISpmsContext db, IMapper mapper, IUserService userService)
            {
                _db = db;
                _mapper = mapper;
                _userService = userService;
            }

            public async Task<EditBiographyDto> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
            {
                var dto = await _db.Biography.Include(b => b.Player)
                    .Include(b => b.Posting)
                    .Include(b => b.State).Where(x => x.Id == request.Id).ProjectTo<EditBiographyDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (dto != null && (!_userService.IsAdmin() && dto.Player.AuthString != _userService.GetAuthId()))
                {
                    return null;
                }
                dto ??= new EditBiographyDto();
                dto.Postings = _db.Posting.Select(x => new ListItemDto(x.Name, x.Id.ToString(), x.Default)).ToList();
                dto.Statuses = _db.BiographyStatus.Select(x => new ListItemDto(x.Name, x.Id.ToString(), x.Default)).ToList();
                dto.States = _db.BiographyState.Select(x => new ListItemDto(x.Name, x.Id.ToString(), x.Default)).ToList();
                
                return dto;
            }
        }
    }
}