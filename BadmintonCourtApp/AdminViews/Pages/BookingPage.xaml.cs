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
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using MaterialDesignThemes.Wpf;

namespace BadmintonCourtApp.AdminViews.Pages
{

    public class BookingDataContext
    {
        public int BookId {  get; set; } = 0;
        public string UserName {  get; set; } = "No Information";
        public string UserPhone { get; set; } = "No Information";
        public int TotalPrice { get; set; } = 0;
        public string Note { get; set; } = string.Empty;

        public Booking SelectedBook { get; set; } = null!;

        public BookingDataContext() { }

        public BookingDataContext(int id, string name, string phone, int price, string note, Booking? booking = null)
        {
            BookId = id;
            UserName = name;
            UserPhone = phone;
            TotalPrice = price;
            Note = note;
            SelectedBook = booking;
        }
    }

    /// <summary>
    /// Interaction logic for BookingPage.xaml
    /// </summary>
    public partial class BookingPage : Page
    {
        private readonly BookingRepository bookingRepository;
        private readonly UserRepository userRepository;
        private readonly CourtRepository courtRepository;

        public BookingPage(BookingRepository bookRepo, UserRepository userRepo, CourtRepository courtRepo)
        {
            InitializeComponent();
            bookingRepository = bookRepo;
            userRepository = userRepo;
            courtRepository = courtRepo;

            UpcomingBooks.ItemsSource = bookRepo.GetAllBookinInfoLiterally().Where(x => x.Status == null).OrderBy(x => x.BookingSlots.First().BookDate).Reverse();

            SearchCourtInput.ItemsSource = courtRepo.GetAll().Select(x => x.CourtName);

            this.DataContext = new BookingDataContext();
        }

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            var item = this.DataContext as BookingDataContext;

            if (item != null && item.BookId != 0)
            {
                this.DataContext = new BookingDataContext();

                if (item.SelectedBook.Status != "Done")
                {
                    item.SelectedBook.Status = "Done";
                    bookingRepository.Update(item.SelectedBook);
                    MessageBox.Show("Checkin customer successfully");
                }
                else
                {
                    MessageBox.Show("You already check in this book", "Warning");
                }

                UpcomingBooks.ItemsSource = bookingRepository.GetAllBookinInfoLiterally().Where(x => x.Status == null).OrderBy(x => x.BookingSlots.First().BookDate).Reverse();
            }
            else
            {
                MessageBox.Show("You did not selected any book item", "Warning");
            }
        }

        private void UpcomingBooks_CurrentCellChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var currentItem = UpcomingBooks.SelectedItem as Booking;

            if (currentItem != null)
            {
                this.DataContext = new BookingDataContext(currentItem.BookingId, currentItem.User.Name, currentItem.User.PhoneNumber, currentItem.TotalPrice ?? 0, currentItem.SpecialNote, currentItem); ;
            }
            else
            {
                this.DataContext = new BookingDataContext();
            }

            

            this.DataContext = (this.DataContext as BookingDataContext);
        }

        private void SearchInputChanged(string text, string courtName)
        {
            var result = bookingRepository.GetAllBookinInfoLiterally().Where(x => x.Status == null).OrderBy(x => x.BookingSlots.First().BookDate).Reverse();

            if (!text.IsNullOrEmpty())
            {
                result = result.Where(x => x.User.Name.Contains(text, StringComparison.OrdinalIgnoreCase));
            }
            if (!courtName.IsNullOrEmpty())
            {
                result = result.Where(x => x.Court.CourtName == courtName);
            }

            UpcomingBooks.ItemsSource = result;
        }

        private void SearchnNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine((sender as TextBox).Text);
            SearchInputChanged((sender as TextBox).Text, null);
        }

        private void SearchCourtInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(e.AddedItems[0].ToString());
            SearchInputChanged(null, e.AddedItems[0].ToString());
        }
    }
}
