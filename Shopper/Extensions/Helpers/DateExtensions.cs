using System;

namespace Shopper.Extensions.Helpers
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

            return string.Format("{0:ddd dd}{1} {0:MMM yyyy}", dateTime, ordinal);
        }
    }
}
