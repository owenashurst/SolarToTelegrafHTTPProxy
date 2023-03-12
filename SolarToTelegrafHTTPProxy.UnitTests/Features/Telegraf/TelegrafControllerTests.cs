using System;
using Moq;
using SolarToTelegrafHTTPProxy.Features.Telegraf;
using SolarToTelegrafHTTPProxy.Features.Telegraf.Details;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace SolarToTelegrafHTTPProxy.UnitTests.Features.Telegraf
{
    public class TelegrafControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<TelegrafController>> _loggerMock;
        private readonly TelegrafController _controller;
        
        private const string MockRequest =
            "WIFI,98001912101639,30,DT,92931903102995,B,000.0,00.00,240.0,50.00,0000,0000,000,24.8,000,074,027.5,000,00000,00000,000,10100110,00,000,00010110,00000,000.0,0000,00000,000.0,0000,00000,00000000,00000000,0000,0000,00014,-,-,-,20230209163548";

        public TelegrafControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<TelegrafController>>();
            _controller = new TelegrafController(_mediatorMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task PostData_ReturnsOk_WhenRequestIsNotDetailedMonitoringInfo()
        {
            var request = "TEST,TEST,TEST,NOTDT";
            var result = await _controller.PostData(request);
            
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PostData_ReturnsOk_WhenQueryIsSuccessful()
        {
            // Arrange
            var response = new Response { Success = true };
            _mediatorMock.Setup(m => m.Send(It.IsAny<Query>(), default)).ReturnsAsync(response);

            // Act
            var result = await _controller.PostData(MockRequest);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PostData_ReturnsStatusCode500_WhenQueryIsNotSuccessful()
        {
            // Arrange
            var response = new Response { Success = false };
            _mediatorMock.Setup(m => m.Send(It.IsAny<Query>(), default)).ReturnsAsync(response);

            // Act
            var result = await _controller.PostData(MockRequest);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = (StatusCodeResult) result;
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task PostData_ReturnsStatusCode500_WhenParsingDataThrowsException()
        {
            // Arrange
            _mediatorMock.Setup(m => m.Send(It.IsAny<Query>(), default)).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.PostData(MockRequest);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
            var statusCodeResult = (StatusCodeResult) result;
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}