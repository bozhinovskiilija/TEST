using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{

    public static class ParseFromString
    {
        public static decimal ParseFloatFromString(this string str)
        {
            return decimal.Parse(string.Join("", str.ToCharArray().Where(char.IsDigit)));
        }

        public static int ParseIntFromString(this string str)
        {
            return Int32.Parse(string.Join("", str.ToCharArray().Where(char.IsDigit)));
        }

        public static double ParseDoubleFromString(this string str)
        {
            return double.Parse(string.Join("", str.ToCharArray().Where(char.IsDigit)));
        }
    }
}
