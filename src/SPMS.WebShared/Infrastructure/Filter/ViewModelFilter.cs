using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SPMS.Application.Common.Interfaces;
using SPMS.Application.System.Query;

namespace SPMS.WebShared.Infrastructure.Filter
{
    public class ViewModelFilter : ActionFilterAttribute
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public ViewModelFilter(IMediator mediator, IMapper mapper, IUserService userService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userService = userService;
        }

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // Do something before the action executes.
            await _userService.CreateNewPlayer(CancellationToken.None);

            // next() calls the action method.
            var resultContext = await next();
            // resultContext.Result is set.
            // Do something after the action executes.
            //await _userService.CreateNewPlayer(CancellationToken.None);
            if (resultContext.Controller is Controller controller)
            {
                if (controller.ViewData.Model is Common.ViewModels.BaseViewModel model)
                {
                    var dto = await _mediator.Send(new TenantQuery() { Url = context.HttpContext.Request.Host.Host }, CancellationToken.None);

                    _mapper.Map(dto, model);
                }
            }
        }
    }
}
