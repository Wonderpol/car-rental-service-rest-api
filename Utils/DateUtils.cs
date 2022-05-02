using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalRestApi.Utils
{
    public static class DateUtils
    {
        public static List<DateTimeOffset> GetDatesBetweenTwoDates(DateTimeOffset firstDate, DateTimeOffset secondDate)
        {
            var allDates = new List<DateTimeOffset>();
            for (var date = firstDate;  date <= secondDate; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            return allDates;
        }

        public static DateTimeOffset ConvertTimestampToDateTimeOffset(long timeStamp)
        {
            return DateTimeOffset.FromUnixTimeSeconds(timeStamp).LocalDateTime;
        }

        public static bool CheckIfCanRentVehicleBasedOnTime(long timeStamp1, long timeStamp2, List<DateTimeOffset> alreadyTaken)
        {
            var date1 = ConvertTimestampToDateTimeOffset(timeStamp1);
            var date2 = ConvertTimestampToDateTimeOffset(timeStamp2);

            var allDatesStrings = GetDatesBetweenTwoDates(date1, date2)
                .Select(i => i.Date.ToShortDateString());
            
            var alreadyTakenDatesStrings = alreadyTaken.Select(i => i.Date.ToShortDateString());

            return !allDatesStrings.Any(x => alreadyTakenDatesStrings.Any(y => y == x));
        }
    }
    
}