using Delivery.Pricing.Common;
using System.Collections.Generic;

namespace Delivery.Pricing.Application.Client3
{
    public class Client3PricingRequest :
        IPricingRequest
    {
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public List<Package> Packages { get; set; }
    }
}
