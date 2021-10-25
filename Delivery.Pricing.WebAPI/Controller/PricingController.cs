using Delivery.Pricing.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Delivery.Pricing.WebAPI.Controller
{
    [ApiController]
    public class PricingController : 
        ControllerBaseController
    {
        public PricingController(IMediator mediator) : 
            base(mediator)
        { }

        [HttpPost]
        public async Task<IActionResult> GetBestPricing([FromBody] GetPricingQuery query)
        {
            var response = await mediator.Send(query);
            return Ok(response);
        }
    }
}
