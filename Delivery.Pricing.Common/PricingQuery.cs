using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Pricing.Common
{
    public abstract class PricingQuery
    {
        public HttpClient Client { get; }
        public string URL { get; set; }
        public HttpContent Content { get; set; }

        public PricingQuery()
        {
            Client = new HttpClient();
        }

        ~PricingQuery()
        {
            Client.Dispose();
        }

        public virtual async Task<decimal> GetPrice(CancellationToken cancellationToken)
        {
            var retVal = 0M;
            using (var response = await Client.PostAsync(URL, Content, cancellationToken))
            {
                if (response.IsSuccessStatusCode)
                {
                    retVal = await GetClientQuote(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    retVal = 0;
                }
            }

            return retVal;
        }

        public abstract Task<HttpContent> ClientContent(IPricingRequest request);
        public abstract Task<decimal> GetClientQuote(string quoteResponse);
    }
}
