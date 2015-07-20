using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace TicketReminder.HelperClasses
{
    public class AppWindow
    {

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        public bool IsInternet { get { return CheckInternetConnection(); } }

        public AppWindow() {  }

        /// <summary>
        /// Method check internet connection
        /// </summary>
        /// <returns>True: Have internet connection</returns>
        /// <returns>False: Not have internet connection</returns>
        public bool CheckInternetConnection()
        {
            int Desc;
            if (InternetGetConnectedState(out Desc, 0) == true)
                return true;
            else
                return false;
        }

        private static bool IsActive(Window win)
        {
            if (win == null) return false;
            return GetForegroundWindow() == new WindowInteropHelper(win).Handle;
        }

        /// <summary>
        /// Chech is app active
        /// </summary>
        /// <returns></returns>
        public static bool IsAppActive()
        {
            foreach (var win in Application.Current.Windows.OfType<Window>())
                if (IsActive(win))
                    return true;
            return false;
            
        }

        /// <summary>
        /// Chech is current window open
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T IsWindowOpen<T>(string name = null) where T : Window
        {
            var windows = Application.Current.Windows.OfType<T>();
            return string.IsNullOrEmpty(name) ? windows.FirstOrDefault() : windows.FirstOrDefault(w => w.Name.Equals(name));
        }
    }
}
