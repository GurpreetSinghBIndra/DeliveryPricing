using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Delivery.Pricing.Application.Client1;
using Delivery.Pricing.Application.Client2;
using Delivery.Pricing.Application.Client3;
using Delivery.Pricing.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Delivery.Pricing.Application
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, bool isUnitTest = false)
        {
            services.AddAutoMapper(cfg => cfg.AddCollectionMappers(), Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            if (!isUnitTest)
            {
                services.AddScoped<PricingQuery, Client1PricingQuery>();
                services.AddScoped<PricingQuery, Client2PricingQuery>();
                services.AddScoped<PricingQuery, Client3PricingQuery>();
            }
            return services;
        }
    }
}
