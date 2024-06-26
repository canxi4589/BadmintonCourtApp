using Repository.Models;
using Repository.repository;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BadmintonCourtApp
{
    public partial class CourtDetailWindow : Window
    {
        private readonly BadmintonCourt _court;
        private readonly ItemRepository _itemRepository;
        private readonly CourtRepository _courtRepository;
        private readonly int userid;

        public CourtDetailWindow(BadmintonCourt court, ItemRepository itemRepository, CourtRepository courtRepository,int serid)
        {
            _itemRepository = itemRepository;
            _courtRepository = courtRepository;
            InitializeComponent();
            _court = court;
            LoadCourtDetails();
            LoadItemTypes();
            userid = serid;
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
                    Name = c.Name,
                    Price = (int)c.Price // Assuming Item has a Price property
                }).ToList();

            ItemsListBox.ItemsSource = items;
            ItemsListBox.DisplayMemberPath = "Name";
            ItemsListBox.SelectedValuePath = "ItemId";
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
                    value = _courtRepository.GetTimeSlotById((int)c.TimeSlotId).Value + (_courtRepository.IsTimeSlotBooked(_court.CourtId, (int)c.TimeSlotId, date) ? " (Booked)" : ""),
                    TimeSlot = c.TimeSlot.Name,
                    IsBooked = _courtRepository.IsTimeSlotBooked(_court.CourtId, (int)c.TimeSlotId, date),
                    IsBookedText = _courtRepository.IsTimeSlotBooked(_court.CourtId, (int)c.TimeSlotId, date) ? " (Booked)" : ""
                }).ToList();

            TimeslotListBox.ItemsSource = timeSlots;
            TimeslotListBox.DisplayMemberPath = "value";
        }

        private void BookCourtButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTimeslot = TimeslotListBox.SelectedItem as TimeSlotViewModel;
            var selectedItems = ItemsListBox.SelectedItems.Cast<ItemsViewModel>().ToList();

            if (selectedTimeslot == null || !selectedItems.Any())
            {
                MessageBox.Show("Please select a timeslot and at least one service item.");
                return;
            }



            // Create a new booking
            var newBooking = new Booking
            {
                UserId = userid,
                CourtId = _court.CourtId,
                NumberOfGuest = 0, // Update this as per your requirements
                SpecialNote = "",  // Update this as per your requirements
                TotalPrice = CalculateTotalPrice(selectedTimeslot, selectedItems)
            };

            // Add selected timeslot to the booking
            newBooking.BookingSlots.Add(new BookingSlot
            {
                Vstid = selectedTimeslot.TimeSlotID
            });

            // Add selected items to the booking
            foreach (var item in selectedItems)
            {
                newBooking.BookingItems.Add(new BookingItem
                {
                    ItemId = item.ItemId,
                    UnitQuantity = 1, // Update this as per your requirements
                    SumPrice = item.Price // Assuming each item has a Price property
                });
            }

            // Save the booking to the database
            _courtRepository.AddBooking(newBooking);

            MessageBox.Show("Court booked successfully!");
        }

      

        private int CalculateTotalPrice(TimeSlotViewModel selectedTimeslot, List<ItemsViewModel> selectedItems)
        {
            int totalPrice = _court.Price ?? 0; // Add court price if applicable

            foreach (var item in selectedItems)
            {
                totalPrice += item.Price; // Assuming each item has a Price property
            }

            return totalPrice;
        }
    }

    public class TimeSlotViewModel
    {
        public int TimeSlotID { get; set; }
        public string value { get; set; }
        public string TimeSlot { get; set; }
        public bool IsBooked { get; set; }
        public string IsBookedText { get; set; }
    }

    public class ItemsViewModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; } // Assuming Item has a Price property
    }

    public class TypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
