using System;
using Microsoft.SPOT;

namespace Agent.TripleFace
{
    public class Time
    {

        public Time()
        {
            CurrentTime = DateTime.Now;

        }

        public DateTime CurrentTime { get; set; }

        public string AMPM
        {
            get
            {
                if (CurrentTime.Hour >= 12) return "PM";
                return "AM";
            }
        }

        public string HourMinute
        {
            get { return Hour + ":" + Minute; }
        }

        public string HourMinuteAMPM
        {
            get { return Hour + ":" + Minute + " " + AMPM; }
        }
        public string Hour24Minute
        {
            get { return Hour24 + ":" + Minute; }
        }

        public string HourMinuteSecond
        {
            get { return Hour + ":" + Minute + ":" + Second; }
        }

        public string Hour24MinuteSecond
        {
            get { return Hour24 + ":" + Minute + ":" + Second; }
        }

        public string Month
        {
            get { return CurrentTime.Month.ToString(); }
        }

        public string Day
        {
            get { return CurrentTime.Day.ToString(); }
        }

        public string Year
        {
            get { return CurrentTime.Year.ToString(); }
        }

        public string MonthName
        {
            get { return System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames[(int) CurrentTime.Month]; }
        }

        public string MonthNameShort
        {
            get { return System.Globalization.DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames[(int) CurrentTime.Month]; }
        }

        public string DayOfWeek
        {
            get { return System.Globalization.DateTimeFormatInfo.CurrentInfo.DayNames[(int) CurrentTime.DayOfWeek]; }
        }

        public string ShortDate
        {
            get { return Year + "/" + Month + "/" + Day; }
        }


        public string Hour
        {
            get
            {
                int hour = CurrentTime.Hour;
                if (hour == 0) hour = 12;
                if (hour > 12) hour = hour - 12;
                var h = hour.ToString();
                return h;
            }
        }

        public string Hour24
        {
            get
            {
                var hour = CurrentTime.Hour.ToString();
                if (hour.Length == 1) hour = "0" + hour;
                return hour;
            }
        }

        public string Minute
        {
            get
            {
                var min = CurrentTime.Minute.ToString();
                if (min.Length == 1) min = "0" + min;
                return min;

            }
        }

        public string Second
        {
            get
            {
                var seconds = CurrentTime.Second.ToString();
                if (seconds.Length == 1) seconds = "0" + seconds;
                return seconds;
            }
        }
    }
}