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
using TicketReminder.Showcase;
using MahApps.Metro.Controls;
using System.Threading;
using System.Windows.Threading;
using TicketReminder.HelperClasses;
using DprcParser.DataClasses;
using DprcParser;
using TicketReminder.DataClasses;
using TicketReminder.UIHelperClasses;
using ClassAirPort.Json;
using System.Windows.Media.Animation;

namespace TicketReminder.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainAppWindow.xaml
    /// </summary>
    public partial class MainAppWindow : Window
    {
        public static MainAppWindow Instance { get; private set; }


        private static string[] TransitionEffects = new[] { "Fade" };
        private string TransitionType;
        private int EffectIndex = 0;

        private DispatcherTimer dispTimer;
        static DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public MainAppWindow()
        {
            InitializeComponent();
            
            // Add the city fom VK user information to load weather information ============================================!!!!!!!!!!
            LoadWeather("Kiev");
            
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            Instance = this;

            Loaded += delegate
            {

                LoadTimerCheckInternet();
            };

            if (Properties.Settings.Default.UserVkLogin != "" && Properties.Settings.Default.UserVkPassword != "")
            {
                VkontakteHelper.Authorize();
                SetUserAvatar();
            }

            SetUserInfo();

            //// TODO load stations names by 1-2-.. and all letters. Fix load bug

            cmbBoxPointFrom.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent,
                     new System.Windows.Controls.TextChangedEventHandler(ComboBox_TextChanged));
            cmbBoxPointTo.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent,
                     new System.Windows.Controls.TextChangedEventHandler(ComboBox_TextChanged));
            
        }

        public void cmbBoxPointTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LoadWeather(GetCityFromPointTo((cmbBoxPointTo.SelectedItem.ToString())));
            }
            catch (Exception) { }
        }

        private string GetCityFromPointTo(string pointTo)
        {
            if (pointTo.IndexOf('-') != -1)
                return pointTo.Substring(0, pointTo.IndexOf('-'));
            else if (pointTo.IndexOf(' ') != -1)
                return pointTo.Substring(0, pointTo.IndexOf(' '));
            else
                return pointTo;

        }

        private void Main_Activated(object sender, EventArgs e)
        {
            CheckInternet(sender, e);
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Runtime getting station names from dprc server
            if ((sender as ComboBox).Text != "")
            {
                (sender as ComboBox).ItemsSource = Parser.GetRunTimeNames((sender as ComboBox).Text);
                (sender as ComboBox).IsDropDownOpen = true;
            }
        }

        private void SetUserAvatar()
        {
            try
            {
                if (Properties.Settings.Default.UserVkLogin != "" && Properties.Settings.Default.UserVkPassword != "")
                {
                    Uri userAvatar = new Uri(VkontakteHelper.GetUserPhoto200());
                    BitmapImage userBitmapImage = new BitmapImage(userAvatar);
                    imageUser.Source = userBitmapImage;
                }
            }
            catch (Exception)
            {
                throw new VkNet.Exception.AccessDeniedException();
            }
        }

        private void LoadTimerCheckInternet()
        {
            //Dispatcher check internet connection
            dispTimer = new DispatcherTimer();
            dispTimer.Interval = new TimeSpan(0, 0, 3);
            dispTimer.IsEnabled = true;
            dispTimer.Tick += CheckInternet;
            dispTimer.Start();
        }

        private void LoadWeather(string city)
        {
            Weather _weatherCurrent = new Weather(city);
            weatherControl.DataContext = _weatherCurrent.GetWeather();
            txtTitleWeatherFrom.Text = _weatherCurrent.GetTitle();
        }

        //Check internet connection during program working
        private void CheckInternet(object sender, EventArgs e)
        {
            TicketReminder.HelperClasses.AppWindow app = new HelperClasses.AppWindow();
            if (app.IsInternet.Equals(true))
                imageInternet.Source = new BitmapImage(new Uri("pack://application:,,,/Images/wifi.png"));
            else
                imageInternet.Source = new BitmapImage(new Uri("pack://application:,,,/Images/no_wifi.png"));
        }

        private void panelTop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            //Stop Dispatcher timer
            dispTimer.Stop();
            this.Hide();
        }

        // Logout the application
        private void btnLeaveApp_Click(object sender, RoutedEventArgs e)
        {
            new AuthQuestionWindow().Show();
            this.Close();
        }

        private void btnSearchSettings_Click(object sender, RoutedEventArgs e)
        {
            new SearchParametersWindow().ShowDialog();
        }

        #region SettingsTab
        private void btnAddVkontakteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "" && passwordBoxVkPass.Password != "")
            {
                Properties.Settings.Default.UserVkLogin = tbLogin.Text;
                Properties.Settings.Default.UserVkPassword = passwordBoxVkPass.Password;
                Properties.Settings.Default.Save();
            }
            if (tbLogin.Text != "")
                Properties.Settings.Default.UserVkLogin = tbLogin.Text;
            VkontakteHelper.Authorize();
            SetUserInfo();
        }

        private void SetUserInfo()
        {
            SetUserAvatar();
            GetProfileInfo();
            txtTimeNow.Text = "";
            txtDateNow.Text = "";

            txtTimeNow.Text += DateTime.Today.Date.ToShortTimeString();
            txtDateNow.Text += DateTime.Today.Date.ToShortDateString();
        }

        private void GetProfileInfo()
        {
            User currentUser = VkontakteHelper.GetInfoUser();
            //Bind general panel controls
            if (currentUser.Country != "" || currentUser.City != "")
                txtBlockCityCountryGeneralPanel.Text = currentUser.Country != "" ? currentUser.City + ", " + currentUser.Country : currentUser.City;
            txtBlockUserNameGeneralPanel.Text = currentUser.FirstName;
            txtBlockUserSurNameGeneralPanel.Text = currentUser.LastName;

            //Bind Profile tab controls
            txtBlockCityProfileTab.Text = currentUser.City;
            txtBlockEmailProfileTab.Text = Properties.Settings.Default.UserEmail;
            txtBlockUserIdProfileTab.Text = currentUser.VkUserId.ToString();
            txtBlockFirstNameProfileTab.Text = currentUser.FirstName;
            txtBlockLastNameProfileTab.Text = currentUser.LastName;

            //Bind Settings app controls
            tbLogin.Text = Properties.Settings.Default.UserVkLogin;
            tbEmail.Text = Properties.Settings.Default.UserEmail;
        }

        private void btnAddEmail_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text != "" && passwordBoxEmailPass.Password != "")
            {
                Properties.Settings.Default.UserEmail = tbEmail.Text;
                Properties.Settings.Default.UserEmailPassword = passwordBoxEmailPass.Password;
                Properties.Settings.Default.Save();
            }
        }

        private void cmbBoxTrainNumber_DropDownOpened(object sender, EventArgs e)
        {
            cmbBoxTrainNumber.Items.Clear();
            //Runtime getting station names from dprc server
            if (cmbBoxPointFrom.Text != "" && cmbBoxPointTo.Text != "" && datePicker.SelectedDate != null)
            {
                string[] numbers = Parser.GetTrainsNumbers(cmbBoxPointFrom.Text, cmbBoxPointTo.Text, Helper.ConvertDate(datePicker.SelectedDate.Value.ToShortDateString()));
                foreach (var num in numbers)
                    cmbBoxTrainNumber.Items.Add(num);
            }
            cmbBoxTrainNumber.Items.Add("Все поезда");
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Search logic by parameters from search settings
            int lastCount = 0;
            int count = 0;
            if (cmbBoxTrainNumber.Text == "Все поезда")
            {
                List<Train> trains = Train.GetAllTrainsByRouteInfo(cmbBoxPointFrom.Text, cmbBoxPointTo.Text, Helper.ConvertDate(datePicker.SelectedDate.Value.ToShortDateString()), SearchSettings.Instance.EnableReserve, SearchSettings.Instance.ReservePriority);
                foreach (var train in trains)
                {
                    if (SearchSettings.Instance.PlaceTypes.Count != 0 && SearchSettings.Instance.CarTypes.Count != 0)
                    {
                        count = train.GetCountPlacesByCarAndPlaceTypes(SearchSettings.Instance.PlaceTypes, SearchSettings.Instance.CarTypes);
                    }
                    else if (SearchSettings.Instance.PlaceTypes.Count != 0 && SearchSettings.Instance.CarTypes.Count == 0)
                    {
                        count = train.GetCountPlacesByPlaceType(SearchSettings.Instance.PlaceTypes);
                    }
                    else if (SearchSettings.Instance.PlaceTypes.Count == 0 && SearchSettings.Instance.CarTypes.Count != 0)
                    {
                        count = train.GetCountPlacesByCarType(SearchSettings.Instance.CarTypes);
                    }
                    else
                        count = train.PlacesCount;
                }
                if (lastCount != count)
                {
                    NotifyAllTrains(trains);
                    lastCount = count;
                }
            }
            else
            {
                Train train = Train.GetAllTrainInfo(cmbBoxPointFrom.Text, cmbBoxPointTo.Text, Helper.ConvertDate(datePicker.SelectedDate.Value.ToShortDateString()), cmbBoxTrainNumber.Text, cmbBoxTrainNumber.SelectedIndex, SearchSettings.Instance.EnableReserve, SearchSettings.Instance.ReservePriority);
                if (SearchSettings.Instance.PlaceTypes.Count != 0 && SearchSettings.Instance.CarTypes.Count != 0)
                {
                    count = train.GetCountPlacesByCarAndPlaceTypes(SearchSettings.Instance.PlaceTypes, SearchSettings.Instance.CarTypes);
                }
                else if (SearchSettings.Instance.PlaceTypes.Count != 0 && SearchSettings.Instance.CarTypes.Count == 0)
                {
                    count = train.GetCountPlacesByPlaceType(SearchSettings.Instance.PlaceTypes);
                }
                else if (SearchSettings.Instance.PlaceTypes.Count == 0 && SearchSettings.Instance.CarTypes.Count != 0)
                {
                    count = train.GetCountPlacesByCarType(SearchSettings.Instance.CarTypes);
                }
                else
                    count = train.PlacesCount;
                if (lastCount != count)
                {
                    foreach (INotifier n in SearchSettings.Instance.Notifiers)
                    {
                        n.Notify(new List<MessageArgs>{new MessageArgs
                    {
                        From = cmbBoxPointFrom.Text,
                        To = cmbBoxPointTo.Text,
                        Date = Helper.ConvertDate(datePicker.SelectedDate.Value.ToShortDateString()),
                        TrainNumber = train.Number,
                        PlacesCount = count.ToString()
                    }});
                    }
                    new Notify("New ticket!",
                        new List<MessageArgs>{new MessageArgs
                    {
                        From = cmbBoxPointFrom.Text,
                        To = cmbBoxPointTo.Text,
                        Date = Helper.ConvertDate(datePicker.SelectedDate.Value.ToShortDateString()),
                        TrainNumber = train.Number,
                        PlacesCount = train.PlacesCount.ToString()
                    }}).ShowNotify();
                    SetToolTipNotify();
                    lastCount = count;
                    TicketsViewModel t = new TicketsViewModel(train.Cars, null);
                    this.DataContext = t;
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
                    Date = Helper.ConvertDate(datePicker.SelectedDate.Value.ToShortDateString()),
                    TrainNumber = train.Number,
                    PlacesCount = train.PlacesCount.ToString()
                });
            foreach (INotifier n in SearchSettings.Instance.Notifiers)
                n.Notify(args);
            new Notify("New ticket!",args).ShowNotify();
            SetToolTipNotify();
            List<Car> cars = new List<Car>();
            foreach (var t in trains)
                cars.AddRange(t.Cars);
            TicketsViewModel ticketViewModel = new TicketsViewModel(cars, trains);
            this.DataContext = ticketViewModel;
        }

        private void SetToolTipNotify()
        {
            ToolTipNoTicket toolTipNoTicket = new ToolTipNoTicket();
            toolTipNoTicket.HeaderText = "New ticket for you";
            toolTipNoTicket.InfoText = "You have new ticket. For details go on ticket tab";
        }

        private void btnSearchSearchTabs_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            dispatcherTimer.Interval = new TimeSpan(0, SearchSettings.Instance.CheckPeriod, 0);
            dispatcherTimer.IsEnabled = true;
            dispatcherTimer.Start();
        }

        private void btnHelpWindow_Click(object sender, RoutedEventArgs e)
        {
            TicketReminder.Windows.HelpWindow help = new Windows.HelpWindow(this);
            help.Show();
        }


        private void btnAddEmail_Unloaded(object sender, RoutedEventArgs e)
        {
            ////WHEN WE STARTED WE ADD SETTINGS FIRST TIME AND GO TO MAIN WINDOW
            //if (isFirstLaunch)
            //{
            //    new MainWindow().Show();
            //    this.Close();
            //}
        }

        #endregion

        #region Weather change. Forecast or current
        private void checkBoxWeather_Checked(object sender, RoutedEventArgs e)
        {
            listBoxForecastWeather.Visibility = System.Windows.Visibility.Visible;
            Weather _weatherForecast = new Weather("Kiev", true);

            var list = _weatherForecast.GetWeatherForecast();
            listBoxForecastWeather.ItemsSource = list;

            txtWeatherCalled.Text = "Forecast weather";

            TransitionType = TransitionEffects[EffectIndex].ToString();
            Storyboard StboardFadeOut = (Resources[string.Format("{0}Out", TransitionType.ToString())] as Storyboard).Clone();
            StboardFadeOut.Begin(weatherControl);
            Storyboard StboardFadeIn = Resources[string.Format("{0}In", TransitionType.ToString())] as Storyboard;
            StboardFadeIn.Begin(listBoxForecastWeather);
        }

        private void checkBoxWeather_Unchecked(object sender, RoutedEventArgs e)
        {
            txtWeatherCalled.Text = "Current weather";

            TransitionType = TransitionEffects[EffectIndex].ToString();
            Storyboard StboardFadeOut = (Resources[string.Format("{0}Out", TransitionType.ToString())] as Storyboard).Clone();
            StboardFadeOut.Begin(listBoxForecastWeather);
            Storyboard StboardFadeIn = Resources[string.Format("{0}In", TransitionType.ToString())] as Storyboard;
            StboardFadeIn.Begin(weatherControl);
        }

        #endregion
    }
}
