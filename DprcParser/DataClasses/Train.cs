using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReminder.DataClasses
{
    public class Train
    {
        public static List<Car> Cars { get; set; }
        public string Number { get; set; }
        public string Route { get; set; }
        public string Date { get; set; }
        public int PlacesCount { get; set; }

        public static List<Train> GetAllTrainsByRouteInfo(string fromPlace, string toPlace, string date)
        {
            List<Train> trains = new List<Train>();
            string[] trainsNumbers = DprcGovUaParser.GetTrainsNumbers(fromPlace, toPlace, date);
            for (int i = 0; i < trainsNumbers.Length; i++)
            {
                trains.Add(DprcGovUaParser.GetAllTrainInfo(fromPlace, toPlace, date, trainsNumbers[i], i));
            }
            return trains;
        }

        public static Train GetAllTrainInfo(string fromPlace, string toPlace, string date, string trainNumber, int trainIndexInList)
        {
            return DprcGovUaParser.GetAllTrainInfo(fromPlace, toPlace, date, trainNumber, trainIndexInList);
        }

        public int GetPlacesCount()
        {
            int count = 0;
            foreach(var car in Cars)
            {
                count += car.HighCoupeCount;
                count += car.HighSideCount;
                count += car.LowCoupeCount;
                count += car.LowSideCount;
            }
            return count;
        }

        public static int GetCountPlacesByCarType(List<CarType> types)
        {
           int count = 0;
           foreach(var car in Cars)
               if(types.Contains(car.Type))
               {
                   count += car.HighCoupeCount;
                   count += car.HighSideCount;
                   count += car.LowCoupeCount;
                   count += car.LowSideCount;
               }
           return count;
        }

        public static int GetCountPlacesByPlaceType(List<PlaceType> types)
        {
            int count = 0;
            foreach(var car in Cars)
            {
                if (types.Contains(PlaceType.HighCoupe))
                    count += car.HighCoupeCount;
                if (types.Contains(PlaceType.LowCoupe))
                    count += car.LowCoupeCount;
                if (types.Contains(PlaceType.HighSide))
                    count += car.HighSideCount;
                if (types.Contains(PlaceType.LowSide))
                    count += car.LowSideCount;
            }
            return count;
        }
    }
}
