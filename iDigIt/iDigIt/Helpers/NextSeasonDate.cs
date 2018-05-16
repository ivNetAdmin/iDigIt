using System;
using System.Collections.Generic;
using System.Text;

namespace iDigIt.Helpers
{
    static class NextSeasonDate
    {
        public static DateTimeOffset Date(DateTimeOffset jobDate)
        {
            var currentDay = jobDate.DayOfWeek;
            var futureDate = jobDate.AddYears(1);
            var futureDay = futureDate.DayOfWeek;
            var dateAdjustment = ((int)futureDay - (int)currentDay) * -1;

            return futureDate.AddDays(dateAdjustment);
        }
    }
}
