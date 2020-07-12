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

namespace SPMS.Application.Admin.Query
{
    public class SeoStatusQuery : IRequest<SeoDto>
    {

        public class SeoStatusQueryHandler : IRequestHandler<SeoStatusQuery, SeoDto>
        {
            private readonly ISpmsContext _db;
            private readonly IMapper _mapper;
            private readonly IUserService _userService;
            private readonly IGameService _gameService;


            public SeoStatusQueryHandler(ISpmsContext db, IMapper mapper, IUserService userService, IGameService gameService)
            {
                _db = db;
                _mapper = mapper;
                _userService = userService;
                _gameService = gameService;
            }

            public async Task<SeoDto> Handle(SeoStatusQuery request, CancellationToken cancellationToken)
            {
                var game = await _gameService.GetGameAsync();
               

                var seoDto = new SeoDto();
                seoDto.Author = game.Author;
                seoDto.Description = game.Description;
                seoDto.IsSpiderable = game.IsSpiderable ?? true;
                seoDto.Keywords = game.Keywords;
                seoDto.RobotsText = game.RobotsText;

                return seoDto;
            }
        }
    }
}