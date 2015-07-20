using DprcParser;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using TicketReminder.DataClasses;

namespace TicketReminder
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public LogInWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            VkNet.VkApi api = new VkNet.VkApi();
            int lastCount = 0;
            Train train = Train.GetAllTrainInfo("ЗАПОРІЖЖЯ 1", "КИЇВ", "2015-06-28", "072П", 3);
            int currentCount = train.GetCountPlacesByCarType(new List<CarType>(){CarType.ReservedSeat,CarType.ReservedSeatFirm});
            if (currentCount > 0 && currentCount != lastCount)
                api.Messages.Send(162945005, false, "ЗАПОРІЖЖЯ 1 - КИЇВ 2015-06-28 072П місця - " + currentCount.ToString(), "Ticket reminder");
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "" && passwordBoxPass.Password != "")
            {
                Properties.Settings.Default.UserDprcGovUaEmail = tbLogin.Text;
                Properties.Settings.Default.UserDprcGovUaPassword = passwordBoxPass.Password;
            }
            Parser.Auth(Properties.Settings.Default.UserDprcGovUaEmail, Properties.Settings.Default.UserDprcGovUaPassword);
            dispatcherTimer.Interval = new TimeSpan(0,4,0);
            dispatcherTimer.Start();
        }
    }
}
