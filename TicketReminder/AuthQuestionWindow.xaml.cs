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
using TicketReminder.HelperClasses;
using DprcParser;
using System.Windows.Threading;

namespace TicketReminder
{
    /// <summary>
    /// Interaction logic for AuthQuestionWindow.xaml
    /// </summary>
    public partial class AuthQuestionWindow : Window
    {

        public static AuthQuestionWindow Instance { get; private set; }

        private static string[] TransitionEffects = new[] { "Fade" };
        private string TransitionType;
        private int EffectIndex = 0;
        private Animation _animation = new Animation();
        private DispatcherTimer timer;

        public AuthQuestionWindow()
        {
            InitializeComponent();
            Loaded += delegate { Load(); };
        }

        private void Load()
        {
            Instance = this;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2);
            timer.IsEnabled = true;
            timer.Tick += timer_Tick;
            timer.Start();

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
                btnNextStep.Visibility = System.Windows.Visibility.Visible;

                // Slide loading controls
                SlideLoad(imageFly);
                SlideLoad(imageTrain);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            SlideLoad(btnNextStep);
            SlideLoad(btnRegistration);
            timer.Stop();
            timer.IsEnabled = false;
        }

        #region Buttons Click Events
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

        private void btnNextStep_Click(object sender, RoutedEventArgs e)
        {
            btnNextStep.Visibility = System.Windows.Visibility.Collapsed;
            btnClose.Visibility = System.Windows.Visibility.Visible;
            btnSignIn.Visibility = System.Windows.Visibility.Visible;
            btnRegistration.Visibility = System.Windows.Visibility.Collapsed;

            canvasSignIn.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            new TicketReminder.Windows.MainAppWindow().Show();
            this.Close();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == "" || tbPass.Password == "")
                MessageBox.Show("Неверно указан логин или пароль!", "Ошибка", MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
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
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Ошибка авторизации!", "Ошибка", MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region Animation Method
        public void SlideLoad(FrameworkElement element)
        {
            try
            {
                TransitionType = TransitionEffects[EffectIndex].ToString();
                Storyboard StboardFadeOut = (Resources[string.Format("{0}Out", TransitionType.ToString())] as Storyboard).Clone();
                StboardFadeOut.Begin(element);
                Storyboard StboardFadeIn = Resources[string.Format("{0}In", TransitionType.ToString())] as Storyboard;
                StboardFadeIn.Begin(element);
            }
            catch (Exception) { }
        }
        #endregion

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
        private void menuSearchTicket_Click(object sender, RoutedEventArgs e)
        {
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
        private void menuMainWindow_Click(object sender, RoutedEventArgs e)
        {
            var win = AppWindow.IsWindowOpen<TicketReminder.Windows.MainAppWindow>();
            if (win != null)
                win.Focus();
            else
            {
                win = new TicketReminder.Windows.MainAppWindow();
                win.Show();
            }
            if (win != null && win.Focus() == false)
                win.Show();
        }
        #endregion
        }
    }
