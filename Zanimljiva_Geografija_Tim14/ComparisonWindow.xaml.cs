using System.Collections.Generic;
using System.Windows;

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

            foreach (var country in _countries)
            {
                MessageBox.Show(country.OfficialName);
            }
        }
    }
}
