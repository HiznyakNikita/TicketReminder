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

namespace TicketReminder.Windows
{
    /// <summary>
    /// Логика взаимодействия для HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : MetroWindow
    {
        public HelpWindow(Window win)
        {
            InitializeComponent();
            LoadTab(win);
        }

        private void LoadTab(Window win)
        {
            if (win != null && win.IsActive == true)
            {
                switch (win.Name)
                {
                    case "Registration":
                        tabRegistration.IsSelected = true;
                        break;
                    case "Settings":
                        tabSettings.IsSelected = true;
                        break;
                    default:
                        break;
                }
            }
            else
                return;
        }
    }
}
