using System;
using System.Globalization;
using MediatR;

namespace SolarToTelegrafHTTPProxy.Features.Telegraf.Details
{
    public class Query : IRequest<Model>
    {
        public Query(string[] data)
        {
            DataHeader = data.GetValue(0) as string;
            CardId = Convert.ToInt64(data.GetValue(1));
            ProtocolId = Convert.ToInt32(data.GetValue(2));
            Keywords = data.GetValue(3) as string;
            SerialNumber = Convert.ToInt64(data.GetValue(4));
            WorkMode = data.GetValue(5) as string;
            GridVoltage = Convert.ToDecimal(data.GetValue(6));
            GridFrequency = Convert.ToDecimal(data.GetValue(7));
            ACOutputVoltage = Convert.ToDecimal(data.GetValue(8));
            ACOutputFrequency = Convert.ToDecimal(data.GetValue(9));
            ACOutputApparentPower = Convert.ToDecimal(data.GetValue(10));
            ACOutputActivePower = Convert.ToDecimal(data.GetValue(11));
            LoadPercentage = Convert.ToInt32(data.GetValue(12));
            BatteryVoltage = Convert.ToDecimal(data.GetValue(13));
            BatteryChargingCurrent = Convert.ToInt32(data.GetValue(14));
            BatteryCapacity = Convert.ToInt32(data.GetValue(15));
            PVInputVoltage = Convert.ToDecimal(data.GetValue(16));
            TotalChargingCurrent = Convert.ToDecimal(data.GetValue(17));
            TotalACOutputApparentPower = Convert.ToInt32(data.GetValue(18));
            TotalOutputActivePower = Convert.ToInt32(data.GetValue(19));
            TotalACOutputPercentage = Convert.ToInt32(data.GetValue(20));
            InverterStatus = Convert.ToInt32(data.GetValue(21));
            PVInputCurrentForBattery = Convert.ToInt32(data.GetValue(22));
            BatteryDischargeCurrent = Convert.ToInt32(data.GetValue(23));
            DeviceStatus = Convert.ToInt32(data.GetValue(24));
            PVChargingPower = Convert.ToInt32(data.GetValue(25));
            PV2InputVoltage = Convert.ToDecimal(data.GetValue(26));
            PV2InputCurrent = Convert.ToInt32(data.GetValue(27));
            PV2ChargingPower = Convert.ToInt32(data.GetValue(28));
            PV3InputVoltage = Convert.ToDecimal(data.GetValue(29));
            PV3InputCurrent = Convert.ToInt32(data.GetValue(30));
            PV3ChargingPower = Convert.ToInt32(data.GetValue(31));
            LinePowerDirection = Convert.ToInt32(data.GetValue(32));
            DeviceStatus2 = Convert.ToInt32(data.GetValue(33));
            ACChargingCurrent = Convert.ToInt32(data.GetValue(34));
            ACChargingPower = Convert.ToInt32(data.GetValue(35));
            PVTotalChargingPower = Convert.ToInt32(data.GetValue(36));

            CurrentTime = DateTime.ParseExact(data.GetValue(40) as string, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }

        public string DataHeader { get; set; }

        public long CardId { get; set; }

        public int ProtocolId { get; set; }

        public string Keywords { get; set; }

        public long SerialNumber { get; set; }

        public string WorkMode { get; set; }

        public decimal GridVoltage { get; set; }

        public decimal GridFrequency { get; set; }

        public decimal ACOutputVoltage { get; set; }

        public decimal ACOutputFrequency { get; set; }

        public decimal ACOutputApparentPower { get; set; }

        public decimal ACOutputActivePower { get; set; }

        public int LoadPercentage { get; set; }

        public decimal BatteryVoltage { get; set; }

        public decimal BatteryChargingCurrent { get; set; }

        public int BatteryCapacity { get; set; }

        public decimal PVInputVoltage { get; set; }

        public decimal TotalChargingCurrent { get; set; }

        public decimal TotalACOutputApparentPower { get; set; }

        public int TotalOutputActivePower { get; set; }

        public int TotalACOutputPercentage { get; set; }

        public int InverterStatus { get; set; }

        public decimal PVInputCurrentForBattery { get; set; }

        public decimal BatteryDischargeCurrent { get; set; }

        public long DeviceStatus { get; set; }

        public decimal PVChargingPower { get; set; }

        public decimal PV2InputVoltage { get; set; }

        public decimal PV2InputCurrent { get; set; }

        public decimal PV2ChargingPower { get; set; }

        public decimal PV3InputVoltage { get; set; }

        public decimal PV3InputCurrent { get; set; }

        public decimal PV3ChargingPower { get; set; }

        public decimal LinePowerDirection { get; set; }

        public long DeviceStatus2 { get; set; }

        public decimal ACChargingCurrent { get; set; }

        public decimal ACChargingPower { get; set; }

        public decimal PVTotalChargingPower { get; set; }

        public DateTime CurrentTime { get; set; }
    }
}
