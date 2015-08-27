using System;
using System.Text.RegularExpressions;

namespace AwaitAnything
{
    public class TimeConverter
    {
        public static TimeSpan Convert(string src)
        {
            var pattern = @"\s*(?:(?<num>[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?)\s*(?<type>t|ms|m|s|h|d))\s*";
            var r = new Regex(pattern);
            var m = r.Match(src);
            var num = double.Parse(m.Groups["num"].Value);
            
            switch (m.Groups["type"].Value)
            {
                case "t":
                    return TimeSpan.FromTicks((long)num);
                case "ms":
                    return TimeSpan.FromMilliseconds(num);
                case "s":
                    return TimeSpan.FromSeconds(num);
                case "m":
                    return TimeSpan.FromMinutes(num);
                case "h":
                    return TimeSpan.FromHours(num);
                case "d":
                    return TimeSpan.FromDays(num);
            }

            return TimeSpan.MinValue;
        }
    }
}