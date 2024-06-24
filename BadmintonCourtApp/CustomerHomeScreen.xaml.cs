using Repository.Models;
using Repository.repository;
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

namespace BadmintonCourtApp
{
    /// <summary>
    /// Interaction logic for CustomerHomeScreen.xaml
    /// </summary>
    public partial class CustomerHomeScreen : Window
    {
        private readonly CourtRepository _courtRepository;
        private List<BadmintonCourt> allCourts;

        public CustomerHomeScreen(CourtRepository r)
        {
            InitializeComponent();
            _courtRepository = r;
            loadData();

        }
        private void loadData()
        {
            var courts = _courtRepository.GetAll();
            CourtDataGrid.ItemsSource = courts;
            allCourts = courts.ToList();
        }

        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
                
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text.ToLower();
            var filteredCourts = allCourts.Where(c => c.CourtName.ToLower().Contains(searchText)).ToList();
            CourtDataGrid.ItemsSource = filteredCourts;
        }

        private void CourtDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CourtDataGrid.SelectedItem is BadmintonCourt selectedCourt)
            {
                var courtDetailWindow = new CourtDetailWindow(selectedCourt);
                courtDetailWindow.ShowDialog();
            }
        }
    }
}
