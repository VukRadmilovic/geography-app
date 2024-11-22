using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace geography_app
{
    /// <summary>
    /// Interaction logic for ComparisonWindow.xaml
    /// </summary>
    public partial class ComparisonWindow : Window
    {
        private readonly List<TextBlock> _scalableTextBlocks = new List<TextBlock>();
        private readonly int _countryCount;

        public ComparisonWindow()
        {
            InitializeComponent();
        }

        public ComparisonWindow(List<Country> countries)
        {
            InitializeComponent();
            FontSize = 18;

            _countryCount = countries.Count;
            PopulateGrid(countries);

            SizeChanged += ComparisonWindow_SizeChanged;
            ScaleTextBoxes();
        }

        private void ComparisonWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScaleTextBoxes();
        }

        private void ScaleTextBoxes()
        {
            foreach (TextBlock textBlock in _scalableTextBlocks)
            {
                textBlock.MaxWidth = ActualWidth / _countryCount;
            }
        }

        private Viewbox CreateLabel(string text)
        {
            Viewbox viewbox = new Viewbox() { Stretch = Stretch.Uniform, StretchDirection = StretchDirection.DownOnly, Margin = new Thickness(5, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Left };
            TextBlock textBlock = new TextBlock() { Text = text, FontWeight = FontWeights.DemiBold, FontSize = 20};
            viewbox.Child = textBlock;
            return viewbox;
        }

        private void PopulateGrid(List<Country> countries)
        {
            countryGrid.Children.Clear();
            countryGrid.RowDefinitions.Clear();
            countryGrid.ColumnDefinitions.Clear();

            countryGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            for (int i = 0; i < _countryCount; i++)
            {
                countryGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            }

            int numRows = 12;
            for (int i = 0; i < numRows; i++)
            {
                countryGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            Viewbox holder = new Viewbox() { Stretch = Stretch.Uniform };
            countryGrid.Children.Add(holder);
            Grid.SetRow(holder, 0);
            Grid.SetRowSpan(holder, numRows);
            Grid.SetColumn(holder, 0);
            Grid.SetColumnSpan(holder, _countryCount);

            string[] labels = { "Name", "Flag", "Coat of arms", "Capitals", "Languages", "Currencies", "Region", "Sub-region", "Continents", "Population", "Latitude", "Longitude" };
            int row = 0;
            int col = 0;

            foreach (string label in labels)
            {
                Viewbox textBlock = CreateLabel(label + ":");
                countryGrid.Children.Add(textBlock);
                Grid.SetRow(textBlock, row++);
                Grid.SetColumn(textBlock, col);
            }

            foreach (Country c in countries)
            {
                row = 0;
                col++;

                TextBlock name = new TextBlock() { Text = c.OfficialName, TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center, FontSize = 20, FontWeight = FontWeight.FromOpenTypeWeight(600)};
                Viewbox nameViewbox = new Viewbox() { Child = name, Stretch = Stretch.Uniform, StretchDirection = StretchDirection.DownOnly, Margin = new Thickness(0, 0, 5, 0) };
                countryGrid.Children.Add(nameViewbox);
                Grid.SetRow(nameViewbox, row++);
                Grid.SetColumn(nameViewbox, col);
                _scalableTextBlocks.Add(name);

                if (c.Flag["png"].Length > 0)
                {
                    WrapPanel flagWrap = new WrapPanel() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0, 0, 5, 2) };
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
                }
                else
                {
                    row++;
                }
                

                if (c.CoatOfArms["png"].Length > 0)
                {
                    WrapPanel coatOfArmsWrap = new WrapPanel() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(0, 2, 5, 0) };
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
                }
                else
                {
                    row++;
                }
                

                TextBlock capitals = new TextBlock() { Text = string.Join(", ", c.Capitals), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                Viewbox capitalsViewbox = new Viewbox() { Child = capitals, Stretch = Stretch.Uniform, StretchDirection = StretchDirection.DownOnly, Margin = new Thickness(0, 0, 5, 0) };
                countryGrid.Children.Add(capitalsViewbox);
                Grid.SetRow(capitalsViewbox, row++);
                Grid.SetColumn(capitalsViewbox, col);
                _scalableTextBlocks.Add(capitals);

                TextBlock languages = new TextBlock()
                {
                    Text = string.Join(", ", c.Languages),
                    TextWrapping = TextWrapping.Wrap,
                    MaxWidth = 300,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Viewbox languagesViewbox = new Viewbox()
                {
                    Child = languages,
                    Stretch = Stretch.Uniform,
                    StretchDirection = StretchDirection.DownOnly,
                    Margin = new Thickness(0, 0, 5, 0)
                };
                countryGrid.Children.Add(languagesViewbox);
                Grid.SetRow(languagesViewbox, row++);
                Grid.SetColumn(languagesViewbox, col);
                _scalableTextBlocks.Add(languages);

                TextBlock currencies = new TextBlock() { 
                    Text = string.Join(", ", c.GetAllCurrencies()), 
                    TextWrapping = TextWrapping.Wrap,
                    MaxWidth = 300,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Viewbox currenciesViewbox = new Viewbox()
                {
                    Child = currencies, 
                    Stretch = Stretch.Uniform, 
                    StretchDirection = StretchDirection.DownOnly,
                    Margin = new Thickness(0, 0, 5, 0)
                };
                countryGrid.Children.Add(currenciesViewbox);
                Grid.SetRow(currenciesViewbox, row++);
                Grid.SetColumn(currenciesViewbox, col);
                _scalableTextBlocks.Add(currencies);

                TextBlock region = new TextBlock() { Text = c.Region, TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                Viewbox regionViewbox = new Viewbox() { Child = region, Stretch = Stretch.Uniform, StretchDirection = StretchDirection.DownOnly, Margin = new Thickness(0, 0, 5, 0) };
                countryGrid.Children.Add(regionViewbox);
                Grid.SetRow(regionViewbox, row++);
                Grid.SetColumn(regionViewbox, col);
                _scalableTextBlocks.Add(region);

                TextBlock subRegion = new TextBlock() { Text = c.SubRegion, TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                Viewbox subRegionViewbox = new Viewbox() { Child = subRegion, Stretch = Stretch.Uniform, StretchDirection = StretchDirection.DownOnly, Margin = new Thickness(0, 0, 5, 0) };
                countryGrid.Children.Add(subRegionViewbox);
                Grid.SetRow(subRegionViewbox, row++);
                Grid.SetColumn(subRegionViewbox, col);
                _scalableTextBlocks.Add(subRegion);

                TextBlock continents = new TextBlock() { Text = string.Join(", ", c.Continents), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                Viewbox continentsViewBox = new Viewbox()
                { Child = continents, Stretch = Stretch.Uniform, StretchDirection = StretchDirection.DownOnly, Margin = new Thickness(0, 0, 5, 0) };
                countryGrid.Children.Add(continentsViewBox);
                Grid.SetRow(continentsViewBox, row++);
                Grid.SetColumn(continentsViewBox, col);
                _scalableTextBlocks.Add(continents);

                TextBlock population = new TextBlock() { Text = c.Population.ToString("n0", CultureInfo.InvariantCulture), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                Viewbox populationViewBox = new Viewbox()
                {
                    Child = population,
                    Stretch = Stretch.Uniform,
                    StretchDirection = StretchDirection.DownOnly,
                    Margin = new Thickness(0, 0, 5, 0)
                };
                countryGrid.Children.Add(populationViewBox);
                Grid.SetRow(populationViewBox, row++);
                Grid.SetColumn(populationViewBox, col);
                _scalableTextBlocks.Add(population);

                TextBlock lat = new TextBlock() { Text = c.LatLng[0].ToString("N2", CultureInfo.InvariantCulture), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                Viewbox latViewBox = new Viewbox()
                    { Child = lat, Stretch = Stretch.Uniform, StretchDirection = StretchDirection.DownOnly, Margin = new Thickness(0, 0, 5, 0) };
                countryGrid.Children.Add(latViewBox);
                Grid.SetRow(latViewBox, row++);
                Grid.SetColumn(latViewBox, col);
                _scalableTextBlocks.Add(lat);

                TextBlock lng = new TextBlock() { Text = c.LatLng[1].ToString("N2", CultureInfo.InvariantCulture), TextWrapping = TextWrapping.Wrap, VerticalAlignment = VerticalAlignment.Center };
                Viewbox lngViewBox = new Viewbox()
                    { Child = lng, Stretch = Stretch.Uniform, StretchDirection = StretchDirection.DownOnly, Margin = new Thickness(0, 0, 5, 0) };
                countryGrid.Children.Add(lngViewBox);
                Grid.SetRow(lngViewBox, row++);
                Grid.SetColumn(lngViewBox, col);
                _scalableTextBlocks.Add(lng);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
