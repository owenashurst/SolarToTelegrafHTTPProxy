namespace SolarToTelegrafHTTPProxy.Services.Mqtt.Models;

public class SolarInfo
{
    public decimal PVInputVoltage { get; set; }
    
    public decimal PVInputCurrentForBattery { get; set; }
    
    public decimal PVTotalChargingPower { get; set; }

    public decimal BatteryVoltage { get; set; }

    public decimal BatteryChargingCurrent { get; set; }

    public decimal BatteryDischargeCurrent { get; set; }

    public int BatteryCapacity { get; set; }

    public decimal ACOutputVoltage { get; set; }

    public decimal ACOutputFrequency { get; set; }

    public decimal ACOutputPower { get; set; }

    public decimal ACOutputApparentPower { get; set; }
}