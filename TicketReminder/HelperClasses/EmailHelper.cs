using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TicketReminder.HelperClasses;

namespace TicketReminder
{
    public class EmailHelper : INotifier
    {
        private string password;
        private string mailto;
        private string caption;
        private MessageArgs messageArgs;

        public EmailHelper(string password,
            string mailto, string caption, MessageArgs messageArgs)
        {
            this.password = password;
            this.mailto = mailto;
            this.caption = caption;
            this.messageArgs = messageArgs;
        }

        public static void SendMail(string password,
            string mailto, string caption, MessageArgs args)
        {
            string message = CreateMessage(args); 
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Properties.Settings.Default.UserDprcGovUaEmail);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                SmtpClient client = new SmtpClient();
                client.Host = Properties.Settings.Default.AppSmtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(Properties.Settings.Default.UserDprcGovUaEmail.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        private static string CreateMessage(MessageArgs args) 
        {
            return args.From + " - " + args.To + " на " + args.Date + "Поезд: " + args.TrainNumber + " Количество мест: " + args.PlacesCount;
        }

        public void Notify()
        {
            SendMail(password, mailto, caption, messageArgs);
        }
    }
}
