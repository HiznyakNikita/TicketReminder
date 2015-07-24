using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReminder.DataClasses;
using TicketReminder.HelperClasses;

namespace TicketReminder
{
    public class SearchSettings
    {
        private SearchSettings() { }
        private static SearchSettings _instance;

        public static SearchSettings Instance
        {
            get
            {
                if (_instance == null)
                    {
                    _instance = new SearchSettings();
                    return _instance;
                }
                else
                    return _instance;
            }
        }

        public List<CarType> CarTypes { get; set; }
        public List<PlaceType> PlaceTypes { get; set; }
        public bool EnableReserve { get; set; }
        public CarType ReservePriority { get; set; }
        public int CheckPeriod { get; set; }
        public List<INotifier> Notifiers = new List<INotifier>();

        }
}
