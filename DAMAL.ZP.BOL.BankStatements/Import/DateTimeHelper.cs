using System;
using System.Text.RegularExpressions;

namespace DAMAL.ZP.BOL.BankStatements.Import
{
    public class DateTimeHelper
    {
        public static DateTime ToDateTimeLong(string stringDate)
        {
            Regex r = new Regex(@"^(?<year>\d\d\d\d)(?<month>\d\d)(?<day>\d\d)$");
            return ToDateTime(stringDate, r);
        }

        private static bool StringDateIsShort(string stringDate)
        {
            if(stringDate.Length ==  6)
                return true;
            return false;
        }

        public static DateTime ToDateTimeShort(string stringDate)
        {
            if (StringDateIsShort(stringDate))
                stringDate = "20" + stringDate;
            return ToDateTimeLong(stringDate);
            //Regex r = new Regex(@"^(?<year>\d\d)(?<month>\d\d)(?<day>\d\d)$");
            //return ToDateTime(stringDate, r);
        }

        private static DateTime ToDateTime(string stringDate, Regex regex)
        {
            if (!regex.IsMatch(stringDate))
                throw new Exception("Zły format daty");

            int year = Convert.ToInt32(regex.Match(stringDate).Result("${year}"));
            int month = Convert.ToInt32(regex.Match(stringDate).Result("${month}"));
            int day = Convert.ToInt32(regex.Match(stringDate).Result("${day}"));
            return new DateTime(year, month, day);
        }

        public static DateTime ToDateTime(string s, string separator)
        {
            if (separator != string.Empty)
            {
                string[] date = s.Split(separator.ToCharArray());
                int year = Convert.ToInt32(date[0]);
                int month = Convert.ToInt32(date[1]);
                int day = Convert.ToInt32(date[2]);
                return new DateTime(year, month, day);
            }

            if (s.Length != 8)
                throw new Exception("Zły format daty");

            int year1 = Convert.ToInt32(s.Substring(0, 4));
            int month1 = Convert.ToInt32(s.Substring(4, 2));
            int day1 = Convert.ToInt32(s.Substring(6, 2));
            return new DateTime(year1, month1, day1);
        }

        public static string ReplaceInStr(string line, char c1, char c2)
        {
            string s = "";
            bool isInside = false;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                    isInside = !isInside;
                if (isInside && line[i] == c1)
                    s += c2;
                else
                    s += line[i];
            }
            return s;
        }
    }
}
