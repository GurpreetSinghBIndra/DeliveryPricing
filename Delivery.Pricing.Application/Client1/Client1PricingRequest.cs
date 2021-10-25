using Delivery.Pricing.Common;
using System.Collections.Generic;

namespace Delivery.Pricing.Application.Client1
{
    public class Client1PricingRequest :
        IPricingRequest
    {
        public Address Contact { get; set; }
        public Address Warehouse { get; set; }
        public ICollection<Package> Package { get; set; }
    }
}
