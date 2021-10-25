using Delivery.Pricing.Common;
using System.Collections.Generic;

namespace Delivery.Pricing.Application.Client3
{
    public class Client3PricingResponse :
        IPricingResponse
    {
        public decimal Quote { get; set; }
    }
}
