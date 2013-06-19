using System;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace Agent.TripleFace.Faces
{
    public class WeatherFace : IFace
    {
        private Font font = Resources.GetFont(Resources.FontResources.Digital714Full);
        private Font bigfont = Resources.GetFont(Resources.FontResources.Digital748TimeOnly);
        private Font smallFont = Resources.GetFont(Resources.FontResources.small);

        private int days = 3;
        private int buffer = 7;
        private int top = 10;
        public void Render(Bitmap screen, bool military, ArrayList forecast)
        {
            screen.DrawLine(Color.White, 2, 0, Program.AgentSize / 2, Program.AgentSize, Program.AgentSize / 2);

            DateTime now = DateTime.Now;
            int counter = (int) now.DayOfWeek;
            counter++;
            int left =  buffer;
            Forecast nowForecast = null;
            bool needsDate = false;
            DateTime lastUpdated = DateTime.Now;
            for (int x = 0; x <= days; x++)
            {
                
                string dayName = System.Globalization.DateTimeFormatInfo.CurrentInfo.DayNames[counter];
                if (dayName.Length >= 3) dayName = dayName.Substring(0, 3);
                int width = Program.MeasureString(dayName, font);
                screen.DrawText(dayName, font, Color.White, left, top);
                if (forecast != null && forecast.Count > 0)
                {
                    Forecast current = null;
                    var startDate = now.Date.AddDays(x);
                    foreach (Forecast f in forecast)
                    {
                        if (f.Date.Year == now.Date.Year && f.Date.Month == now.Date.Month && f.Date.Day == now.Date.Day)
                            nowForecast = f;
                        if (startDate.Year == f.Date.Year && startDate.Month == f.Date.Month && startDate.Day == f.Date.Day)
                        {
                            Debug.Print("Found match");
                            current = f;
                            break;
                        }
                    }
                    if (current != null)
                    {
                        needsDate = true;
                        screen.DrawText(current.Current.ToString(), font, Color.White, left+4, top+font.Height + 2);
                        lastUpdated = current.LastUpdated;
                    }
                }
                counter++;
                if (counter > 6) counter = 0;
                left += width + buffer;
            }
            if (nowForecast != null)
            {
                string display = nowForecast.Current.ToString();
                int forecastLeft = Program.AgentSize - Program.MeasureString(display, bigfont);
                screen.DrawText(display, bigfont, Color.White, forecastLeft, (Program.AgentSize / 2) + 2);
                needsDate = true;
                lastUpdated = nowForecast.LastUpdated;
            }
            if (needsDate)
            {
                screen.DrawText(lastUpdated.ToString(), smallFont, Color.White, 3, (Program.AgentSize / 2)-smallFont.Height-1);
                
            }

        }
    }
}
