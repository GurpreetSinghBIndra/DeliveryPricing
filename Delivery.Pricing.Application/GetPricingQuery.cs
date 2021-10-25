using AutoMapper;
using Delivery.Pricing.Common;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.Pricing.Application
{
    public class GetPricingQuery :
        IPricingRequest,
        IRequest<decimal>
    {
        public Address Source { get; set; }
        public Address Destination { get; set; }
        public ICollection<Package> Packages { get; set; }
    }

    public class GetPricingQueryHandler :
        IRequestHandler<GetPricingQuery, decimal>
    {
        private readonly IEnumerable<PricingQuery> clients;

        public GetPricingQueryHandler(IEnumerable<PricingQuery> clients)
        {
            this.clients = clients;
        }

        public async Task<decimal> Handle(GetPricingQuery request, CancellationToken cancellationToken)
        {
            var tasks = new List<Task<decimal>>();
            clients.ToList().ForEach(async cl => {
                cl.Content = await cl.ClientContent(request);
                tasks.Add(cl.GetPrice(cancellationToken));
            });

            var response = await Task.WhenAll(tasks);

            return response.Where(x => x > 0).Min();
        }
    }
}
