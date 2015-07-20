using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;

namespace TicketReminder
{
    public partial class RegisterBrowser : Form
    {
        bool isRegisterLoad = false;
        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        
        public RegisterBrowser()
        {
            InitializeComponent();
            webBrowser1.ScrollBarsEnabled = false;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (isRegisterLoad)
            {
                webBrowser1.Document.GetElementById("register_form").Focus();
                isRegisterLoad = false;
                //Check for validate registration form
                if (webBrowser1.Url.Equals(new Uri("https://dprc.gov.ua/register.php")) && webBrowser1.Document.GetElementById("aggr_error") == null
                    && webBrowser1.Document.GetElementById("email_error") == null && webBrowser1.Document.GetElementById("pwd_error") == null
                    && webBrowser1.Document.GetElementById("phone_error") == null && webBrowser1.Document.GetElementById("cap_error") == null)
                {
                    System.Windows.MessageBox.Show("На указанную почту отправлено сообщение для подтверждения регистрации. Пожалуйста подтвердите регистрацию.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    AuthQuestionWindow authQuestionWindow = new AuthQuestionWindow();
                    authQuestionWindow.ShowDialog();
                    this.Close();
                }
                //USED FOR PRINT SCREEN CAPTCHA MAYBE HELP IN TODO
                //Rectangle bounds = Screen.GetBounds(Point.Empty);
                //Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);

                //using (Graphics g = Graphics.FromImage(bitmap))
                //{
                //    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                //}
                //Bitmap captcha = CopyBitmap(bitmap, new Rectangle(573,410,133,40));
                //RegistrationWindow registrationWindow = new RegistrationWindow(captcha);
                //registrationWindow.Show();
                //dispatcherTimer.Stop();
                //this.Close();
            }    
        }
       
        private void RegisterBrowser_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://dprc.gov.ua/index.php?set_language=2");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {          
            List<HtmlElement> elements = new List<HtmlElement>();
            HtmlElement a;
            a = (HtmlElement)webBrowser1.Document.GetElementById("register");
            a.InvokeMember("click");

            //var spans = webBrowser1.Document.GetElementsByTagName("A");
            //foreach (var item in spans)
            //{
            //    if (((HtmlElement)item).InnerText != null && ((HtmlElement)item).InnerText.Contains("Создать учётную запись"))
            //    {
            //        span = (HtmlElement)item;
            //        span.InvokeMember("click");
            //    }
            //}
            isRegisterLoad = true;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0,500);
            dispatcherTimer.Start();
        }

        Bitmap CopyBitmap(Image src, Rectangle rect)
        {
            var ret = new Bitmap(rect.Width, rect.Height);
            using (var g = Graphics.FromImage(ret))
                g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel);
            return ret;
        }
    }
}
