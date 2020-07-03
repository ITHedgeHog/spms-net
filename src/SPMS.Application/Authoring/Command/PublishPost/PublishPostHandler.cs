using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;

namespace SPMS.Application.Authoring.Command.PublishPost
{
    public class PublishPostCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public class PublishPostHandler : IRequestHandler<PublishPostCommand, bool>
        {
            private readonly ISpmsContext _db;

            public PublishPostHandler(ISpmsContext db)
            {
                _db = db;
            }

            public async Task<bool> Handle(PublishPostCommand request, CancellationToken cancellationToken)
            {
                var entity = await _db.EpisodeEntry.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null) return false;

                entity.EpisodeEntryStatusId =
                    (await _db.EpisodeEntryStatus.FirstAsync(x => x.Name == StaticValues.Published,
                        cancellationToken: cancellationToken)).Id;
                await _db.SaveChangesAsync(cancellationToken);


                return true;
            }
        }
    }
}