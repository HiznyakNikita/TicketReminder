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

        public EmailHelper(string password,
            string mailto, string caption)
        {
            this.password = password;
            this.mailto = mailto;
            this.caption = caption;
        }

        private static void SendMail(string password,
            string mailto, string caption, List<MessageArgs> args)
        {
            string message = CreateMessage(args);
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Properties.Settings.Default.UserEmail);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                SmtpClient client = new SmtpClient();
                client.Host = Properties.Settings.Default.AppSmtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(Properties.Settings.Default.UserEmail.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        private static string CreateMessage(List<MessageArgs> args)
        {
            string message = "";
            foreach(var arg in args)
                message+= arg.From + " - " + arg.To + " на " + arg.Date + " Поезд: " + arg.TrainNumber + " Количество мест: " + arg.PlacesCount + Environment.NewLine;
            return message;
        }

        public void Notify(List<MessageArgs> args)
        {
            SendMail(password, mailto, caption, args);
        }
    }
}