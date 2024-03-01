namespace SolarToTelegrafHTTPProxy.Services.WattHour
{
    public interface IWattHourStorage
    {
        /// <summary>
        /// Appends the current power received to the list for calculating WattHours produced today
        /// </summary>
        /// <param name="power">The total power being generated right now</param>
        void TrackCurrentPowerForDateTime(decimal power);

        /// <summary>
        /// Retrieve the calculated WattHours
        /// </summary>
        /// <returns><see cref="decimal"/>The WattHours as a decimal to 2 decimal places</returns>
        decimal RetrieveCurrentWattHoursForToday();
    }
}