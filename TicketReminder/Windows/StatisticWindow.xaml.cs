using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TicketReminderLibrary;

namespace TicketReminder.Windows
{
    /// <summary>
    /// Логика взаимодействия для StatisticWindow.xaml
    /// </summary>
    public partial class StatisticWindow : MetroWindow
    {
        public StatisticWindow()
        {
            InitializeComponent();
            this.DataContext = new TicketsViewModel();
        }
    }

    public class TicketsViewModel
    {
        public ObservableCollection<Sound> Tickets { get; private set; }

        public TicketsViewModel()
        {
            Tickets = new ObservableCollection<Sound>();
            Tickets.Add(new Sound() { Name = "Kiev - London", Count = 100 });
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
