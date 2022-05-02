using System;
using System.Collections.Generic;

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

        public static bool CheckIfCanRentVehicleBasedOnTime(long timeStamp1, long timeStamp2)
        {
            //TODO
            return true;
        }
        
    }
    
}