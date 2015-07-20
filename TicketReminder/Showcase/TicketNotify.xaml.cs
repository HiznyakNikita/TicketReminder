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

namespace TicketReminder.Showcase
{
    /// <summary>
    /// Логика взаимодействия для TicketNotify.xaml
    /// </summary>
    public partial class TicketNotify : UserControl
    {

        public TicketNotify()
        {
            InitializeComponent();
        }

        private void txtCountTicket_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (((Label)sender).Content != null)
            {
                switch (((Label)sender).Content.ToString().Length)
                {
                    case 1:
                        ((Label)sender).FontSize = 12;
                        ((Label)sender).Content = " " + ((Label)sender).Content;
                        break;
                    case 2:
                        ((Label)sender).FontSize = 12;
                        break;
                }
            }
        }
    }
}
