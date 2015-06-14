using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReminder
{
    public class SearchSettings
    {
        public string CarType { get; set; }
        public string PlaceType { get; set; }
        public bool EnableReserve { get; set; }
        public string ReservePriority { get; set; }
        public int CheckPeriod { get; set; }
    }
}
