using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Pricing.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerBaseController : 
        ControllerBase
    {
        protected readonly IMediator mediator;

        public ControllerBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}
