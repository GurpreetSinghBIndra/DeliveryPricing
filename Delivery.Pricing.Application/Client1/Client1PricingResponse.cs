using Delivery.Pricing.Common;
using System.Collections.Generic;

namespace Delivery.Pricing.Application.Client1
{
    public class Client1PricingResponse :
        IPricingResponse
    {
        public decimal Total { get; set; }
    }
}
