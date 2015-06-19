using DprcParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        //System.Windows.Forms.WebBrowser webBrowser1 = new System.Windows.Forms.WebBrowser();

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
            RegisterBrowser registerBrowser = new RegisterBrowser();
            registerBrowser.Show();
        }



        private void webBrowser1_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            HtmlElement span;
            var spans = ((HtmlDocument)webBrowser1.Document).GetElementsByTagName("SPAN");
            List<HtmlElement> elements = new List<HtmlElement>();
            foreach (var item in spans)
            {
                if (((HtmlElement)item).InnerText != null && ((HtmlElement)item).InnerText.Contains("другую"))
                {
                    span = (HtmlElement)item;
                    span.InvokeMember("click");
                    var img = ((HtmlDocument)webBrowser1.Document).GetElementById("captcha");
                }
            }
        }
    }
}
