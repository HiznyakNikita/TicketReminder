using MahApps.Metro.Controls;
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
using TicketReminder.HelperClasses;

namespace TicketReminder
{
    /// <summary>
    /// Логика взаимодействия для SettingsAppWindow.xaml
    /// </summary>
    public partial class SettingsAppWindow : MetroWindow
    {
        Sound sound = new Sound();
        public SettingsAppWindow()
        {
            InitializeComponent();
            Loaded += delegate
            {
                Load();
                LoadSounds();
            };
        }

        private void Load()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;

            //Notify.IsNotify = true;
            Sound.IsPlay = true;
            this.IsWindowDraggable = false;
        }

        private void LoadSounds()
        {
            //Also customer can load own sounds in notification
            foreach (var item in sound.SearchSounds())
                comboSound.Items.Add(new Sound() { Name = item.Name, PathToFile = item.PathToFile });    
        }
       
        private void comboSound_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic item = comboSound.SelectedItem;
            Sound.Location = item.PathToFile;
        }

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (comboSound.SelectedItem != null)
            {
                // Check is play music
                if (toggleButton.IsChecked == false)
                    Sound.IsPlay = false;

                // Check is show notification
                //if (toggleNotification.IsChecked == false)
                //    Notify.IsNotify = false;

                dynamic item = comboSound.SelectedItem;
                Sound.Location = (string)item.PathToFile;
            }
        }
    }
}
