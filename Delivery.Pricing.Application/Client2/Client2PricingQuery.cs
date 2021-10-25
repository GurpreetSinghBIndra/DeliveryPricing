using AutoMapper;
using Delivery.Pricing.Common;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Pricing.Application.Client2
{
    public class Client2PricingQuery : 
        PricingQuery
    {
        private readonly IMapper mapper;

        public Client2PricingQuery(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public override async Task<HttpContent> ClientContent(IPricingRequest request) =>
            new StringContent(JsonConvert.SerializeObject(mapper.Map<Client2PricingRequest>(request)), Encoding.UTF8, "application/json");

        public override async Task<decimal> GetClientQuote(string quoteResponse)
        {
            var res = JsonConvert.DeserializeObject<Client2PricingResponse>(quoteResponse);
            return res.Amount;
        }
    }
}
