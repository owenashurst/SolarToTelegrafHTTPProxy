namespace SolarToTelegrafHTTPProxy.Features.Telegraf.Models;

public class TelegrafData
{
    public decimal ACOutputVoltage { get; set; }

    public decimal ACOutputFrequency { get; set; }

    public decimal ACOutputApparentPower { get; set; }

    public decimal ACOutputActivePower { get; set; }

    public decimal BatteryVoltage { get; set; }

    public decimal BatteryChargingCurrent { get; set; }

    public int BatteryCapacity { get; set; }

    public decimal PVInputVoltage { get; set; }

    public decimal PVInputCurrentForBattery { get; set; }

    public decimal BatteryDischargeCurrent { get; set; }

    public decimal ACChargingCurrent { get; set; }

    public decimal ACChargingPower { get; set; }

    public decimal PVTotalChargingPower { get; set; }

    public double? CurrentAgileRate { get; set; } = 0;
}