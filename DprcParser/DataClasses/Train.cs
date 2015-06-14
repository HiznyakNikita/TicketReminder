using DprcParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReminder.DataClasses
{
    public class Train
    {
        public List<Car> Cars = new List<Car>();
        public string Number { get; set; }
        public string Route { get; set; }
        public string Date { get; set; }
        public int PlacesCount { get; set; }

        /// <summary>
        /// Method for getting info about all trains by route from fromPlace to toPlace on date
        /// </summary>
        /// <param name="fromPlace">Place - start point of route</param>
        /// <param name="toPlace">Place - end point of route</param>
        /// <param name="date">Date of route</param>
        /// <returns></returns>
        public static List<Train> GetAllTrainsByRouteInfo(string fromPlace, string toPlace, string date)
        {
            List<Train> trains = new List<Train>();
            string[] trainsNumbers = Parser.GetTrainsNumbers(fromPlace, toPlace, date);
            for (int i = 0; i < trainsNumbers.Length; i++)
            {
                trains.Add(Parser.GetAllTrainInfo(fromPlace, toPlace, date, trainsNumbers[i], i));
            }
            return trains;
        }

        /// <summary>
        /// Method for getting all information about train
        /// </summary>
        /// <param name="fromPlace">Place - start point of route</param>
        /// <param name="toPlace">Place - end point of route</param>
        /// <param name="date">Date of route</param>
        /// <param name="trainNumber">Train number on route</param>
        /// <param name="trainIndexInList">Index of train in list that generates in runtime fro dprc server. Helper field.</param>
        /// <returns></returns>
        public static Train GetAllTrainInfo(string fromPlace, string toPlace, string date, string trainNumber, int trainIndexInList)
        {
            return Parser.GetAllTrainInfo(fromPlace, toPlace, date, trainNumber, trainIndexInList);
        }

        /// <summary>
        /// Get count of places at this train
        /// </summary>
        /// <returns>Places count</returns>
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

        /// <summary>
        /// Get count of places at this train by car type
        /// </summary>
        /// <param name="types">car types(Luxe,Seat etc.)</param>
        /// <returns>Count of places by current car types</returns>
        public int GetCountPlacesByCarType(List<CarType> types)
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

        /// <summary>
        /// Get count of places by place type
        /// </summary>
        /// <param name="types">Place types(normal high, side low, etc.)</param>
        /// <returns>Count of places for current place types</returns>
        public int GetCountPlacesByPlaceType(List<PlaceType> types)
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
