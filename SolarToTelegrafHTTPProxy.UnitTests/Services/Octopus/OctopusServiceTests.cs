using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using SolarToTelegrafHTTPProxy.Config;
using SolarToTelegrafHTTPProxy.Services.Octopus;
using SolarToTelegrafHTTPProxy.Services.Octopus.Models;
using Xunit;

namespace SolarToTelegrafHTTPProxy.UnitTests.Services.Octopus;

public class OctopusServiceTests
{
    private readonly OctopusService _sut;
    private readonly Mock<ILogger<OctopusService>> _octopusServiceLoggerMock;
    private readonly Mock<IOctopusClient> _octopusClientMock;

    public OctopusServiceTests()
    {
        _octopusClientMock = new Mock<IOctopusClient>();
        _octopusServiceLoggerMock = new Mock<ILogger<OctopusService>>();
        
        var mockOptions = new Mock<IOptions<OctopusSettings>>();
        mockOptions.Setup(x => x.Value).Returns(new OctopusSettings
        {
            TariffCode = "ABC",
            RegionalCode = "DEF"
        });
        
        _sut = new OctopusService(_octopusServiceLoggerMock.Object, _octopusClientMock.Object, mockOptions.Object);
    }

    [Fact]
    public async Task GIVEN_OctopusService_WHEN_ACallIsMadeToGetTheAgileRates_THEN_ShouldGetTheCurrentAgileRate()
    {
        var mockOctopusAgileResponse = new TariffResponse
        {
            Count = 0,
            Next = string.Empty,
            Previous = null,
            Results = new []
            {
                new Tariff
                {
                    ValueExcVat = 17.85,
                    ValueIncVat = 18.7425,
                    ValidFrom = DateTime.Parse("2023-10-22T21:30:00Z"),
                    ValidTo = DateTime.Parse("2023-10-22T22:00:00Z")
                },
                new Tariff
                {
                    ValueExcVat = 21.76,
                    ValueIncVat = 22.848,
                    ValidFrom = DateTime.Parse("2023-10-22T21:00:00Z"),
                    ValidTo = DateTime.Parse("2023-10-22T21:30:00Z")
                },
                new Tariff
                {
                    ValueExcVat = 17.85,
                    ValueIncVat = 18.7425,
                    ValidFrom = DateTime.Parse("2023-10-22T20:30:00Z"),
                    ValidTo = DateTime.Parse("2023-10-22T21:00:00Z")
                },
                new Tariff
                {
                    ValueExcVat = 23.42,
                    ValueIncVat = 24.591,
                    ValidFrom = DateTime.Parse("2023-10-22T20:00:00Z"),
                    ValidTo = DateTime.Parse("2023-10-22T20:30:00Z")
                },
                new Tariff
                {
                    ValueExcVat = 22.91,
                    ValueIncVat = 24.0555,
                    ValidFrom = DateTime.Parse("2023-10-22T19:30:00Z"),
                    ValidTo = DateTime.Parse("2023-10-22T20:00:00Z")
                },
            }
        };

        _octopusClientMock.Setup(x => x.GetCurrentAgileRatesAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(mockOctopusAgileResponse);
        
        var result = await _sut.GetCurrentAgileRateAsync();
        
        result.ShouldBeEquivalentTo(24.0555);
    }
}