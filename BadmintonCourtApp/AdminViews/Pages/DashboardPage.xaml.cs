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

namespace BadmintonCourtApp.AdminViews.Pages
{
    public class DashboardDataContext
    {
        public int Total { get; set; } = 0;
        public int Upcoming { get; set; } = 0;
        public int Finished { get; set; } = 0;
        public int TotalEarnedAwait { get; set; } = 0;
        public int TotalEarned { get; set; } = 0;

        public DashboardDataContext() { }

        public DashboardDataContext(int total, int finished, int upcoming, int total_earned_await, int total_earned) 
        {
            Total = total;
            Upcoming = upcoming;
            Finished = finished;
            TotalEarnedAwait = total_earned_await;
            TotalEarned = total_earned;
        }
    }

    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        public DashboardPage(BookingRepository bookingRepo)
        {
            InitializeComponent();
            var todayBooking = bookingRepo.GetAllBookinInfoLiterally();
            int finished = todayBooking.Where(x => x.BookingSlots.Any(y => y.BookDate <= DateOnly.FromDateTime(DateTime.Now))).Count();
            int upcoming = todayBooking.Where(x => x.BookingSlots.Any(y => y.BookDate > DateOnly.FromDateTime(DateTime.Now))).Count();
            int totalAwait = todayBooking.Where(x => x.BookingSlots.Any(y => y.BookDate > DateOnly.FromDateTime(DateTime.Now)) && x.Status == "Booked").Sum(x => Convert.ToInt32(x.TotalPrice));
            int total = todayBooking.Where(x => x.Status == "Done").Sum(x => Convert.ToInt32(x.TotalPrice));

            this.DataContext = new DashboardDataContext(todayBooking.Count(), finished, upcoming, totalAwait, total);

            UpcomingBooks.ItemsSource = todayBooking.Where(x => x.BookingSlots.Any(y => y.BookDate > DateOnly.FromDateTime(DateTime.Now)) && x.Status == "Booked") ;
        }
    }
}
