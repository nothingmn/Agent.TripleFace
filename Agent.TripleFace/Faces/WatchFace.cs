using System;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace Agent.TripleFace.Faces
{
    public class WatchFace : IFace
    {
        private Font font = Resources.GetFont(Resources.FontResources.Digital714Full);
        private Font bigfont = Resources.GetFont(Resources.FontResources.Digital748TimeOnly);

        public void Render(Bitmap screen, bool military, ArrayList forecast)
        {
            DateTime now = DateTime.Now;
            string display = "";
            string hour, minute = now.Minute.ToString();
            if (military)
            {
                hour = now.Hour.ToString();
            }
            else
            {
                int h = now.Hour;
                if (h >= 12) h = h - 12;
                if (h == 0) h = 12;
                hour = h.ToString();
            }
            if (minute.Length == 1) minute = "0" + minute;

            display = hour + ":" + minute;
            screen.DrawLine(Color.White, 2, 0, Program.AgentSize/2, Program.AgentSize, Program.AgentSize/2);

            int left = Program.AgentSize - Program.MeasureString(display, bigfont);
            screen.DrawText(display, bigfont, Color.White, left, (Program.AgentSize/2) +2);
 
            string dow = System.Globalization.DateTimeFormatInfo.CurrentInfo.DayNames[(int) now.DayOfWeek];
            screen.DrawText(dow.ToString(), font, Color.White, 5, 10);

            string date = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames[(int) now.Month];
            date = date + " " + now.Day.ToString();
            screen.DrawText(date, font, Color.White, 5, 30);

        }
    }
}