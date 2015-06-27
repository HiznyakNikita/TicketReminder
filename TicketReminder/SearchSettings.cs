using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public string CarType { get; set; }
        public string PlaceType { get; set; }
        public bool EnableReserve { get; set; }
        public string ReservePriority { get; set; }
        public int CheckPeriod { get; set; }
        public INotifier Notifier { get; set; }
    }
}
