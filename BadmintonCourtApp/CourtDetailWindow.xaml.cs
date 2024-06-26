using Repository.Models;
using Repository.repository;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
// SelectedDateChanged="DatePicker_SelectedDateChanged"
namespace BadmintonCourtApp
{
    public partial class CourtDetailWindow : Window
    {
        private readonly BadmintonCourt _court;
        private readonly ItemRepository _itemRepository;
        private readonly CourtRepository _courtRepository;

        public CourtDetailWindow(BadmintonCourt court, ItemRepository itemRepository, CourtRepository courtRepository)
        {
            _itemRepository = itemRepository;
            _courtRepository = courtRepository;
            InitializeComponent();
            _court = court;
            LoadCourtDetails();
            LoadItemTypes();
        }

        private void LoadCourtDetails()
        {
            CourtNameTextBlock.Text = _court.CourtName;
            LocationTextBlock.Text = _court.Location?.Name ?? "N/A";
            CapacityTextBlock.Text = _court.Capacity.ToString();
            PriceTextBlock.Text = _court.Price.ToString();
            DescriptionTextBlock.Text = _court.Description;

            
        }

        private void LoadItemTypes()
        {
            var itemTypes = _itemRepository.GetItemsByType()
                .Select(c => new TypeViewModel
                {
                    Id = c.ItemTypeId,
                    Name = c.Type
                }).ToList();

            ItemTypeComboBox.ItemsSource = itemTypes;
            ItemTypeComboBox.DisplayMemberPath = "Name";
            ItemTypeComboBox.SelectedValuePath = "Id";
        }

        private void ItemTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemTypeComboBox.SelectedValue is int selectedTypeId)
            {
                LoadItems(selectedTypeId);
            }
        }

        private void LoadItems(int itemTypeId)
        {
            var items = _itemRepository.GetItems(itemTypeId)
                .Select(c => new ItemsViewModel
                {
                    ItemId = c.ItemId,
                    Name = c.Name
                }).ToList();

            ItemsComboBox.ItemsSource = items;
            ItemsComboBox.DisplayMemberPath = "Name";
            ItemsComboBox.SelectedValuePath = "ItemId";
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePicker.SelectedDate.HasValue)
            {
                LoadTimeslots(DatePicker.SelectedDate.Value);
            }
        }

        private void LoadTimeslots(DateTime date)
        {
            var timeSlots = _courtRepository.getAllV(_court.CourtId)
                .Select(c => new TimeSlotViewModel
                {
                    TimeSlotID = (int)c.TimeSlotId,
                    TimeSlot = c.TimeSlot.Name,
                    IsBooked = _courtRepository.IsTimeSlotBooked(_court.CourtId, (int)c.TimeSlotId, date),
                    IsBookedText = _courtRepository.IsTimeSlotBooked(_court.CourtId, (int)c.TimeSlotId, date) ? " (Booked)" : ""
                }).ToList();

            TimeslotListBox.ItemsSource = timeSlots;
        }
        private void BookCourtButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTimeslot = TimeslotListBox.SelectedItem as TimeSlotViewModel;
            var selectedItem = ItemsComboBox.SelectedItem as ItemsViewModel;

            if (selectedTimeslot == null || selectedItem == null)
            {
                MessageBox.Show("Please select a timeslot and a service item.");
                return;
            }

            // Implement booking logic
            MessageBox.Show("Court booked successfully!");
            this.Close();
        }
    }

    public class TimeSlotViewModel
    {
        public int TimeSlotID { get; set; }
        public string TimeSlot { get; set; }
        public bool IsBooked { get; set; }
        public string IsBookedText { get; set; }
    }

    public class ItemsViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
    }

    public class TypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
