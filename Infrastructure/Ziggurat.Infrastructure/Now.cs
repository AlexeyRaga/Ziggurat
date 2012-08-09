using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure
{
    public static class Now
    {
        private static Func<DateTime> _dateFunction;

        static Now() { Reset(); }

        public static void Reset()
        {
            _dateFunction = () => DateTime.UtcNow;
        }

        public static void SetUtcTime(DateTime nowDate)
        {
            _dateFunction = () => nowDate;
        }

        public static DateTime UtcTime
        {
            get { return _dateFunction(); }
        }
    }
}
