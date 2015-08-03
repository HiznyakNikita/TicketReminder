using Hardcodet.Wpf.TaskbarNotification;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicketReminder.HelperClasses;

namespace TicketReminder.Showcase
{
    /// <summary>
    /// Логика взаимодействия для UserNotification.xaml
    /// </summary>
    public partial class UserNotification : UserControl
    {
        public UserNotification()
        {
            InitializeComponent();
            TaskbarIcon.AddBalloonClosingHandler(this, OnBalloonClosing);
        }

        private bool isClosing = false;

        //#region BalloonText dependency property

        ///// <summary>
        ///// Description
        ///// </summary>
        //public static readonly DependencyProperty BalloonTextProperty =
        //    DependencyProperty.Register("BalloonText",
        //        typeof (string),
        //        typeof(BlueBalloon),
        //        new FrameworkPropertyMetadata(""));

        ///// <summary>
        ///// A property wrapper for the <see cref="BalloonTextProperty"/>
        ///// dependency property:<br/>
        ///// Description
        ///// </summary>
        //public string BalloonText
        //{
        //    get { return (string) GetValue(BalloonTextProperty); }
        //    set { SetValue(BalloonTextProperty, value); }
        //}

        //#endregion


        //<summary>
        /// By subscribing to the <see cref="TaskbarIcon.BalloonClosingEvent"/>
        /// and setting the "Handled" property to true, we suppress the popup
        /// from being closed in order to display the custom fade-out animation.
        /// </summary>
        [STAThread]
        private void OnBalloonClosing(object sender, RoutedEventArgs e)
        {
            e.Handled = true; //suppresses the popup from being closed immediately
            isClosing = true;
        }


        /// <summary>
        /// Resolves the <see cref="TaskbarIcon"/> that displayed
        /// the balloon and requests a close action.
        /// </summary>
        [STAThread]
        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //the tray icon assigned this attached property to simplify access
            
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.CloseBalloon();
        }

        /// <summary>
        /// If the users hovers over the balloon, we don't close it.
        /// </summary>
        [STAThread]
        private void grid_MouseEnter(object sender, MouseEventArgs e)
        {
            //if we're already running the fade-out animation, do not interrupt anymore
            //(makes things too complicated for the sample)
            if (isClosing) return;

            //the tray icon assigned this attached property to simplify access
            TaskbarIcon taskbarIcon = TaskbarIcon.GetParentTaskbarIcon(this);
            taskbarIcon.ResetBalloonCloseTimer();
        }


        /// <summary>
        /// Closes the popup once the fade-out animation completed.
        /// The animation was triggered in XAML through the attached
        /// BalloonClosing event.
        /// </summary>
        [STAThread]
        private void OnFadeOutCompleted(object sender, EventArgs e)
        {
            Popup pp = (Popup)Parent;
            pp.IsOpen = false;
        }

        private void btnShowTicket_Click(object sender, RoutedEventArgs e)
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
    }
}
