using System;
using System.Collections;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation.Media;


namespace Agent.TripleFace.Faces
{
    public class AnalogFace : IFace
    {
        private Font font = Resources.GetFont(Resources.FontResources.small);
        
        private Bitmap img = new Bitmap(Resources.GetBytes(Resources.BinaryResources.WatchFaceFromScratch),
                                Bitmap.BitmapImageType.Gif);
        public void Render(Bitmap screen, bool military, ArrayList forecast)
        {
            
            var time = new Time();
            time.CurrentTime = DateTime.Now;
            screen.DrawImage(0, 0, img, 0, 0, img.Width, img.Height);


            var text = "agent";
            Point textLocation = new Point(
                Program.Center.X - (Program.MeasureString(text, font) / 2), Program.Center.Y - 25);
            screen.DrawText(text, font, Color.White, textLocation.X, textLocation.Y);

            var date = time.MonthNameShort + " " + time.Day;
            Point dateLocation = new Point(
                Program.Center.X - (Program.MeasureString(date, font) / 2), Program.Center.Y + 20);
            screen.DrawText(date, font, Color.White, dateLocation.X, dateLocation.Y);

            PaintSkinnyHands(screen, time);
        }

        public void PaintSkinnyHands(Bitmap screen, Time time)
        {
            PaintHourHand(screen, Color.White, 3, time.CurrentTime.Hour, time.CurrentTime.Minute);
            PaintMinuteHand(screen, Color.White, 2, time.CurrentTime.Minute,
                                           time.CurrentTime.Second);
            //PaintSecondHand(screen,Color.White, 1, time.CurrentTime.Second);

            screen.DrawEllipse(Color.White, 1, Program.Center.X, Program.Center.Y, 3, 3, Color.White, 0, 0,
                                  Color.White, 0, 0, 255);
            screen.DrawEllipse(Color.White, 1, Program.Center.X, Program.Center.Y, 2, 2, Color.Black, 0, 0,
                                              Color.White, 0, 0, 255);

        }
        public void PaintMinuteHand(Bitmap screen, Color color, int thickness, int minute, int second)
        {
            PaintLine(screen, color, thickness, Program.Center, MinuteHandLocation(minute, second));
        }

        public void PaintSecondHand(Bitmap screen, Color color, int thickness, int second)
        {

            PaintLine(screen, color, thickness, Program.Center, SecondHandLocation(second));
        }

        public void PaintHourHand(Bitmap screen, Color color, int thickness, int hour, int minute)
        {
            PaintLine(screen, color, thickness, Program.Center, HourHandLocation(hour, minute));

        }

        public void PaintLine(Bitmap screen, Color color, int thickness, Point start, Point end)
        {
            screen.DrawLine(color, thickness, start.X, start.Y, end.X, end.Y);
        }
        public Point MinuteHandLocation(int minute, int second)
        {
            int min = (int)((6 * minute) + (0.1 * second)); // Jump to Minute and add offset for 6 degrees over 60 seconds'
            return PointOnCircle(TRANSLATE_RADIUS_MINUTES, min + (-90), Program.Center);
        }

        public Point HourHandLocation(int hour, int minute)
        {
            int hr = (int)((30 * (hour % 12)) + (0.5 * minute));
            // Jump to Hour and add offset for 30 degrees over 60 minutes
            return PointOnCircle(TRANSLATE_RADIUS_HOURS, hr + (-90), Program.Center);
        }

        public Point SecondHandLocation(int second)
        {
            int sec = 6 * second;
            return PointOnCircle(TRANSLATE_RADIUS_SECONDS, sec + (-90), Program.Center);
        }

        private Point PointOnCircle(float radius, float angleInDegrees, Point origin)
        {
            return new Point(
                (int)(radius * System.Math.Cos(angleInDegrees * System.Math.PI / 180F)) + origin.X,
                (int)(radius * System.Math.Sin(angleInDegrees * System.Math.PI / 180F)) + origin.Y
                );
        }

        private const int TRANSLATE_RADIUS_SECONDS = 47;
        private const int TRANSLATE_RADIUS_MINUTES = 40;
        private const int TRANSLATE_RADIUS_HOURS = 30;

    }
}