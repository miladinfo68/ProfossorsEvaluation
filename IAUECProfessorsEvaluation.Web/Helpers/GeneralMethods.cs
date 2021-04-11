using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace IAUECProfessorsEvaluation.Web.Helpers
{
    public static class GeneralMethods
    {
        static PersianCalendar pc = new PersianCalendar();
        public static string ConvertToJalaliDateTime(DateTime date)
        {
            return pc.GetYear(date).ToString() + '/' + pc.GetMonth(date).ToString() + '/' + pc.GetDayOfMonth(date).ToString() + ' ' + date.ToLongTimeString().Replace("AM", "ق.ظ").Replace("PM", "ب.ظ");
        }

        public static DateTime ConvertToGregorian(string persianDate)
        {
            try
            {
                var parts = persianDate.Split('/');
                var fa = CultureInfo.GetCultureInfoByIetfLanguageTag("fa");
                var en = CultureInfo.GetCultureInfoByIetfLanguageTag("en");

                return pc.ToDateTime(Convert.ToInt32(parts[0].ConvertDigitChar(fa, en)), Convert.ToInt32(parts[1].ConvertDigitChar(fa, en)), Convert.ToInt32(parts[2].ConvertDigitChar(fa, en)), 0, 0, 0, 0);
            }
            catch(Exception ex)
            {
                return DateTime.MinValue;
            }
        }

        public static string ConvertDigitChar(this string str, CultureInfo source, CultureInfo destination)
        {
            for (int i = 0; i <= 9; i++)
            {
                str = str.Replace(source.NumberFormat.NativeDigits[i], destination.NumberFormat.NativeDigits[i]);
            }
            return str;
        }
    }
}