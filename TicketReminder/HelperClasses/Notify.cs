using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using TicketReminder.Windows;

namespace TicketReminder.HelperClasses
{
    public class Notify
    {
        private MenuItem menuNewTicket = new MenuItem();
        private MessageArgs messageArgs = new MessageArgs();

        public static bool IsNotify = true;

        public string HeaderNotify { get; set; }
        public string MainTextNotify { get; set; }
        public int CountTickets { get; set; }
        PopupAnimation AnimationNotify { get; set; }
        public int? Timeout { get; set; }

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="headerNotify">The header of notification</param>
        /// <param name="mainNotify">Main text of notification</param>
        /// <param name="countTicket">Count tickets that have searched</param>
        /// <param name="animation"></param>
        /// <param name="timeout"></param>
        public Notify(string headerNotify, List<MessageArgs> ListArgs,
            PopupAnimation animation = PopupAnimation.Slide, int? timeout = 5000)
        {
            HeaderNotify = headerNotify;
            foreach (var args in ListArgs)
            {
                MainTextNotify  = "You have new ticket!" + Environment.NewLine +
                                        "From: " + args.From + " To " + args.To + Environment.NewLine +
                                        "Date: " + args.Date + Environment.NewLine +
                                        "Number place: " + args.PlacesCount + " | " +
                                        "Train number: " + args.TrainNumber + Environment.NewLine;
            }
            AnimationNotify = animation;
            Timeout = timeout;
        }

        public Notify() { }

        public void ShowNotify()
        {
            MainAppWindow win = MainAppWindow.Instance;

            Sound.Location = Environment.CurrentDirectory + "\\Sounds\\Iphone_Ringtone_freetone.at.ua.mp3";
            Sound sound = new Sound(Sound.Location);
            sound.PlaySound();
            Notification();
        }

        public void NoNotification()
        {
            AuthQuestionWindow auth = AuthQuestionWindow.Instance;
            MainAppWindow win = MainAppWindow.Instance;

            win.txtInfo.Text = "";
            win.ticketNotify.txtCountTicket.Content = 0;
            win.ticketNotify.borderNotif.Background = System.Windows.Media.Brushes.LightBlue;

            auth.menuNewTicketItme.IsEnabled = false;
            auth.menuNewTicketItme.Background = System.Windows.Media.Brushes.White;
        }

        private void Notification()
        {
            AuthQuestionWindow auth = AuthQuestionWindow.Instance;
            MainAppWindow win = MainAppWindow.Instance;

            TicketReminder.Showcase.UserNotification userNotify = new Showcase.UserNotification();
            userNotify.MainText.Text = MainTextNotify;
            userNotify.HeaderBalloon.Text = HeaderNotify;

            if (Notify.IsNotify == true)
                auth.MyNotification.ShowCustomBalloon(userNotify, AnimationNotify, Timeout);

            auth.menuNewTicketItme.Background = System.Windows.Media.Brushes.LightBlue;
            auth.menuNewTicketItme.IsEnabled = true;
            auth.menuNewTicketItme.Click += delegate { win.tabItemTicket.IsSelected = true; };

            win.ticketNotify.txtCountTicket.Content = Convert.ToString(CountTickets);
            win.ticketNotify.borderNotif.Background = System.Windows.Media.Brushes.DarkGreen;
        }
    }
}
