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
using static System.Collections.Specialized.BitVector32;

namespace BadmintonCourtApp
{
    /// <summary>
    /// Interaction logic for BookingHistory.xaml
    /// </summary>
    public partial class BookingHistory : Window
    {
        private readonly BookingRepository bookingRepository;
        private List<Booking> allBookings;
        private readonly ItemRepository _itemRepository;
        private readonly CourtRepository _courtRepository;

        private readonly DBContext dbContext;
        public BookingHistory(BookingRepository r, CourtRepository c, ItemRepository i)
        {
            InitializeComponent();
            dbContext = new DBContext();
            _courtRepository = c;
            _itemRepository = i;
            bookingRepository = r;
            LoadBookingHistory();
        }
        public void LoadBookingHistory()
        {
            var bookings = bookingRepository.GetBookingsByUserId(Session.LoggedInUser.UserId);
            BookingHistoryDtg.ItemsSource = bookings;
        }
        private void RebookButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var bookingId = (int)button.Tag;
                var booking = bookingRepository.GetById(bookingId);
                if (booking != null)
                {
                    var court = dbContext.BadmintonCourts.Find(booking.CourtId);
                    if (court != null)
                    {
                        var courtDetailWindow = new CourtDetailWindow(court,_itemRepository,_courtRepository,Session.LoggedInUser.UserId);
                        courtDetailWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Court not found!");
                    }
                }
                else
                {
                    MessageBox.Show("Booking not found!");
                }
            }
        }

    }
}
