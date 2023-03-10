using System;
using Moq;
using SolarToTelegrafHTTPProxy.Features.Telegraf;
using SolarToTelegrafHTTPProxy.Features.Telegraf.Details;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace SolarToTelegrafHTTPProxy.UnitTests.Features.Telegraf
{
    public class TelegrafTests
    {
        private readonly Mock<ILogger<TelegrafController>> _mockLogger;
        private readonly Mock<ITelegrafHttpService> _mockTelegrafHttpService;
        private readonly IMediator _mediator;
        private readonly TelegrafController _sut;
        
        private const string GoodRequest =
            "WIFI,98001912101639,30,DT,92931903102995,B,000.0,00.00,240.0,50.00,0000,0000,000,24.8,000,074,027.5,000,00000,00000,000,10100110,00,000,00010110,00000,000.0,0000,00000,000.0,0000,00000,00000000,00000000,0000,0000,00014,-,-,-,20230209163548";

        private const string BadRequest =
            "WIFI,98001912101639,30,DT,92931903102995,S,244.2,50.04,000.0,00.00,0000,0000,000,27.0,003,100,000.0,003,00000,00000,000,01000000,00,000,00000101,00000,000.0,0000,00000,000.0,0000,00000,-,00000000,0003,0081,00000,-,-,-,20221222175625";

        public TelegrafTests()
        {
            _mockLogger = new Mock<ILogger<TelegrafController>>();
            _mockTelegrafHttpService = new Mock<ITelegrafHttpService>();

            _mediator = new Mediator(serviceType =>
            {
                if (serviceType == typeof(IRequestHandler<Query, Response>))
                {
                    return new QueryHandler(_mockTelegrafHttpService.Object);
                }

                throw new NotSupportedException($"{serviceType.FullName} is not supported.");
            });
            
            _sut = new TelegrafController(_mediator, _mockLogger.Object);
        }

        [Fact]
        public async Task GIVEN_QueryHandler_WHEN_AGoodRequestIsReceived_THEN_ShouldMapRequestToObjectAndSendToTelegrafService()
        {
            var logger = new Mock<ILogger<TelegrafController>>().Object;
            var controller = new TelegrafController(_mediator, logger);

            var body = "WIFI,98001912101639,30,DT,92931903102995,B,000.0,00.00,240.0,50.00,0000,0000,000,24.8,000,074,027.5,000,00000,00000,000,10100110,00,000,00010110,00000,000.0,0000,00000,000.0,0000,00000,00000000,00000000,0000,0000,00014,-,-,-,20230209163548";

            // Act
            var result = await controller.PostData(body);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}