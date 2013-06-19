using System;
using Microsoft.SPOT;

namespace Agent.TripleFace
{
    public class Forecast
    {
        public int Current { get; set; }
        public int Low { get; set; }
        public int ChanceOfShower { get; set; }
        public string Location { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}