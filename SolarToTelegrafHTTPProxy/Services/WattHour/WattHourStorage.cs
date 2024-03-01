using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarToTelegrafHTTPProxy.Services.WattHour
{
    public class WattHourStorage : IWattHourStorage
    {
        // DateTime / Power in Watts
        private IDictionary<DateTime, decimal> _wattHours;

        public WattHourStorage()
        {
            Cleanup();
        }

        public void TrackCurrentPowerForDateTime(decimal power)
        {
            // Cleanup the list if it contains values that aren't from today.
            // Should only run once.
            if (_wattHours.Keys.Any(x => x.Day < DateTime.Now.Day))
            {
                Cleanup();
            }

            _wattHours.Add(DateTime.Now, power);
        }

        public decimal RetrieveCurrentWattHoursForToday()
        {
            decimal totalWattHours = 0;

            KeyValuePair<DateTime, decimal>? previousPair = null;
            foreach (var pair in _wattHours.OrderBy(x => x.Key))
            {
                if (previousPair is not null)
                {
                    double hoursDifference = (pair.Key - previousPair.Value.Key).TotalHours;
                    totalWattHours += previousPair.Value.Value * Convert.ToDecimal(hoursDifference);
                }

                previousPair = pair;
            }

            return Math.Round(totalWattHours, 2);
        }

        private void Cleanup()
        {
            _wattHours = new Dictionary<DateTime, decimal>();
        }
    }
}
