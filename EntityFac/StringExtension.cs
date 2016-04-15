
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFac
{
    public static class StringExtension
    {
        public static string StartWithUpper(this string source)
        {
            if (string.IsNullOrEmpty(source)) return source;
            string first = source.Substring(0, 1).ToUpper();
            return first + source.Substring(1);
        }
    }
}
