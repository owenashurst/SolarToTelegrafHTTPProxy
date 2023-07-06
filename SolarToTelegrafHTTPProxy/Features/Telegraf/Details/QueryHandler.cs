using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using SolarToTelegrafHTTPProxy.Config;
using SolarToTelegrafHTTPProxy.Services.Mqtt;

namespace SolarToTelegrafHTTPProxy.Features.Telegraf.Details
{
    public class QueryHandler : IRequestHandler<Query, Response>
    {
        private readonly ITelegrafHttpService _telegrafHttpService;
        private readonly IMqttService _mqttService;
        private readonly GeneralSettings _generalSettings;

        public QueryHandler(ITelegrafHttpService telegrafHttpService, IMqttService mqttService, IOptions<GeneralSettings> generalSettings)
        {
            _telegrafHttpService = telegrafHttpService;
            _mqttService = mqttService;
            
            ArgumentNullException.ThrowIfNull(generalSettings);
            _generalSettings = generalSettings.Value;
        }

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            var telegrafResult = await _telegrafHttpService.SubmitToTelegraf(request);

            if (_generalSettings.EnableMqtt)
            {
                await _mqttService.PublishMessageToBrokerAsync(new Services.Mqtt.Models.SolarInfo
                {
                    ACOutputApparentPower = request.ACOutputApparentPower,
                    ACOutputFrequency = request.ACOutputFrequency,
                    ACOutputPower = request.ACOutputActivePower,
                    ACOutputVoltage = request.ACOutputVoltage,
                    BatteryCapacity = request.BatteryCapacity,
                    BatteryChargingCurrent = request.BatteryChargingCurrent,
                    BatteryDischargeCurrent = request.BatteryDischargeCurrent,
                    BatteryVoltage = request.BatteryVoltage,
                    PVInputCurrentForBattery = request.PVInputCurrentForBattery,
                    PVInputVoltage = request.PVInputVoltage,
                    PVTotalChargingPower = request.PVTotalChargingPower
                });
            }

            // We only really care whether Telegraf submitted successfully or not.
            return telegrafResult ? new Response { Success = true } : new Response { Success = false };
        }
    }
}
