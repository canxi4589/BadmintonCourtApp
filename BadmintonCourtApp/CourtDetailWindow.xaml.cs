using Repository.Models;
using Repository.repository;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;

namespace BadmintonCourtApp
{
    public partial class CourtDetailWindow : Window
    {
        private readonly BadmintonCourt _court;
        private readonly ItemRepository _itemRepository;
        private readonly CourtRepository _courtRepository;
        private readonly int _userId;

        public CourtDetailWindow(BadmintonCourt court, ItemRepository itemRepository, CourtRepository courtRepository, int userId)
        {
            _itemRepository = itemRepository;
            _courtRepository = courtRepository;
            _userId = userId;
            InitializeComponent();
            _court = court;
            LoadCourtDetails();
            LoadServices();
        }

        private void LoadCourtDetails()
        {
            CourtNameTextBlock.Text = _court.CourtName;
            LocationTextBlock.Text = _court.Location?.Name ?? "N/A";
            CapacityTextBlock.Text = _court.Capacity.ToString();
            PriceTextBlock.Text = _court.Price.ToString();
            DescriptionTextBlock.Text = _court.Description;
        }

        private void LoadServices()
        {
            var items = _itemRepository.GetAllItems()
                .Select(c => new ItemsViewModel
                {
                    ItemId = c.ItemId,
                    Name = c.Name,
                    Price = c.Price ?? 0
                }).ToList();

            ServicesListBox.ItemsSource = items;
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
            foreach (var timeSlot in timeSlots)
            {
                if (timeSlot.IsBooked)
                {
                    // Find the ListBoxItem corresponding to this timeslot
                    var listBoxItem = TimeslotListBox.ItemContainerGenerator.ContainerFromItem(timeSlot) as ListBoxItem;
                    if (listBoxItem != null)
                    {
                        listBoxItem.IsEnabled = false;
                        listBoxItem.Foreground = Brushes.Gray; // Optionally, change the appearance of booked items
                    }
                }
            }
        }

        private void BookCourtButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTimeslot = TimeslotListBox.SelectedItem as TimeSlotViewModel;
            var selectedItems = ServicesListBox.SelectedItems.Cast<ItemsViewModel>().ToList();

            if (selectedTimeslot == null || !selectedItems.Any())
            {
                MessageBox.Show("Please select a timeslot and at least one service item.");
                return;
            }

            var newBooking = new Booking
            {
                UserId = _userId,
                CourtId = _court.CourtId,
                NumberOfGuest = 0,
                SpecialNote = "",
                TotalPrice = CalculateTotalPrice(selectedTimeslot, selectedItems)
            };

            newBooking.BookingSlots.Add(new BookingSlot
            {
                Vstid = selectedTimeslot.TimeSlotID,
                BookDate =DatePicker.SelectedDate.ToDateOnly(),

            });

            foreach (var item in selectedItems)
            {
                newBooking.BookingItems.Add(new BookingItem
                {
                    ItemId = item.ItemId,
                    UnitQuantity = 1,
                    SumPrice = item.Price
                });
            }

            _courtRepository.AddBooking(newBooking);

            MessageBox.Show("Court booked successfully!");
            this.Close();
        }

        private int CalculateTotalPrice(TimeSlotViewModel selectedTimeslot, List<ItemsViewModel> selectedItems)
        {
            int totalPrice = _court.Price ?? 0;

            foreach (var item in selectedItems)
            {
                totalPrice += item.Price;
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
        public int Price { get; set; }
    }
    public static class DateTimeExtends
    {
        public static DateOnly ToDateOnly(this DateTime date)
        {
            return new DateOnly(date.Year, date.Month, date.Day);
        }

        public static DateOnly? ToDateOnly(this DateTime? date)
        {
            return date != null ? (DateOnly?)date.Value.ToDateOnly() : null;
        }
    }
}

