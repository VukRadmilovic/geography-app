﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Zanimljiva_Geografija_Tim14
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Collection<Country> _countries;
        private readonly CountryService _service;
        private Country chosenCountry;

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
                chosenCountry = _countries[0]; //Ovo ce se menjati kada se bude implementirala logika za biranje drzave
                DataContext = chosenCountry;
                compareButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            var window = new ComparisonWindow(new List<Country>(_countries.OrderBy(x => random.Next()).Take(3)));
            Hide();
            window.ShowDialog();
            Show();
        }

        private void button_map_Click(object sender, RoutedEventArgs e)
        {
            if(map_window.Visibility == Visibility.Visible)
            {
                map_window.Visibility = Visibility.Hidden;
                button_map.Content = "See map";
                return;
            }
            button_map.Content = "See info";
            map_window.Visibility = Visibility.Visible;
        }
    }
}
