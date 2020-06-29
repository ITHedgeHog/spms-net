using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
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
            try
            {
                var seeder = new BasicDataSeeder(_context);

                await seeder.SeedAllAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                var i = 0;
            }

            return Unit.Value;
        }
    }

    public class BasicDataSeederExceptionHandler : IRequestExceptionAction<BasicDataSeederCommand>
    {
        private readonly ILogger<BasicDataSeederExceptionHandler> _logger;

        public BasicDataSeederExceptionHandler(ILogger<BasicDataSeederExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task Execute(BasicDataSeederCommand request, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An Exception Occurred");
        }
    }
}