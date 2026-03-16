using System;
using System.Globalization;

namespace ArthurKnight.Core
{
    public static class DateExtension
    {
        public static DateTime ParseDate(this string iso)
        {
            if (DateTime.TryParseExact(
                    iso,
                    "O", // Round-trip ISO 8601 format
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind,
                    out var time))
            {
                return time;
            }

            return DateTime.MinValue;
        }

        public static string ToISO(this DateTime date)
        {
            return date.ToString("O");
        }
    }
}