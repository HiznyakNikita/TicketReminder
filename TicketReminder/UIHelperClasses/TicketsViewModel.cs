using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReminder.UIHelperClasses
{
    public class TicketsViewModel
    {
        public ObservableCollection<Sound> Tickets { get; private set; }

        public TicketsViewModel()
        {
            Tickets = new ObservableCollection<Sound>();
            Tickets.Add(new Sound() { Name = "Kiev - London", Count = 70 });
            Tickets.Add(new Sound() { Name = "Kiev - Berlin", Count = 40 });
            Tickets.Add(new Sound() { Name = "Kiev - New-York", Count = 35 });
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
