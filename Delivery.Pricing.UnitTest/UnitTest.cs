using Delivery.Pricing.Common;
using Delivery.Pricing.Application;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;
using Delivery.Pricing.Application.Client1;
using Delivery.Pricing.Application.Client2;
using Delivery.Pricing.Application.Client3;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Collections.Generic;
using FluentAssertions.Execution;
using FluentAssertions;

namespace Delivery.Pricing.UnitTest
{
    public class UnitTest :
        IClassFixture<UnitTest>,
        IDisposable
    {
        public Mock<PricingQuery> Client1PricingService { get; private set; }
        public Mock<PricingQuery> Client2PricingService { get; private set; }
        public Mock<PricingQuery> Client3PricingService { get; private set; }
        public IMediator Mediator { get; protected set; }

        protected IServiceCollection _services;

        public UnitTest()
        {
            _services = new ServiceCollection();
            _services.AddApplication(true);
            Client1PricingService = new Mock<PricingQuery>();
            Client2PricingService = new Mock<PricingQuery>();
            Client3PricingService = new Mock<PricingQuery>();

            _services.Remove(new ServiceDescriptor(typeof(PricingQuery), typeof(Client1PricingQuery),
                    ServiceLifetime.Scoped));
            _services.AddScoped(x => Client1PricingService.Object);

            _services.Remove(new ServiceDescriptor(typeof(PricingQuery), typeof(Client2PricingQuery),
                    ServiceLifetime.Scoped));
            _services.AddScoped(x => Client2PricingService.Object);

            _services.Remove(new ServiceDescriptor(typeof(PricingQuery), typeof(Client3PricingQuery),
                    ServiceLifetime.Scoped));
            _services.AddScoped(x => Client3PricingService.Object);

            var provider = _services.BuildServiceProvider();
            Mediator = provider.GetService<IMediator>();
        }

        [Fact]
        public async void All_Workflow_Works_Test()
        {
            this.Client1PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(5);
            this.Client2PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(19);
            this.Client3PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(8);

            var response = await Mediator.Send(new GetPricingQuery
            {
                Destination = new Address { AddressLine = "TEST" },
                Source = new Address { AddressLine = "TEST" },
                Packages = new List<Package> {
                    new Package { Height = 10, Length = 5, Width = 1 }
                }
            });

            using(new AssertionScope())
            {
                response.Should().Be(5);
            }
        }

        [Theory]
        [InlineData("Service1Error")]
        [InlineData("Service2Error")]
        [InlineData("Service3Error")]
        public async void Workflow_Service_Failure_Test(string condition)
        {
            switch (condition)
            {
                case "Service1Error":
                    this.Client1PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(0);
                    this.Client2PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                         .ReturnsAsync(19);
                    this.Client3PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                         .ReturnsAsync(8);
                    break;
                case "Service2Error":
                    this.Client1PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(5);
                    this.Client2PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                         .ReturnsAsync(0);
                    this.Client3PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                         .ReturnsAsync(8);
                    break;
                case "Service3Error":
                    this.Client1PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(5);
                    this.Client2PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                         .ReturnsAsync(19);
                    this.Client3PricingService.Setup(x => x.GetPrice(It.IsAny<CancellationToken>()))
                         .ReturnsAsync(0);
                    break;
            }
            

            var response = await Mediator.Send(new GetPricingQuery
            {
                Destination = new Address { AddressLine = "TEST" },
                Source = new Address { AddressLine = "TEST" },
                Packages = new List<Package> {
                    new Package { Height = 10, Length = 5, Width = 1 }
                }
            });

            using(new AssertionScope())
            {
                switch (condition)
                {
                    case "Service1Error":
                        response.Should().Be(8);
                        break;
                    case "Service2Error":
                        response.Should().Be(5);
                        break;
                    case "Service3Error":
                        response.Should().Be(5);
                        break;
                }
            }
        }

        public void Dispose()
        {
            _services.Clear();
            _services = null;
        }
    }
}
