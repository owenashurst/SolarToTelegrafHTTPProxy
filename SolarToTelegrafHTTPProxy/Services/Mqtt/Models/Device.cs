﻿using System.Text.Json.Serialization;

namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models;

public class Device
{

    [JsonPropertyName("identifiers")] 
    public string[] Identifiers { get; set; } = { "IconicaSolar" };

    [JsonPropertyName("name")] 
    public string Name { get; set; } = "Iconica Solar";
}