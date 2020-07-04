using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.Services;

namespace SPMS.Application.Character.Command
{
    public class UpdateCharacterCommand : IRequest<UpdateCharacterResponse>
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
        public string Species { get; set; }
        public string Homeworld { get; set; }
        public string Gender { get; set; }
        public string Born { get; set; }
        public string Eyes { get; set; }
        public string Hair { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Affiliation { get; set; }
        public string Assignment { get; set; }
        public string Rank { get; set; }
        public string RankImage { get; set; }

        public int StatusId { get; set; }
        public string Status { get; set; }
        public int StateId { get; set; }
        public string State { get; set; }
        public int PostingId { get; set; }
        public string Posting { get; set; }

        public int PlayerId { get; set; }
        public string Player { get; set; }
        public string History { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }

        public class UpdateCharacterHandler : IRequestHandler<UpdateCharacterCommand, UpdateCharacterResponse>
        {
            private readonly ISpmsContext _context;
            private readonly IMapper _mapper;
            private readonly IUserService _userService;
            private readonly IGameService _gameService;

            public UpdateCharacterHandler(ISpmsContext context, IMapper mapper, IUserService userService, IGameService gameService)
            {
                _context = context;
                _mapper = mapper;
                _userService = userService;
                _gameService = gameService;
            }

            public async Task<UpdateCharacterResponse> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
            {
                var gameId = await _gameService.GetGameIdAsync();
                var entity = await _context.Biography.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null)
                {
                    entity = _mapper.Map<Domain.Models.Biography>(request);
                    entity.PostingId = (await _context.Posting.FirstOrDefaultAsync(x => x.Default && x.GameId == gameId, cancellationToken: cancellationToken)).Id;
                    entity.StatusId = (await _context.BiographyStatus.FirstOrDefaultAsync(x => x.Default && x.GameId == gameId, cancellationToken: cancellationToken)).Id;
                    entity.StateId = (await _context.BiographyState.FirstAsync(x => x.Default && x.GameId == gameId, cancellationToken: cancellationToken)).Id;
                    entity.TypeId = (await _context.BiographyTypes.FirstAsync(x => x.Default && x.GameId == gameId, cancellationToken: cancellationToken)).Id;
                    entity.PlayerId = _userService.GetId();

                    await _context.Biography.AddAsync(entity, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                    return UpdateCharacterResponse.Created;
                }

                _mapper.Map(request, entity);

                await _context.SaveChangesAsync(cancellationToken);



                return UpdateCharacterResponse.Updated;
            }
        }



    }

    public enum UpdateCharacterResponse
    {
        Updated,
        Created
    }
}
