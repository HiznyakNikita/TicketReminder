using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReminder.DataClasses;

namespace TicketReminder.UIHelperClasses
{
    public class TicketsViewModel
    {
        public IList<Car> Tickets { get; private set; }
        public IList<Train> Trains { get; private set; }

        public TicketsViewModel(IList<Car> tickets, IList<Train> trains)
        {
            Tickets = tickets;
            Trains = trains;
        }

        private object selectedItem = null;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                // selected item has changed
                selectedItem = value;
            }
        }
    }
}
