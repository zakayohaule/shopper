using System;

namespace ShopperAdmin.Extensions.Helpers
{
    public static class DateExtensions
    {
        public static string FormatDate(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MM-yyyy");
        }

        public static string FormatWithSuffix(this DateTime dateTime)
        {
            string ordinal;

            switch (dateTime.Day)
            {
                case 1:
                case 21:
                case 31:
                    ordinal = "st";
                    break;
                case 2:
                case 22:
                    ordinal = "nd";
                    break;
                case 3:
                case 23:
                    ordinal = "rd";
                    break;
                default:
                    ordinal = "th";
                    break;
            }

            return string.Format("{0:ddd d}{1} {0:MMM yyyy}", dateTime, ordinal);
        }

        public static string FormatDateWithTime(this DateTime dateTime)
        {
            return dateTime.ToString("MM/dd/yyyy, hh:mm tt");
        }

        public static string DateWithSuffix(this DateTime dateTime)
        {
            string ordinal;

            switch (dateTime.Day)
            {
                case 1:
                case 21:
                case 31:
                    ordinal = "st";
                    break;
                case 2:
                case 22:
                    ordinal = "nd";
                    break;
                case 3:
                case 23:
                    ordinal = "rd";
                    break;
                default:
                    ordinal = "th";
                    break;
            }

            return $"{dateTime.Day}{ordinal}";
        }

        public static string FormatWithSuffixAndTime(this DateTime dateTime)
        {
            string ordinal;

            switch (dateTime.Day)
            {
                case 1:
                case 21:
                case 31:
                    ordinal = "st";
                    break;
                case 2:
                case 22:
                    ordinal = "nd";
                    break;
                case 3:
                case 23:
                    ordinal = "rd";
                    break;
                default:
                    ordinal = "th";
                    break;
            }

            return string.Format("{0:ddd dd}{1} {0:MMM yyyy, HH:mm}", dateTime, ordinal);
        }

        public static bool IsToday(this DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today.Date;
        }

        public static DateTime StartOfWeek(this DateTime dateTime)
        {
            return DateTime.Today.AddDays(-1 * (int) (DateTime.Today.DayOfWeek));
        }

        public static bool WithinThisWeek(this DateTime dateTime)
        {
            // var startOfWeek = dateTime.StartOfWeek();
            return dateTime.Date >= DateTime.Now.AddDays(-7).Date && dateTime.Date <= DateTime.Today.Date;
        }

        public static DateTime StartOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static bool WithinThisMonth(this DateTime dateTime)
        {
            return dateTime.Date.Month == DateTime.Now.Month;
        }

        public static bool WithinThisYear(this DateTime dateTime)
        {
            return dateTime.Date.Year == DateTime.Now.Year;
        }
    }
}
