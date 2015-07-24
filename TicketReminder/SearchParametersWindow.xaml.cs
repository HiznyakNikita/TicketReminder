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
using TicketReminder.DataClasses;

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
            try
            {
                if (lstBoxCarType.SelectedItems != null)
                {
                    List<CarType> carTypes = new List<CarType>();
                    foreach (var item in lstBoxCarType.SelectedItems)
                    {
                        switch (item.ToString().Substring(item.ToString().IndexOf(":") + 2, item.ToString().Length - (item.ToString().IndexOf(":")+2)))
                        {
                            case "Люкс":
                                carTypes.Add(CarType.Luxe);
                                break;
                            case "Купэ фирменное":
                                carTypes.Add(CarType.CoupeFirm);
                                break;
                            case "Купэ":
                                carTypes.Add(CarType.Coupe);
                                break;
                            case "Плацкарт фирменный":
                                carTypes.Add(CarType.ReservedSeatFirm);
                                break;
                            case "Плацкарт":
                                carTypes.Add(CarType.ReservedSeat);
                                break;
                            case "Сидя":
                                carTypes.Add(CarType.Seat);
                                break;
                        }
                    }
                    SearchSettings.Instance.CarTypes = carTypes;
                }
                if (cmbBoxCarTypeReserve.SelectedItem != null)
                {
                    switch (cmbBoxCarTypeReserve.Text)
                    {
                        case "Люкс":
                            SearchSettings.Instance.ReservePriority = CarType.Luxe;
                            break;
                        case "Купэ фирменное":
                            SearchSettings.Instance.ReservePriority = CarType.CoupeFirm;
                            break;
                        case "Купэ":
                            SearchSettings.Instance.ReservePriority = CarType.Coupe;
                            break;
                        case "Плацкарт фирменный":
                            SearchSettings.Instance.ReservePriority = CarType.ReservedSeatFirm;
                            break;
                        case "Плацкарт":
                            SearchSettings.Instance.ReservePriority = CarType.ReservedSeat;
                            break;
                        case "Сидя":
                            SearchSettings.Instance.ReservePriority = CarType.Seat;
                            break;
                    }
                }
                if (checkBoxReserve.IsChecked == true)
                    SearchSettings.Instance.EnableReserve = true;
                else
                    SearchSettings.Instance.EnableReserve = false;
                if (lstBoxPlaceType.SelectedItems != null)
                {
                    List<PlaceType> placeTypes = new List<PlaceType>();
                    foreach (var item in lstBoxPlaceType.SelectedItems)
                    {
                        switch (item.ToString().Substring(item.ToString().IndexOf(":") + 2, item.ToString().Length - (item.ToString().IndexOf(":") + 2)))
                        {
                            case "Нижнее купе":
                                placeTypes.Add(PlaceType.LowCoupe);
                                break;
                            case "Верхнее купе":
                                placeTypes.Add(PlaceType.HighCoupe);
                                break;
                            case "Нижнее боковое":
                                placeTypes.Add(PlaceType.LowSide);
                                break;
                            case "Верхнее боковое":
                                placeTypes.Add(PlaceType.HighSide);
                                break;
                        }
                    }
                    SearchSettings.Instance.PlaceTypes = placeTypes;
                }
                if (tbCheckPeriod.Text != "")
                    SearchSettings.Instance.CheckPeriod = Convert.ToInt32(tbCheckPeriod.Text);
                if (lstBoxNotifyType.SelectedItems.Count > 0)
                {
                    if (lstBoxNotifyType.SelectedItems.Count == 1)
                    {
                        if (lstBoxNotifyType.SelectedIndex == 0)
                            SearchSettings.Instance.Notifiers.Add(new EmailHelper(Properties.Settings.Default.UserEmailPassword, Properties.Settings.Default.UserEmail, "Білети Укрзалізниця"));
                        else if (lstBoxNotifyType.SelectedIndex == 1)
                            SearchSettings.Instance.Notifiers.Add(new VkontakteHelper());
                    }
                    else
                    {
                        SearchSettings.Instance.Notifiers.Add(new EmailHelper(Properties.Settings.Default.UserEmailPassword, Properties.Settings.Default.UserDprcGovUaEmail, "Білети Укрзалізниця"));
                        SearchSettings.Instance.Notifiers.Add(new VkontakteHelper());
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Error in data!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}