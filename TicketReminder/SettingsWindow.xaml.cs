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
using System.Windows.Shapes;
namespace TicketReminder
{

    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        private bool isFirstLaunch;
        public SettingsWindow(bool isFirstLaunch)
        {
            InitializeComponent();
            this.isFirstLaunch = isFirstLaunch;
        }

        private void btnAddVkontakteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "" && passwordBoxVkPass.Password != "")
            {
                Properties.Settings.Default.UserVkLogin = tbLogin.Text;
                Properties.Settings.Default.UserVkPassword = passwordBoxVkPass.Password;
            }
            if (tbLogin.Text != "")
                Properties.Settings.Default.UserVkLogin = tbLogin.Text;
            VkontakteHelper.Authorize();
        }

        private void btnAddEmail_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text != "" && passwordBoxEmailPass.Password != "")
            {
                Properties.Settings.Default.UserEmail = tbEmail.Text;
                Properties.Settings.Default.UserEmailPassword = passwordBoxEmailPass.Password;
            }
        }

        private void btnHelpWindow_Click(object sender, RoutedEventArgs e)
        {
            TicketReminder.Windows.HelpWindow help = new Windows.HelpWindow(this);
            help.Show();
        }


        ///WHAT IS???
        private void passwordBoxVkPass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBoxVkPass.Password == "")
                passwordBoxVkPass.Password = "123456789";
        }

        private void passwordBoxVkPass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBoxVkPass.Password == "123456789")
                passwordBoxVkPass.Password = "";  
        }

        private void btnAddEmail_Unloaded(object sender, RoutedEventArgs e)
        {
            //WHEN WE STARTED WE ADD SETTINGS FIRST TIME AND GO TO MAIN WINDOW
            if (isFirstLaunch)
            {
                new MainWindow().Show();
                this.Close();
            }
        }

        private void passwordBoxEmailPass_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBoxEmailPass.Password == "123456789")
                passwordBoxEmailPass.Password = "";  
        }

        private void passwordBoxEmailPass_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passwordBoxEmailPass.Password == "")
                passwordBoxEmailPass.Password = "123456789";
        }
    }
}
