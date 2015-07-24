using DprcParser.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketReminder.HelperClasses;
using VkNet;
using VkNet.Enums.Filters;

namespace TicketReminder
{
    public class VkontakteHelper : INotifier
    {
        private static VkApi v = new VkApi();

        public static void Authorize()
        {
            try
            {
                v.Authorize(3921202, Properties.Settings.Default.UserVkLogin, Properties.Settings.Default.UserVkPassword, VkNet.Enums.Filters.Settings.All);
            }
            catch(VkNet.Exception.VkApiAuthorizationException)
            { }
        }

        private static void SendMessage(List<MessageArgs> args)
        {
            try
            {
                v.Messages.Send((long)v.UserId, false, CreateMessage(args) , "Билеты Укрзалізниця");
            }
            catch (VkNet.Exception.VkApiAuthorizationException)
            { }
        }

        private static string CreateMessage(List<MessageArgs> args)
        {
            string message = "";
            foreach (var arg in args)
                message += arg.From + " - " + arg.To + " на " + arg.Date + " Поезд: " + arg.TrainNumber + " Количество мест: " + arg.PlacesCount + Environment.NewLine;
            return message;
        }

        public void Notify(List<MessageArgs> args)
        {
            SendMessage(args);
        }

        public static User GetInfoUser()
        {
            User userToReturn = new User();
            v.Authorize(Convert.ToInt32(Properties.Settings.Default.VkAppId), Properties.Settings.Default.UserVkLogin, Properties.Settings.Default.UserVkPassword, Settings.All);
            VkNet.Model.User vkUser = new VkNet.Model.User();
            vkUser = v.Users.Get((long)v.UserId, ProfileFields.All, null); // v.Account.GetProfileInfo();
            if (vkUser == null)
                return null;

            userToReturn.FirstName = vkUser.FirstName;
            userToReturn.LastName = vkUser.LastName;
            userToReturn.City = vkUser.City != null ? vkUser.City.Title : "";
            userToReturn.VkUserId = vkUser.Id;
            userToReturn.Country = vkUser.City != null ? vkUser.Country.Title : "";
            return userToReturn;
        }

        public static string GetUserPhoto200()
        {
            try
            {
                VkNet.Model.User u = v.Users.Get(v.UserId.Value, VkNet.Enums.Filters.ProfileFields.All);
                return u.PhotoPreviews.Photo200;
            }
            catch (Exception)
            {
                throw new VkNet.Exception.VkApiMethodInvokeException();
            }
        }
    }
}
