using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReminder.HelperClasses
{
    public class MessageArgs
    {
        public string From { get; set; }
        public string  To { get; set; }
        public string Date { get; set; }
        public string PlacesCount { get; set; }
        public string TrainNumber { get; set; }
    }
}
