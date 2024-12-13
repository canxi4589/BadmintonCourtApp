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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BadmintonCourtApp.AdminViews.Popup;

namespace BadmintonCourtApp.AdminViews.Pages
{
    /// <summary>
    /// Interaction logic for AddCourtPage.xaml
    /// </summary>
    public partial class AddCourtPage : Page
    {
        private CourtRepository courtRepository;
        private LocationRepository locationRepository;
        private BadmintonCourt badmintonCourt;
        public AddCourtPage(CourtRepository courtRepository)
        {
            this.courtRepository = courtRepository;
            this.locationRepository = locationRepository;
            InitializeComponent();
            LoadLocationName();
            clear();
        }
        private void clear()
        {
            NewCapacity.Text = string.Empty;
            NewCourtName.Text = string.Empty;
            NewDescription.Text = string.Empty;
            NewPrice.Text = string.Empty;
        }

        private void LoadLocationName()
        {
            var locations = locationRepository.getAll()
                .Select(c => new LocationViewModel
                {
                    LocationID = c.LocationId,
                    Name = c.Name
                }).ToList();

            LocationComboBox.ItemsSource = locations;
            LocationComboBox.DisplayMemberPath = "Name";
            LocationComboBox.SelectedValuePath = "LocationID";
        }
        public class LocationViewModel
        {
            public int LocationID { get; set; }
            public string Name { get; set; }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LocationComboBox.SelectedValue is int locationID)
            {
                LoadLocation(locationID);
            }
        }
        private void LoadLocation(int locationID)
        {
            var locations = locationRepository.GetLocationsByID(locationID)
                .Select(c => new LocationViewModel
                {
                    LocationID = c.LocationId,
                    Name = c.Name
                }).ToList();

            LocationComboBox.ItemsSource = locations;
            LocationComboBox.DisplayMemberPath = "Name";
            LocationComboBox.SelectedValuePath = "LocationID";
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NewCapacity.Text, out int newCapacity) && int.TryParse(NewPrice.Text, out int newPrice))
            {
                if (LocationComboBox.SelectedValue is int selectedLocationID)
                {
                    var newCourt = new BadmintonCourt
                    {
                        CourtName = NewCourtName.Text,
                        Description = NewDescription.Text,
                        Capacity = int.Parse(NewCapacity.Text),
                        Price = int.Parse(NewPrice.Text),
                        LocationId = selectedLocationID
                    };

                    courtRepository.Add(newCourt);
                    MessageBox.Show("Court added successfully!", "Add Success", MessageBoxButton.OK);
                    clear();
                }
                else
                {
                    MessageBox.Show("Please select a location.", "Input Error", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Invalid input for capacity or price!", "Input Error", MessageBoxButton.OK);
            }
        }
    }
}
