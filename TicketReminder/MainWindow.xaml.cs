using DprcParser;
using MahApps.Metro.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TicketReminder.DataClasses;
using TicketReminder.HelperClasses;

namespace TicketReminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        static DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            //AuthQuestionWindow authQuestionWindow = new AuthQuestionWindow();
            //authQuestionWindow.ShowDialog();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            // TODO load stations names by 1-2-.. and all letters. Fix load bug
            cmbBoxPointFrom.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent,
                      new System.Windows.Controls.TextChangedEventHandler(ComboBox_TextChanged));
            cmbBoxPointTo.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent,
                      new System.Windows.Controls.TextChangedEventHandler(ComboBox_TextChanged));
            
            Parser.Auth(Properties.Settings.Default.UserDprcGovUaEmail, Properties.Settings.Default.UserDprcGovUaPassword);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Search logic by parameters from search settings
            int lastCount = 0;
            int count = 0;
            if (cmbBoxTrainNumber.Text == "Все поезда")
            {
                List<Train> trains = Train.GetAllTrainsByRouteInfo(cmbBoxPointFrom.Text, cmbBoxPointTo.Text, Helper.ConvertDate(datePickerDate.SelectedDate.Value.ToShortDateString()), SearchSettings.Instance.EnableReserve, SearchSettings.Instance.ReservePriority);
                foreach (var train in trains)
                    //count += GetCountPlaces(train);
                if (lastCount != count)
                {
                    NotifyAllTrains(trains);
                    lastCount = count;
                }
            }
            else
            {
                Train train = Train.GetAllTrainInfo(cmbBoxPointFrom.Text, cmbBoxPointTo.Text, Helper.ConvertDate(datePickerDate.SelectedDate.Value.ToShortDateString()), cmbBoxTrainNumber.Text, cmbBoxTrainNumber.SelectedIndex, SearchSettings.Instance.EnableReserve, SearchSettings.Instance.ReservePriority);
                //count = //GetCountPlaces(train);
                if (lastCount != count)
                {
                    foreach (INotifier n in SearchSettings.Instance.Notifiers)
                    {
                        n.Notify(new List<MessageArgs>{new MessageArgs
                    {
                        From = cmbBoxPointFrom.Text,
                        To = cmbBoxPointTo.Text,
                        Date = Helper.ConvertDate(datePickerDate.SelectedDate.Value.ToShortDateString()),
                        TrainNumber = train.Number,
                        PlacesCount = train.PlacesCount.ToString()
                    }});
                    }
                }
            }
        }

        private void NotifyAllTrains(List<Train> trains)
        {
            List<MessageArgs> args = new List<MessageArgs>();
            foreach (var train in trains)
                args.Add(new MessageArgs
                {
                    From = cmbBoxPointFrom.Text,
                    To = cmbBoxPointTo.Text,
                    Date = Helper.ConvertDate(datePickerDate.SelectedDate.Value.ToShortDateString()),
                    TrainNumber = train.Number,
                    PlacesCount = train.PlacesCount.ToString()
                });
            foreach (INotifier n in SearchSettings.Instance.Notifiers)
                n.Notify(args);
        }

        //UNCOMMENT WHEN DO MULTIPLY SELECT COMBOBOXES
        //private int GetCountPlaces(Train train)
        //{
        //    if(SearchSettings.Instance.CarType!="" && SearchSettings.Instance.PlaceType=="")
        //    {
        //        return train.GetCountPlacesByCarType(new List<CarType> { SearchSettings.Instance.CarType });
        //    }
        //    else if (SearchSettings.Instance.CarType == "" && SearchSettings.Instance.PlaceType != "")
        //    {
        //        return train.GetCountPlacesByPlaceType(new List<PlaceType> { SearchSettings.Instance.PlaceType });
        //    }
        //    else if (SearchSettings.Instance.CarType != "" && SearchSettings.Instance.PlaceType != "")
        //    {
        //        return train.GetCountPlacesByCarAndPlaceTypes(new List<PlaceType> { SearchSettings.Instance.PlaceType }, new List<CarType> { SearchSettings.Instance.CarType });
        //    }
        //    else
        //    {
        //        return train.GetPlacesCount();
        //    }
        //}

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Runtime getting station names from dprc server
            if ((sender as ComboBox).Text != "")
            {
                (sender as ComboBox).ItemsSource = Parser.GetRunTimeNames((sender as ComboBox).Text);
                (sender as ComboBox).IsDropDownOpen = true;
            }
        }

        private void cmbBoxTrainNumber_DropDownOpened(object sender, EventArgs e)
        {
            //Runtime getting station names from dprc server
            if (cmbBoxPointFrom.Text != "" && cmbBoxPointTo.Text != "" && datePickerDate.SelectedDate != null)
                cmbBoxTrainNumber.ItemsSource = Parser.GetTrainsNumbers(cmbBoxPointFrom.Text, cmbBoxPointTo.Text, Helper.ConvertDate(datePickerDate.SelectedDate.Value.ToShortDateString()));
            cmbBoxTrainNumber.Items.Add("Все поезда");
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchParametersWindow searchParametersWindow = new SearchParametersWindow();
            searchParametersWindow.ShowDialog();
            // Run search timer with check period from search settings
            dispatcherTimer.Interval = new TimeSpan(0, 0, SearchSettings.Instance.CheckPeriod);
            dispatcherTimer.Start();
        }

        private void menuItemSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(false);
            settingsWindow.ShowDialog();
        }

        private void menuItemCancelReserve_Click(object sender, RoutedEventArgs e)
        {
            //Cancel reserve ticket. Need to buy a ticket by user with credit card number and other settings that not support in program(To ensure user safety)
            Parser.CancelReserveTicket();
        }

        private void btnHelpWindow_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Тут буде певна підказка");
        }
    }
}
