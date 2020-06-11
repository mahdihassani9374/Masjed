using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;
using System.Globalization;

namespace Varesin.Mvc.Extensions
{
    public static class DateTimeExtensionMethods
    {
        public static DateTime? ToDateTime(this string input)
        {
            if (input == null) return null;
            if (input.Length != 10) return null;

            try
            {
                input = input.ToEnglishNumbers();

                int year = Convert.ToInt32(input.Substring(0, 4));
                int month = Convert.ToInt32(input.Substring(5, 2));
                int day = Convert.ToInt32(input.Substring(8, 2));

                PersianCalendar persianCalendar = new PersianCalendar();

                DateTime dt = new DateTime(year, month, day, persianCalendar);

                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
