using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Strings
{
    public  static class StringConverters
    {
        public static string DateTimeToString(DateTime date) {
            return date.Month + "/" + date.Day + "/" + date.Year;
        }
    }
}
