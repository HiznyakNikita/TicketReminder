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

namespace TicketReminder.Showcase
{

    public enum MessageBoxType
    {
        ConfirmationWithYesNo = 0,
        ConfirmationWithYesNoCancel,
        Information,
        Error,
        Warning
    }

    public enum MessageBoxImage
    {
        Warning = 0,
        Question,
        Information,
        Error,
        None
    }
    /// <summary>
    /// Логика взаимодействия для MetroMessageBox.xaml
    /// </summary>
    public partial class MetroMessageBox : Window
    {
        static MetroMessageBox messageBox;
        static MessageBoxResult result = MessageBoxResult.No;

        public MetroMessageBox()
        {
            InitializeComponent();
            this.MessageTitle.Text = "";
            
        }

        public static MessageBoxResult Show(string caption, string msg, MessageBoxType type)
        {
            switch (type)
            {
                case MessageBoxType.ConfirmationWithYesNo:
                    return Show(caption, msg, MessageBoxButton.YesNo, MessageBoxImage.Question);
                case MessageBoxType.ConfirmationWithYesNoCancel:
                    return Show(caption, msg, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                case MessageBoxType.Information:
                    return Show(caption, msg, MessageBoxButton.OK, MessageBoxImage.Information);
                case MessageBoxType.Error:
                    return Show(caption, msg, MessageBoxButton.OK, MessageBoxImage.Error);
                case MessageBoxType.Warning:
                    return Show(caption, msg, MessageBoxButton.OK, MessageBoxImage.Warning);
                default:
                    return MessageBoxResult.No;
            }
        }

        public static MessageBoxResult Show(string msg, MessageBoxType type)
        {
            return Show(string.Empty, msg, type);
        }

        public static MessageBoxResult Show(string msg)
        {
            return Show(string.Empty, msg, MessageBoxButton.OK, MessageBoxImage.None);
        }

        public static MessageBoxResult Show(string caption, string text)
        {
            return Show(caption, text, MessageBoxButton.OK, MessageBoxImage.None);
        }

        public static MessageBoxResult Show(string caption, string text, MessageBoxButton button)
        {
            return Show(caption, text, button, MessageBoxImage.None);
        }

        public static MessageBoxResult Show(string caption, string text, MessageBoxButton button, MessageBoxImage image)
        {
            messageBox = new MetroMessageBox();
            messageBox.textMessage.Text = text;
            messageBox.MessageTitle.Text = caption;
            setVisibilityOfButtons(button);
            setImageOfMessageBox(image);
            messageBox.ShowDialog();
            return result;
        }

        private static void setVisibilityOfButtons(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                    messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    messageBox.btnOk.Focus();
                    break;
                case MessageBoxButton.OKCancel:
                    messageBox.btnCancel.Focus();
                    break;
                case MessageBoxButton.YesNoCancel:
                    messageBox.btnOk.Visibility = Visibility.Collapsed;
                    messageBox.btnCancel.Focus();
                    break;
                default:
                    break;
            }
        }

        private static void setImageOfMessageBox(MessageBoxImage image)
        {
            MetroMessageBox metro = new MetroMessageBox();
            SolidColorBrush brush = new SolidColorBrush();
            switch (image)
            {
                case MessageBoxImage.Warning:
                    {
                        messageBox.setImage("warning.png");
                        messageBox.mainGrid.Background = (LinearGradientBrush)metro.Resources["yellowBackground"];
                    }
                    break;
                case MessageBoxImage.Question:
                    {
                        messageBox.setImage("question.png");
                        messageBox.mainGrid.Background = (LinearGradientBrush)metro.Resources["greenBackground"];
                    }
                    break;
                case MessageBoxImage.Information:
                    {
                        messageBox.setImage("information.png");
                        messageBox.mainGrid.Background = (LinearGradientBrush)metro.Resources["blueBackground"];
                    }
                    break;
                case MessageBoxImage.Error:
                    {
                        messageBox.setImage("error.png");
                        messageBox.mainGrid.Background = (LinearGradientBrush)metro.Resources["redBackground"];
                    }
                    break;
                default:
                    messageBox.image.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void setImage(string imageName)
        {
            string uri = string.Format("/Icons/{0}", imageName);
            var uriSource = new Uri(uri, UriKind.RelativeOrAbsolute);
            image.Source = new BitmapImage(uriSource);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnOk)
                result = MessageBoxResult.OK;
            else if (sender == btnCancel)
                result = MessageBoxResult.Cancel;
            else
                result = MessageBoxResult.None;

            messageBox.Close();
            messageBox = null;
        }

        protected override void OnActivated(EventArgs e)
        {
            if (Owner != null)
            {
                if (Owner.WindowState == WindowState.Maximized)
                {
                    Left = 0;
                    Top = (Owner.Height - 200) / 2;
                    Width = Owner.Width;
                }
                else
                {
                    Left = Owner.Left + 1;
                    Top = Owner.Top + ((Owner.Height - 200) / 2);
                    Width = Owner.Width - 2;
                }
            }

            base.OnActivated(e);
        }
    }
}
