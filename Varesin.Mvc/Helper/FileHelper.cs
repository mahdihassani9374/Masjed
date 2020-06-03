using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;

namespace Varesin.Mvc.Helper
{
    public static class FileHelper
    {
        public static string GetLength(long length)
        {
            double result = length / 1048576;
            double a = length % 1048576;
            a = a / 1048576;
            return (string.Format("{0:0.00}", (result + a)) + " مگابایت ").ToPersianNumbers();
        }
    }
}
