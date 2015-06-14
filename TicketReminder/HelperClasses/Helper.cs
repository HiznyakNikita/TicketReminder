using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReminder
{
    public class Helper
    {
        public static string ConvertDate(string dateToConvert)
        {
            string year = dateToConvert.Substring(6, 4);
            string month = dateToConvert.Substring(3, 2);
            string day = dateToConvert.Substring(0, 2);
            return year + "-" + month + "-" + day;
        }

        public static CarType ConvertToCarType(string type)
        {
            switch(type)
            {
                case "Люкс":
                    return CarType.Luxe;
                case "Купэ фирменное":
                    return CarType.CoupeFirm;
                case "Купэ":
                    return CarType.Coupe;
                case "Плацкарт фирменный":
                    return CarType.ReservedSeatFirm;
                case "Плацкарт":
                    return CarType.ReservedSeat;
                case "Сидя":
                    return CarType.Seat;
                default:
                    return CarType.Seat;
            }
        }
    }
}
