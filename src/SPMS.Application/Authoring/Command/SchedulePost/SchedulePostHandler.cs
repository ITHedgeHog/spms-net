using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SPMS.Application.Common.Interfaces;
using SPMS.Common;

namespace SPMS.Application.Authoring.Command.SchedulePost
{
    public class SchedulePostCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DateTime PublishAt { get; set; }

        public class SchedulePostHandler : IRequestHandler<SchedulePostCommand, bool>
        {
            private readonly ISpmsContext _db;

            public SchedulePostHandler(ISpmsContext db)
            {
                _db = db;
            }

            public async Task<bool> Handle(SchedulePostCommand request, CancellationToken cancellationToken)
            {
                var entity = await _db.EpisodeEntry.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (entity == null) return false;

                entity.EpisodeEntryStatusId =
                    (await _db.EpisodeEntryStatus.FirstAsync(x => x.Name == StaticValues.Pending,
                        cancellationToken: cancellationToken)).Id;
                entity.PublishedAt = request.PublishAt;
                await _db.SaveChangesAsync(cancellationToken);


                return true;
            }
        }
    }
}
