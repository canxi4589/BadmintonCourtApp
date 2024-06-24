using Repository.Models;
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
    /// Interaction logic for CourtDetailWindow.xaml
    /// </summary>
    public partial class CourtDetailWindow : Window
    {
        private readonly BadmintonCourt _court;

        public CourtDetailWindow(BadmintonCourt court)
        {
            InitializeComponent();
            _court = court;
            LoadCourtDetails();
        }

        private void LoadCourtDetails()
        {
            CourtNameTextBlock.Text = _court.CourtName;
            LocationTextBlock.Text = _court.Location?.Name ?? "N/A";
            CapacityTextBlock.Text = _court.Capacity.ToString();
            PriceTextBlock.Text = _court.Price.ToString();
            DescriptionTextBlock.Text = _court.Description;

            // Load available timeslots and services
            TimeslotListBox.ItemsSource = _court.VenueServiceTimes; // Assuming VenueServiceTimes contains the timeslots
            ServiceListBox.ItemsSource = "hehe"; // Assuming there's a collection of services available for the court
        }

        private void BookCourtButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle booking logic here
            // Retrieve selected timeslot and service
            var selectedTimeslot = TimeslotListBox.SelectedItem as VenueServiceTime;
            //var selectedService = ServiceListBox.SelectedItem as ;

            //if (selectedTimeslot == null || selectedService == null)
            if (selectedTimeslot == null)
            {
                MessageBox.Show("Please select a timeslot and a service.");
                return;
            }

            // Implement booking logic
            MessageBox.Show("Court booked successfully!");
            this.Close();
        }
    }
}
