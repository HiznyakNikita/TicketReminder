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
    /// Логика взаимодействия для ToolTipNoTicket.xaml
    /// </summary>
    public partial class ToolTipNoTicket : UserControl
    {
        #region InfoText dependency property

        /// <summary>
        /// The tooltip details.
        /// </summary>
        public static readonly DependencyProperty InfoTextProperty =
            DependencyProperty.Register("InfoText",
                typeof(string),
                typeof(ToolTipNoTicket),
                new FrameworkPropertyMetadata(""));

        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText",
                typeof(string),
                typeof(ToolTipNoTicket),
                new FrameworkPropertyMetadata(""));

        /// <summary>
        /// A property wrapper for the <see cref="InfoTextProperty"/>
        /// dependency property:<br/>
        /// The tooltip details.
        /// </summary>
        public string InfoText
        {
            get { return (string)GetValue(InfoTextProperty); }
            set { SetValue(InfoTextProperty, value); }
        }

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        #endregion

        public ToolTipNoTicket()
        {
            InitializeComponent();
            
        }
    }
}
