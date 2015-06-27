using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TicketReminder.Showcase;
using TicketReminderLibrary;
using TicketReminder.HelperClasses;
using DprcParser;
//using Hardcodet.Wpf.TaskbarNotification;

namespace TicketReminder
{
    /// <summary>
    /// Interaction logic for AuthQuestionWindow.xaml
    /// </summary>
    public partial class AuthQuestionWindow : Window
    {
        //TaskbarIcon iconF = new TaskbarIcon();
        Animation _animation = new Animation();
        DoubleAnimation _anime;
        int c = 1;
        int countWindow = 1;

        public AuthQuestionWindow()
        {
            InitializeComponent();
            Loaded += delegate { Load(); };
        }

        private void Load()
        {
            if (Properties.Settings.Default.UserDprcGovUaEmail != "" &&
                    Properties.Settings.Default.UserDprcGovUaPassword != "")
            {
                //TODO MAIN WINDOW SHOWED
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
                this.Close();
            }
            else
            {
                _animation.Initialize(this);
                SlowLoadButton(btnNextStep);
                SlowLoadButton(btnRegistration);

                UserNotification noti = new UserNotification();

                Sound.Location = Environment.CurrentDirectory + "\\Sounds\\Iphone_Ringtone_freetone.at.ua.mp3";
                Sound sound = new Sound(Sound.Location);

                noti.MainText.Text = "Ticket";
                noti.HeaderBalloon.Text = "New ticket:";
                noti.AllUnReadMessagesCount.Text = DateTime.Now.ToString();
            }

            //sound.PlaySound();
            //MyNotification.ShowCustomBalloon(noti, PopupAnimation.Slide, 5000);
            
            //AppSettings app = AppSettings.Instance;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterBrowser registerBrowser = new RegisterBrowser();
            registerBrowser.ShowDialog();
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MyNotification.Visibility = System.Windows.Visibility.Collapsed;
            _animation.SlowCloseWindow(this);
        }

        private void OnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNextStep_Click(object sender, RoutedEventArgs e)
        {

            Sound sound = new Sound(Sound.Location);
            sound.PlaySound();

            btnNextStep.Visibility = System.Windows.Visibility.Collapsed;

            btnClose.Visibility = System.Windows.Visibility.Visible;
            btnSignIn.Visibility = System.Windows.Visibility.Visible;
            btnRegistration.Visibility = System.Windows.Visibility.Collapsed;

            canvasSignIn.Visibility = System.Windows.Visibility.Visible;
        }

        #region Animation 
        public void SlowLoadButton(Button button)
        {
            _anime = new DoubleAnimation();
            _anime.From = Opacity;
            _anime.To = 0.0;
            //_oa.RepeatBehavior = RepeatBehavior.Forever;
            _anime.AutoReverse = true;
            _anime.Duration = new Duration(TimeSpan.FromMilliseconds(1000d));

            button.BeginAnimation(OpacityProperty, _anime);
        }
        #endregion

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            new RegisterBrowser().ShowDialog();
            AppWindow.ActualWindow = this;
            //this.Hide();
        }

        #region MenuItems in Tray. Code behind

        private void menuShowSettingsApp_Click(object sender, RoutedEventArgs e)
        {
            var win = AppWindow.IsWindowOpen<SettingsAppWindow>();

            if (win != null)
                win.Focus();
            else
            {
                win = new SettingsAppWindow();
                win.Show();
            }  

        }

        private void menuStatistic_Click(object sender, RoutedEventArgs e)
        {
            var win = AppWindow.IsWindowOpen<TicketReminder.Windows.StatisticWindow>();

            if (win != null)
                win.Focus();
            else
            {
                win = new TicketReminder.Windows.StatisticWindow();
                win.Show();
            }  
        }

        private void menuSearchTicket_Click(object sender, RoutedEventArgs e)
        {
            //ShowWindow<MainWindow>();

            var win = AppWindow.IsWindowOpen<MainWindow>();

            if (win != null)
                win.Focus();
            else
            {
                win = new MainWindow();
                win.Show();
            }
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            MyNotification.Visibility = System.Windows.Visibility.Collapsed;
            Environment.Exit(0);
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == "" || tbPass.Password == "")
                MessageBox.Show("Неверно указан логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                try
                {
                    Parser.Auth(tbLogin.Text, tbPass.Password);
                    Properties.Settings.Default.UserDprcGovUaEmail = tbLogin.Text;
                    Properties.Settings.Default.UserDprcGovUaPassword = tbPass.Password;
                    SettingsWindow settingsWindow = new SettingsWindow(true);
                    settingsWindow.ShowDialog();
                }
                catch(UnauthorizedAccessException)
                {
                    MessageBox.Show("Ошибка авторизации!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //public void ShowWindow<T>() where T: Window
        //{
        //    T item = (T)Activator.CreateInstance(typeof(T));
        //    var win = AppWindow.IsWindowOpen<T>();

        //    if (win != null)
        //        win.Focus();
        //    else
        //    {
        //        win = item;
        //        win.Show();
        //    }
        //}
        #endregion
    }
}
