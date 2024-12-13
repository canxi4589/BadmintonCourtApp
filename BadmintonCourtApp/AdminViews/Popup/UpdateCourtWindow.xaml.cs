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

namespace BadmintonCourtApp.AdminViews.Popup
{
    /// <summary>
    /// Interaction logic for UpdateCourtWindow.xaml
    /// </summary>
    public partial class UpdateCourtWindow : Window
    {
        private LocationRepository locationRepository;
        private BadmintonCourt badmintonCourt;
        private CourtRepository courtRepository;
        private DBContext DBContext= new DBContext();
        public UpdateCourtWindow(BadmintonCourt badmintonCourt, CourtRepository courtRepository)
        {
            InitializeComponent();
            this.badmintonCourt = badmintonCourt;
            this.courtRepository = courtRepository;
            locationRepository = new LocationRepository(DBContext);

            NewCapacity.Text = OldCapacity.Text = badmintonCourt.Capacity.ToString();
            NewCourtName.Text = OldCourtName.Text = badmintonCourt.CourtName.ToString();
            NewDescription.Text = OldDescription.Text = badmintonCourt?.Description.ToString();
            OldLocation.Text = badmintonCourt.Location.Name.ToString();
            NewPrice.Text = OldPrice.Text = badmintonCourt.Price.ToString();
            CourtID1.Text = CourtID1_Copy.Text = badmintonCourt.CourtId.ToString();
            LoadLocationName();
        }
        private void LoadLocationName()
        {
            var locations = locationRepository?.getAll()
        ?.Where(c => c != null) // Ensure no null entries in the collection
        ?.Select(c => new LocationViewModel
     {
         LocationID = c.LocationId,
         Name = c.Name ?? string.Empty // Handle null Name property
     }).ToList() ?? new List<LocationViewModel>();

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

            LocationComboBox.DisplayMemberPath = "Name";
            LocationComboBox.SelectedValuePath = "LocationID";
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NewCapacity.Text, out int newCapacity) && int.TryParse(NewPrice.Text, out int newPrice))
            {
                badmintonCourt.CourtName = NewCourtName.Text;
                badmintonCourt.Description = NewDescription.Text;
                badmintonCourt.Capacity = int.Parse(NewCapacity.Text);
                badmintonCourt.Price = int.Parse(NewPrice.Text);

                if (LocationComboBox.SelectedValue is int selectedLocationID)
                {
                    badmintonCourt.LocationId = selectedLocationID;
                }

                courtRepository.Update(badmintonCourt);
                MessageBox.Show("Court updated successfully!", "Update Success", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Invalid input for capacity or price!", "Input Error", MessageBoxButton.OK);
            }
        }
    }
}
