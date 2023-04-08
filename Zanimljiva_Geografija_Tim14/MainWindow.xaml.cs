using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace Zanimljiva_Geografija_Tim14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String Value = "50000000";
        private Collection<Country> countries;
        private CountryService service;

        public MainWindow()
        {
            InitializeComponent();
            service = new CountryService();
            _ = LoadCountriesAsync();
        }

        public async Task LoadCountriesAsync()
        {
            countries = new ObservableCollection<Country>(await service.GetCountriesAsync());
        }
    }
}
