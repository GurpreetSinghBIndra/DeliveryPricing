using AutoMapper;

namespace Delivery.Pricing.Application.Client3
{
    public class Client3Mapping :
        Profile
    {
        public Client3Mapping()
        {
            CreateMap<GetPricingQuery, Client3PricingRequest>();
        }
    }
}
