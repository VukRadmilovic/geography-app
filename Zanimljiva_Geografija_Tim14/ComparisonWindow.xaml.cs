using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace Zanimljiva_Geografija_Tim14
{
    /// <summary>
    /// Interaction logic for ComparisonWindow.xaml
    /// </summary>
    public partial class ComparisonWindow : Window
    {
        private List<Country> _countries;

        public ComparisonWindow()
        {
            InitializeComponent();
        }

        public ComparisonWindow(List<Country> countries)
        {
            InitializeComponent();
            _countries = countries;

            PopulateGrid(countries, countries.Count);
        }

        private void PopulateGrid(List<Country> countries, int numColumns)
        {
            countryGrid.Children.Clear();
            countryGrid.RowDefinitions.Clear();
            countryGrid.ColumnDefinitions.Clear();

            countryGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int i = 0; i < numColumns; i++)
            {
                countryGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            }

            int numRows = 12;
            for (int i = 0; i < numRows; i++)
            {
                countryGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            int row = 0;
            int col = 0;

            TextBlock nameLabel = new TextBlock() { Text = "Name:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center};
            countryGrid.Children.Add(nameLabel);
            Grid.SetRow(nameLabel, row++);
            Grid.SetColumn(nameLabel, col);

            TextBlock flagLabel = new TextBlock() { Text = "Flag:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(flagLabel);
            Grid.SetRow(flagLabel, row++);
            Grid.SetColumn(flagLabel, col);

            TextBlock coatOfArmsLabel = new TextBlock() { Text = "Coat of arms:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(coatOfArmsLabel);
            Grid.SetRow(coatOfArmsLabel, row++);
            Grid.SetColumn(coatOfArmsLabel, col);

            TextBlock capitalsLabel = new TextBlock() { Text = "Capitals:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(capitalsLabel);
            Grid.SetRow(capitalsLabel, row++);
            Grid.SetColumn(capitalsLabel, col);

            TextBlock currenciesLabel = new TextBlock() { Text = "Currencies:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(currenciesLabel);
            Grid.SetRow(currenciesLabel, row++);
            Grid.SetColumn(currenciesLabel, col);

            TextBlock regionLabel = new TextBlock() { Text = "Region:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(regionLabel);
            Grid.SetRow(regionLabel, row++);
            Grid.SetColumn(regionLabel, col);

            TextBlock subRegionLabel = new TextBlock() { Text = "Sub-region:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(subRegionLabel);
            Grid.SetRow(subRegionLabel, row++);
            Grid.SetColumn(subRegionLabel, col);

            TextBlock languagesLabel = new TextBlock() { Text = "Languages:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(languagesLabel);
            Grid.SetRow(languagesLabel, row++);
            Grid.SetColumn(languagesLabel, col);

            TextBlock latLabel = new TextBlock() { Text = "Latitude:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(latLabel);
            Grid.SetRow(latLabel, row++);
            Grid.SetColumn(latLabel, col);

            TextBlock lngLabel = new TextBlock() { Text = "Longitude:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(lngLabel);
            Grid.SetRow(lngLabel, row++);
            Grid.SetColumn(lngLabel, col);

            TextBlock continentsLabel = new TextBlock() { Text = "Continents:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(continentsLabel);
            Grid.SetRow(continentsLabel, row++);
            Grid.SetColumn(continentsLabel, col);

            TextBlock populationLabel = new TextBlock() { Text = "Population:", FontWeight = FontWeights.Bold, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
            countryGrid.Children.Add(populationLabel);
            Grid.SetRow(populationLabel, row++);
            Grid.SetColumn(populationLabel, col);

            foreach (Country c in countries)
            {
                row = 0;
                col++;

                TextBlock name = new TextBlock() { Text = c.OfficialName, TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                countryGrid.Children.Add(name);
                Grid.SetRow(name, row++);
                Grid.SetColumn(name, col);

                WrapPanel flagWrap = new WrapPanel() { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 0, 0, 2)};
                var flagImage = new Image();
                BitmapImage flagBitmap = new BitmapImage();
                flagBitmap.BeginInit();
                flagBitmap.UriSource = new Uri(c.Flag["png"], UriKind.Absolute);
                flagBitmap.EndInit();
                flagImage.Source = flagBitmap;
                flagWrap.Children.Add(flagImage);

                countryGrid.Children.Add(flagWrap);
                Grid.SetRow(flagWrap, row++);
                Grid.SetColumn(flagWrap, col);

                WrapPanel coatOfArmsWrap = new WrapPanel() { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(0, 2, 0, 0)};
                var coatOfArmsImage = new Image();
                BitmapImage coatOfArmsBitmap = new BitmapImage();
                coatOfArmsBitmap.BeginInit();
                coatOfArmsBitmap.UriSource = new Uri(c.CoatOfArms["png"], UriKind.Absolute);
                coatOfArmsBitmap.EndInit();
                coatOfArmsImage.Source = coatOfArmsBitmap;
                coatOfArmsWrap.Children.Add(coatOfArmsImage);

                countryGrid.Children.Add(coatOfArmsWrap);
                Grid.SetRow(coatOfArmsWrap, row++);
                Grid.SetColumn(coatOfArmsWrap, col);

                TextBlock capitals = new TextBlock() { Text = string.Join(", ", c.Capitals), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                countryGrid.Children.Add(capitals);
                Grid.SetRow(capitals, row++);
                Grid.SetColumn(capitals, col);

                TextBlock currencies = new TextBlock() { Text = string.Join(", ", c.GetAllCurrencies()), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                countryGrid.Children.Add(currencies);
                Grid.SetRow(currencies, row++);
                Grid.SetColumn(currencies, col);

                TextBlock region = new TextBlock() { Text = c.Region, TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                countryGrid.Children.Add(region);
                Grid.SetRow(region, row++);
                Grid.SetColumn(region, col);

                TextBlock subRegion = new TextBlock() { Text = c.SubRegion, TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                countryGrid.Children.Add(subRegion);
                Grid.SetRow(subRegion, row++);
                Grid.SetColumn(subRegion, col);

                TextBlock languages = new TextBlock() { Text = string.Join(", ", c.Languages), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                countryGrid.Children.Add(languages);
                Grid.SetRow(languages, row++);
                Grid.SetColumn(languages, col);

                TextBlock lat = new TextBlock() { Text = $"{c.LatLng[0]:F}", TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                countryGrid.Children.Add(lat);
                Grid.SetRow(lat, row++);
                Grid.SetColumn(lat, col);

                TextBlock lng = new TextBlock() { Text = $"{c.LatLng[1]:F}", TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                countryGrid.Children.Add(lng);
                Grid.SetRow(lng, row++);
                Grid.SetColumn(lng, col);

                TextBlock continents = new TextBlock() { Text = string.Join(", ", c.Continents), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                countryGrid.Children.Add(continents);
                Grid.SetRow(continents, row++);
                Grid.SetColumn(continents, col);

                TextBlock population = new TextBlock() { Text = $"{c.Population:N}", TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                countryGrid.Children.Add(population);
                Grid.SetRow(population, row++);
                Grid.SetColumn(population, col);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
