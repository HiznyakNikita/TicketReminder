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
    /// Interaction logic for AuthQuestionWindow.xaml
    /// </summary>
    public partial class AuthQuestionWindow : Window
    {
        public AuthQuestionWindow()
        {
            InitializeComponent();
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.ShowDialog();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registerWindow = new RegistrationWindow();
            registerWindow.ShowDialog();
        }
    }
}
