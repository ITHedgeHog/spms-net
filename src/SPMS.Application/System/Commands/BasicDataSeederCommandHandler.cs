using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SPMS.Application.Common.Interfaces;

namespace SPMS.Application.System.Commands
{
    public class BasicDataSeederCommand : IRequest
    {
    }

    public class BasicDataSeederCommandHandler : IRequestHandler<BasicDataSeederCommand>
    {
        private readonly ISpmsContext _context;
        //private readonly IUserManager _userManager;

        public BasicDataSeederCommandHandler(ISpmsContext context )
        {
            _context = context;
        }

        public async Task<Unit> Handle(BasicDataSeederCommand request, CancellationToken cancellationToken)
        {
            var seeder = new BasicDataSeeder(_context);

            await seeder.SeedAllAsync(cancellationToken);

            return Unit.Value;
        }
    }
}