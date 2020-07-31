using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;

namespace SPMS.Application.Authoring.Command.PublishAllPendingPosts
{
    public class PublishAllPendingPostsCommand : IRequest<bool>
    {

        public class PublishAllPendingPostsCommandHandler : IRequestHandler<PublishAllPendingPostsCommand, bool>
        {
            private readonly ISpmsContext _db;

            public PublishAllPendingPostsCommandHandler(ISpmsContext db)
            {
                _db = db;
            }

            public async Task<bool> Handle(PublishAllPendingPostsCommand request, CancellationToken cancellationToken)
            {
                var now = DateTime.UtcNow;
                var publishedId =
                    (await _db.EpisodeEntryStatus.FirstAsync(x => x.Name == StaticValues.Published,
                        cancellationToken: cancellationToken).ConfigureAwait(true)).Id;

                var pendingId =
                    (await _db.EpisodeEntryStatus.FirstAsync(x => x.Name == StaticValues.Pending,
                        cancellationToken: cancellationToken).ConfigureAwait(true)).Id;

                var entitiesToPublish = await _db.EpisodeEntry.Where(x => x.EpisodeEntryStatusId == pendingId && x.PublishedAt <= now).ToListAsync(cancellationToken).ConfigureAwait(true);

                if (entitiesToPublish == null) return false;

                foreach (var entity in entitiesToPublish)
                {
                    entity.EpisodeEntryStatusId = publishedId;
                }

                
                await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(true);

                

                return true;
            }
        }
    }
}
