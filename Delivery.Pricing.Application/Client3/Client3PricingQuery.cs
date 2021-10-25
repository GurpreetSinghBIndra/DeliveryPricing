using AutoMapper;
using Delivery.Pricing.Common;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Delivery.Pricing.Application.Client3
{
    public class Client3PricingQuery : 
        PricingQuery
    {
        private readonly IMapper mapper;

        public Client3PricingQuery(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public override async Task<HttpContent> ClientContent(IPricingRequest request)
        {
            var clientRequest = mapper.Map<Client3PricingRequest>(request);
            var xmlRequest = string.Empty;

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    (new XmlSerializer(typeof(Client3PricingRequest))).Serialize(writer, clientRequest);
                    xmlRequest = sww.ToString(); // Your XML
                }
            }

            return new StringContent(xmlRequest, Encoding.UTF8, "application/xml");
        }

        public override async Task<decimal> GetClientQuote(string quoteResponse)
        {
            var serializer = new XmlSerializer(typeof(Client3PricingResponse));
            Client3PricingResponse result = null;

            using (TextReader reader = new StringReader(quoteResponse))
            {
                result = (Client3PricingResponse)serializer.Deserialize(reader);
            }

            return result.Quote;
        }
    }
}
