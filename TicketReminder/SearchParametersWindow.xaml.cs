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

namespace TicketReminder
{
    /// <summary>
    /// Interaction logic for SearchParametersWindow.xaml
    /// </summary>
    public partial class SearchParametersWindow : MetroWindow
    {
        public SearchParametersWindow()
        {
            InitializeComponent();
        }

        private void cmbBoxCarType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbBoxCarType.Text)
            {
                case "Люкс":
                    cmbBoxPlaceType.IsEnabled = false;
                    lblPlaceType.IsEnabled = false;
                    break;
                case "Купэ фирменное":
                    cmbBoxPlaceType.Items.Add("Нижнее");
                    cmbBoxPlaceType.Items.Add("Верхнее");
                    break;
                case "Купэ":
                    cmbBoxPlaceType.Items.Add("Нижнее");
                    cmbBoxPlaceType.Items.Add("Верхнее");
                    break;
                case "Плацкарт фирменный":
                    cmbBoxPlaceType.Items.Add("Нижнее");
                    cmbBoxPlaceType.Items.Add("Верхнее");
                    cmbBoxPlaceType.Items.Add("Нижнее боковое");
                    cmbBoxPlaceType.Items.Add("Верхнее боковое");
                    break;
                case "Плацкарт":
                    cmbBoxPlaceType.Items.Add("Нижнее");
                    cmbBoxPlaceType.Items.Add("Верхнее");
                    cmbBoxPlaceType.Items.Add("Нижнее боковое");
                    cmbBoxPlaceType.Items.Add("Верхнее боковое");
                    break;
                case "Сидя":
                    cmbBoxPlaceType.IsEnabled = false;
                    lblPlaceType.IsEnabled = false;
                    break;
            }
        }

        private void checkBoxReserve_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbBoxCarTypeReserve.IsEnabled = false;
            lblCarTypeReserve.IsEnabled = false;
        }

        private void checkBoxReserve_Checked(object sender, RoutedEventArgs e)
        {
            cmbBoxCarTypeReserve.IsEnabled = true;
            lblCarTypeReserve.IsEnabled = true;
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (cmbBoxCarType.SelectedItem != null)
                SearchSettings.Instance.CarType = cmbBoxCarType.Text;
            if (cmbBoxCarTypeReserve.SelectedItem != null)
                SearchSettings.Instance.ReservePriority = cmbBoxCarTypeReserve.Text;
            if (checkBoxReserve.IsChecked == true)
                SearchSettings.Instance.EnableReserve = true;
            else
                SearchSettings.Instance.EnableReserve = false;
            if (cmbBoxPlaceType.SelectedItem != null)
                SearchSettings.Instance.PlaceType = cmbBoxPlaceType.Text;
            if (tbCheckPeriod.Text != "")
                SearchSettings.Instance.CheckPeriod = Convert.ToInt32(tbCheckPeriod.Text);
            if(cmbBoxNotifyBy.SelectedItem!= null)
            {
                if (cmbBoxNotifyBy.SelectedIndex == 0)
                    SearchSettings.Instance.Notifier = new EmailHelper(Properties.Settings.Default.UserEmailPassword, Properties.Settings.Default.UserEmail, "Білети Укрзалізниця");
                else if (cmbBoxNotifyBy.SelectedIndex == 1)
                    SearchSettings.Instance.Notifier = new VkontakteHelper();
            }
        }
    }
}
