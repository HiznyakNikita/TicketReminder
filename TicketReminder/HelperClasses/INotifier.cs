using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketReminder.HelperClasses
{
    public interface INotifier
    {
        void Notify(List<MessageArgs> args);
    }
}
