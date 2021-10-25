using Delivery.Pricing.Common;
using System.Collections.Generic;

namespace Delivery.Pricing.Application.Client2
{
    public class Client2PricingRequest :
        IPricingRequest
    {
        public Address Consignee { get; set; }
        public Address Consignor { get; set; }
        public ICollection<Package> Cartons { get; set; }
    }
}
