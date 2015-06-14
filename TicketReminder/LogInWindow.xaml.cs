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
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "" && passwordBoxPass.Password != "")
            {
                Properties.Settings.Default.UserDprcGovUaEmail = tbLogin.Text;
                Properties.Settings.Default.UserDprcGovUaPassword = passwordBoxPass.Password;
            }
        }
    }
}
