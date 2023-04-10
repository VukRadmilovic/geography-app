using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Zanimljiva_Geografija_Tim14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _compareCount = 0;
        private Collection<Country> _countries;
        private readonly CountryService _service;
        private Country chosenCountry;
        private Collection<Country> _selectedCountry;
        private List<String> _compareCountriesNames = new List<string>();

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
                _selectedCountry = new ObservableCollection<Country>();
                foreach (Country country in _countries)
                {
                    this.searchComboBox.Items.Add(country.OfficialName);
                }
                chosenCountry = _countries[0]; //Ovo ce se menjati kada se bude implementirala logika za biranje drzave
                DataContext = chosenCountry;
                compareButton.IsEnabled = false;
                this.countryTable.ItemsSource = _countries;
                this.countryTable.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*Random random = new Random();
            var window = new ComparisonWindow(new List<Country>(_countries.OrderBy(x => random.Next()).Take(3)));*/
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

        private void button_map_Click(object sender, RoutedEventArgs e)
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

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            if(!this.searchComboBox.Text.Equals(""))
            {
                foreach(Country country in _countries)
                {
                    if(country.OfficialName.Equals(this.searchComboBox.Text))
                    {
                        _selectedCountry.Clear();
                        _selectedCountry.Add(country);
                        this.countryTable.ItemsSource = _selectedCountry;
                    }
                }
            }
            else
            {
                this.countryTable.ItemsSource = _countries;
            }
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (sender as DataGridCell).Content as CheckBox;
            if (_compareCount == 3)
            {
                checkBox.IsChecked = false;
            }
            else
            {
                var row = DataGridRow.GetRowContainingElement((sender as DataGridCell).Content as CheckBox);
                _compareCountriesNames.Add((row.DataContext as Country).OfficialName);
            }
            _compareCount++;
            if(_compareCount > 1)
                compareButton.IsEnabled = true;
        }

        private void OnUncheck(object sender, RoutedEventArgs e)
        {
            _compareCount--;
            _compareCountriesNames.Remove((DataGridRow.GetRowContainingElement((sender as DataGridCell).Content as CheckBox).DataContext as Country).OfficialName);
            if(_compareCount < 2)
                compareButton.IsEnabled = false;
        }

        private void Country_Table_Click(object sender, SelectedCellsChangedEventArgs e)
        {
            if(this.countryTable.SelectedCells.Count != 0)
            {
                DataGridCellInfo cellInfo = this.countryTable.SelectedCells[0];
                var content = cellInfo.Column.GetCellContent(cellInfo.Item);

                if (content != null)
                {
                    String selectedCountryName = ((Country)content.DataContext).OfficialName;
                    foreach (Country country in _countries)
                    {
                        if (selectedCountryName.Equals(country.OfficialName))
                        {
                            chosenCountry = country;
                            DataContext = chosenCountry;
                            break;
                        }
                    }
                }
            }
        }
    }
}
