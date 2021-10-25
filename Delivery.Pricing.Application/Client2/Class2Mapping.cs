using AutoMapper;

namespace Delivery.Pricing.Application.Client2
{
    public class Class2Mapping :
        Profile
    {
        public Class2Mapping()
        {
            CreateMap<GetPricingQuery, Client2PricingRequest>()
                .ForMember(d => d.Consignee, o => o.MapFrom(s => s.Destination))
                .ForMember(d => d.Consignor, o => o.MapFrom(s => s.Source))
                .ForMember(d => d.Cartons, o => o.MapFrom(s => s.Packages));
        }
    }
}
