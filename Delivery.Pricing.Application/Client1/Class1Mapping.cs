using AutoMapper;

namespace Delivery.Pricing.Application.Client1
{
    public class Class1Mapping :
        Profile
    {
        public Class1Mapping()
        {
            CreateMap<GetPricingQuery, Client1PricingRequest>()
                .ForMember(d => d.Contact, o => o.MapFrom(s => s.Destination))
                .ForMember(d => d.Warehouse, o => o.MapFrom(s => s.Source))
                .ForMember(d => d.Package, o => o.MapFrom(s => s.Packages));
        }
    }
}
