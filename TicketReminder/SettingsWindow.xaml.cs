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
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void btnAddVkontakteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (tbEmail.Text != "")
                Properties.Settings.Default.UserEmail = tbEmail.Text;
        }

        private void btnAddEmail_Click(object sender, RoutedEventArgs e)
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
    }
}
