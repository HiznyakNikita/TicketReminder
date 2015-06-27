using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace TicketReminderLibrary
{
    public class Animation
    {
        DoubleAnimation _animation;

        public Animation() { _animation = new DoubleAnimation(); }

        public void Initialize(UIElement ui) { ui.BeginAnimation(UIElement.OpacityProperty, _animation); }

        public void SlowCloseWindow(UIElement element)
        {
            _animation.From = element.Opacity;
            _animation.To = 0.2;
            //_oa.RepeatBehavior = RepeatBehavior.Forever;
            _animation.AutoReverse = false;
            _animation.Duration = new Duration(TimeSpan.FromSeconds(1.2));
            _animation.Completed += OnClose;

            element.BeginAnimation(UIElement.OpacityProperty, _animation);
        }

        private void OnClose(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        public void SlowLoadButton(UIElement button)
        {
            _animation.From = button.Opacity;
            _animation.To = 0.0;
            //_oa.RepeatBehavior = RepeatBehavior.Forever;
            _animation.AutoReverse = true;
            _animation.Duration = new Duration(TimeSpan.FromMilliseconds(800d));

            button.BeginAnimation(UIElement.OpacityProperty, _animation);
        }
    }
}
