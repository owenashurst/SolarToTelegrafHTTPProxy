using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SolarToTelegrafHTTPProxy.Config;
using SolarToTelegrafHTTPProxy.Features.Telegraf.Models;
using SolarToTelegrafHTTPProxy.Services.Mqtt;
using SolarToTelegrafHTTPProxy.Services.Mqtt.Models;
using SolarToTelegrafHTTPProxy.Services.Octopus;

namespace SolarToTelegrafHTTPProxy.Features.Telegraf.Details
{
    public class QueryHandler : IRequestHandler<Query, Response>
    {
        private readonly GeneralSettings _generalSettings;
        private readonly ITelegrafHttpService _telegrafHttpService;
        private readonly IMqttService _mqttService;
        private readonly IOctopusService _octopusService;
        private readonly IMapper _mapper;

        public QueryHandler(IOctopusService octopusService, ITelegrafHttpService telegrafHttpService,
            IMqttService mqttService, IOptions<GeneralSettings> generalSettings, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(generalSettings);
            _generalSettings = generalSettings.Value;
            
            _telegrafHttpService = telegrafHttpService;
            _mqttService = mqttService;
            _mapper = mapper;
            _octopusService = octopusService;
        }

        public async Task<Response> Handle(Query query, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<TelegrafData>(query);
            
            if (_generalSettings.EnableOctopusAgileRateRetrieval)
            {
                request.CurrentAgileRate = await _octopusService.GetCurrentAgileRateAsync();
            }
            
            var telegrafResult = await _telegrafHttpService.SubmitToTelegraf(request);

            if (!_generalSettings.EnableMqtt)
                return telegrafResult ? new Response { Success = true } : new Response { Success = false };
            
            await GenerateAllMqttSensorConfigurationsAsync();
                
            await _mqttService.PublishMessageToBrokerAsync(new InverterInformation
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
                PVTotalChargingPower = request.PVTotalChargingPower,
                PVTotalEnergyProduction = request.PVTotalChargingPower
            });

            // We only really care whether Telegraf submitted successfully or not.
            return telegrafResult ? new Response { Success = true } : new Response { Success = false };
        }

        private async Task GenerateAllMqttSensorConfigurationsAsync()
        {
            foreach (var type in System.Reflection.Assembly.GetExecutingAssembly().GetTypes())
            {
                if (!typeof(IConfig).IsAssignableFrom(type) || type.IsAbstract || type.IsInterface) continue;
                
                var instance = (IConfig)Activator.CreateInstance(type);

                await _mqttService.PublishConfigMessageToBrokerAsync(instance);
            }
        }
    }
}
