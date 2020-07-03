using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Authoring.Command.PublishPost;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;

namespace SPMS.Application.Authoring.Command.UnpublishPost
{

    public class UnpublishPostCommand : IRequest<bool>
    {
        public int Id { get; set; }
        

        public class UnpublishPostHandler : IRequestHandler<UnpublishPostCommand, bool>
        {
            private readonly ISpmsContext _db;

            public UnpublishPostHandler(ISpmsContext db)
            {
                _db = db;
            }

            public async Task<bool> Handle(UnpublishPostCommand request, CancellationToken cancellationToken)
            {
                var entity = await _db.EpisodeEntry.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null) return false;

                entity.EpisodeEntryStatusId =
                    (await _db.EpisodeEntryStatus.FirstAsync(x => x.Name == StaticValues.Draft,
                        cancellationToken: cancellationToken)).Id;
                entity.PublishedAt = null;
                await _db.SaveChangesAsync(cancellationToken);


                return true;
            }
        }
    }
}
