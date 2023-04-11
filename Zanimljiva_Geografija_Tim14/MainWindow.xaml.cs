using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Zanimljiva_Geografija_Tim14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _compareCount;
        private Collection<Country> _countries;
        private readonly CountryService _service;
        private Collection<Country> _searchedCountries;
        private readonly List<string> _compareCountriesNames = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            _service = new CountryService();
            _ = LoadCountriesAsync();
        }

        public async Task LoadCountriesAsync()
        {
            try
            {
                _countries = new ObservableCollection<Country>(await _service.GetCountriesAsync());
                _countries = new ObservableCollection<Country>(_countries.OrderBy(country => country.OfficialName));
                _searchedCountries = new ObservableCollection<Country>();
                foreach (Country country in _countries)
                {
                    searchComboBox.Items.Add(country.OfficialName);
                }
                DataContext = _countries[0];
                compareButton.IsEnabled = false;
                countryTable.ItemsSource = _countries;
                countryTable.SelectedIndex = 0;
                VirtualizingStackPanel.SetIsVirtualizing(countryTable, true);
                VirtualizingStackPanel.SetVirtualizationMode(countryTable, VirtualizationMode.Recycling);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Country> countries = new List<Country>();
            foreach(Country country in _countries)
            {
                if (_compareCountriesNames.Contains(country.OfficialName))
                {
                    countries.Add(country);
                }
            }
            var window = new ComparisonWindow(countries);
            Hide();
            window.ShowDialog();
            Show();
        }

        private void Button_map_Click(object sender, RoutedEventArgs e)
        {
            if (map_window.Visibility == Visibility.Visible)
            {
                map_window.Visibility = Visibility.Hidden;
                button_map.Content = "See map";
                return;
            }
            button_map.Content = "See info";
            map_window.Visibility = Visibility.Visible;
        }

        private void Search()
        {
            _searchedCountries.Clear();
            if (!searchComboBox.Text.Equals(""))
            {
                foreach (var country in _countries)
                {
                    if (country.OfficialName.IndexOf(searchComboBox.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        _searchedCountries.Add(country);
                    }
                }

                countryTable.ItemsSource = _searchedCountries;
            }
            else
            {
                countryTable.ItemsSource = _countries;
            }
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            var checkBox = (sender as DataGridCell)?.Content as CheckBox;
            var row = DataGridRow.GetRowContainingElement((sender as DataGridCell)?.Content as CheckBox);
            if (row == null) return;
            var checkedCountry = row.DataContext as Country;
            if (checkedCountry != null && checkedCountry.IsChecked)
                return;

            if (_compareCount >= 3)
            {
                if (checkBox != null) checkBox.IsChecked = false;
                return;
            }

            if (checkBox?.IsChecked == null || !checkBox.IsChecked.Value) return;
            _compareCount++;
            if (checkedCountry != null)
            {
                checkedCountry.IsChecked = true;
                _compareCountriesNames.Add(checkedCountry.OfficialName);
            }

            if (_compareCount > 1)
                compareButton.IsEnabled = true;
        }

        private void OnUncheck(object sender, RoutedEventArgs e)
        {
            var checkBox = (sender as DataGridCell)?.Content as CheckBox;
            var uncheckedCountry = DataGridRow.GetRowContainingElement(checkBox)?.DataContext as Country;

            if (uncheckedCountry == null || !uncheckedCountry.IsChecked)
                return;

            if (checkBox?.IsChecked == null || checkBox.IsChecked.Value) return;
            _compareCount--;
            uncheckedCountry.IsChecked = false;
            _compareCountriesNames.Remove(uncheckedCountry.OfficialName);
            if (_compareCount < 2)
                compareButton.IsEnabled = false;
        }

        private void Country_Table_Click(object sender, SelectedCellsChangedEventArgs e)
        {
            if (countryTable.SelectedCells.Count == 0) return;
            DataGridCellInfo cellInfo = countryTable.SelectedCells[0];
            var content = cellInfo.Column.GetCellContent(cellInfo.Item);

            if (content != null)
            {
                DataContext = (Country)content.DataContext;
            }

            BitmapImage flagBitmap = new BitmapImage();
            flagBitmap.BeginInit();
            flagBitmap.UriSource = new Uri(((Country)DataContext).Flag["png"], UriKind.Absolute);
            flagBitmap.EndInit();
            flagImage.Source = flagBitmap;

            BitmapImage coatOfArmsBitmap = new BitmapImage();
            coatOfArmsBitmap.BeginInit();
            coatOfArmsBitmap.UriSource = new Uri(((Country)DataContext).CoatOfArms["png"], UriKind.Absolute);
            coatOfArmsBitmap.EndInit();
            coatOfArmsImage.Source = coatOfArmsBitmap;
        }

        private void searchComboBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Search();
        }

        private void searchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void searchComboBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Search();
        }
    }
}
