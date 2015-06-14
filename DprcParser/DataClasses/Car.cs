using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReminder.DataClasses
{
    public class Car
    {
        public int Number { get; set; }
        public CarType Type { get; set; }
        public int HighCoupeCount { get; set; }
        public int LowCoupeCount { get; set; }
        public int HighSideCount { get; set; }
        public int LowSideCount { get; set; }
        public double Price { get; set; }

        public Car(int number, int highCoupeCount, int lowCoupeCount, int highSideCount, int lowSideCount, double price, CarType type)
        {
            Number = number;
            Type = type;
            HighCoupeCount = highCoupeCount;
            LowCoupeCount = lowCoupeCount;
            HighSideCount = highSideCount;
            LowSideCount = lowSideCount;
            Price = price;
        }
    }
}
