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
using SPMS.Application.Services;

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
            private readonly IGameService _gameService;


            public GetCharacterQueryHandler(ISpmsContext db, IMapper mapper, IUserService userService, IGameService gameService)
            {
                _db = db;
                _mapper = mapper;
                _userService = userService;
                _gameService = gameService;
            }

            public async Task<EditBiographyDto> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
            {
                var gameId = await _gameService.GetGameIdAsync();
                var dto = await _db.Biography.Include(b => b.Player)
                    .Include(b => b.Posting)
                    .Include(b => b.State).Where(x => x.Id == request.Id).ProjectTo<EditBiographyDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (dto != null && (!_userService.IsAdmin() && dto.Player.AuthString != _userService.GetAuthId()))
                {
                    return null;
                }
                dto ??= new EditBiographyDto();
                dto.Postings = await _db.Posting.Where( x => x.GameId == gameId).Select(x => new ListItemDto(x.Name, x.Id.ToString(), x.Default)).ToListAsync(cancellationToken);
                dto.Statuses = await _db.BiographyStatus.Where(x => x.GameId == gameId).Select(x => new ListItemDto(x.Name, x.Id.ToString(), x.Default)).ToListAsync(cancellationToken);
                dto.States = await _db.BiographyState.Where(x => x.GameId == gameId).Select(x => new ListItemDto(x.Name, x.Id.ToString(), x.Default)).ToListAsync(cancellationToken);
                dto.Types = await _db.BiographyTypes.Where(x => x.GameId == gameId).ProjectTo<ListItemDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                return dto;
            }
        }
    }
}