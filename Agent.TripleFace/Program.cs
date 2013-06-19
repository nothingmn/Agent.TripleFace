using System;
using System.Collections;
using System.Threading;
using Agent.TripleFace.Faces;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;

namespace Agent.TripleFace
{
    public class Program
    {
        public static int AgentSize = 128;
        public static Point Center = new Point(64,64);
        private static Bitmap screen;
        private static ArrayList faces = new ArrayList();
        private static int index = 1;
        private static bool UseMilitaryTime = false;
        private static ArrayList Forecast = new ArrayList();

        public static void Main()
        {
            screen = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);
            
            #region fake forecast
            Forecast.Add(new Forecast()
            {
                Low = 18,
                ChanceOfShower = 30,
                Current = 23,
                Date = DateTime.Now,
                LastUpdated = DateTime.Now,
                Location = "Vancouver, BC",
                Message = "A chance of showers, mainly before 2am."
            });
            Forecast.Add(new Forecast()
            {
                Low = 10,
                ChanceOfShower = 31,
                Current = 12,
                Date = DateTime.Now.AddDays(1),
                LastUpdated = DateTime.Now,
                Location = "Vancouver, BC",
                Message = "A chance of showers, mainly before 2am."
            });
            Forecast.Add(new Forecast()
            {
                Low = 20,
                ChanceOfShower = 32,
                Current = 28,
                Date = DateTime.Now.AddDays(2),
                LastUpdated = DateTime.Now,
                Location = "Vancouver, BC",
                Message = "A chance of showers, mainly before 2am."
            });
            Forecast.Add(new Forecast()
            {
                Low = 15,
                ChanceOfShower = 33,
                Current = 21,
                Date = DateTime.Now.AddDays(3),
                LastUpdated = DateTime.Now,
                Location = "Vancouver, BC",
                Message = "A chance of showers, mainly before 2am."
            });
            Forecast.Add(new Forecast()
            {
                Low = 13,
                ChanceOfShower = 34,
                Current = 22,
                Date = DateTime.Now.AddDays(4),
                LastUpdated = DateTime.Now,
                Location = "Vancouver, BC",
                Message = "A chance of showers, mainly before 2am."
            });
            #endregion

            faces.Add(typeof (Calendar));
            faces.Add(typeof (WatchFace));
            faces.Add(typeof (WeatherFace));

            Render();

            ButtonHelper.Current.OnButtonPress += Current_OnButtonPress;
            Thread.Sleep(Timeout.Infinite);
        }

        private static void Render()
        {
            Type t = faces[index] as Type;
            if (t != null)
            {
                try
                {
                    var ctor = t.GetConstructor(new Type[0]);
                    if (ctor != null)
                    {
                        IFace instance = (ctor.Invoke(new object[0]) as IFace);
                        if (instance != null)
                        {                            
                            screen.Clear();

                            screen.DrawRectangle(Color.White, 1, 1, 1, AgentSize, AgentSize, 0, 0,
                                          Color.Black, 0, 0, Color.Black, 0, 0, 0);

                            
                            instance.Render(screen, UseMilitaryTime, Program.Forecast);
                            screen.Flush();
                        }
                    }
                }
                catch (Exception)
                {
                }

            }

        }

        private static void Current_OnButtonPress(Buttons button, Microsoft.SPOT.Hardware.InterruptPort port,
                                                  ButtonDirection direction, DateTime time)
        {
            if (direction == ButtonDirection.Up)
            {
                if (button == Buttons.Top)
                {
                    if (index < faces.Count-1)
                    {
                        index++;
                        Render();
                    }
                }
                else if(button == Buttons.Bottom)
                {
                    if (index > 0)
                    {
                        index--;
                        Render();
                    }
                }

            }
        }

        public static int MeasureString(string text, Font font)
        {
            if (text == null || text.Trim() == "") return 0;
            int size = 0;
            for (int i = 0; i < text.Length; i++)
            {
                size += font.CharWidth(text[i]);
            }
            return size;
        }

    }
}