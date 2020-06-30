using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.ViewModels.Authoring;
using SPMS.Common;
using SPMS.Domain.Models;

namespace SPMS.Application.Authoring.Command.CreatePost
{
    public class CreatePost : IRequest<int>
    {


        public class CreatePostHandler : IRequestHandler<CreatePost, int>
        {
            private readonly ISpmsContext _db;

            public CreatePostHandler(ISpmsContext db)
            {
                _db = db;
            }

            public async Task<int> Handle(CreatePost request, CancellationToken cancellationToken)
            {
                var activeEpisodeId = await _db.Episode.Include(e => e.Status).CountAsync(e => e.Status.Name == StaticValues.Published || e.Status.Name == StaticValues.Archived, cancellationToken: cancellationToken);



                var entity = new EpisodeEntry();
                var episodeEntryStatus = 
                entity.EpisodeEntryStatusId = (await _db.EpisodeEntryStatus.FirstAsync(
                    x => x.Name == StaticValues.Draft,
                    cancellationToken: cancellationToken)).Id;
                entity.EpisodeEntryTypeId = (await _db.EpisodeEntryType.FirstAsync(x => x.Name == StaticValues.Post,
                    cancellationToken: cancellationToken)).Id;
                entity.EpisodeId = activeEpisodeId;

                await _db.EpisodeEntry.AddAsync(entity, cancellationToken: cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
