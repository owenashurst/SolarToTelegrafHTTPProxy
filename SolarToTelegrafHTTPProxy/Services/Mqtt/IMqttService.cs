﻿using System.Threading.Tasks;
using SolarToTelegrafHTTPProxy.Services.Mqtt.Models;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt;

public interface IMqttService
{
    Task<bool> PublishMessageToBrokerAsync(InverterInformation inverterInformation);

    Task<bool> PublishConfigMessageToBrokerAsync(IConfig config);
}