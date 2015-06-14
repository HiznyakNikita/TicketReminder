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
    public partial class SearchParametersWindow : Window
    {
        public static SearchSettings searchSettings;
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
            searchSettings = new SearchSettings();
            if (cmbBoxCarType.SelectedItem != null)
                searchSettings.CarType = cmbBoxCarType.Text;
            if (cmbBoxCarTypeReserve.SelectedItem != null)
                searchSettings.ReservePriority = cmbBoxCarTypeReserve.Text;
            if (checkBoxReserve.IsChecked == true)
                searchSettings.EnableReserve = true;
            else
                searchSettings.EnableReserve = false;
            if (cmbBoxPlaceType.SelectedItem != null)
                searchSettings.PlaceType = cmbBoxPlaceType.Text;
            if (tbCheckPeriod.Text != "")
                searchSettings.CheckPeriod = Convert.ToInt32(tbCheckPeriod.Text);
        }
    }
}
