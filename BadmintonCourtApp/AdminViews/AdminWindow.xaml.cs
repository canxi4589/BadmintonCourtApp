using BadmintonCourtApp.AdminViews.Pages;
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

namespace BadmintonCourtApp.AdminViews
{


    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

        BookingRepository bookingRepository;
        ItemRepository itemRepository;
        CourtRepository courtRepository;
        UserRepository userRepository;

        public AdminWindow()
        {
            InitializeComponent();

            DBContext context = new DBContext();

            bookingRepository = new BookingRepository(context);
            itemRepository = new ItemRepository(context);
            courtRepository = new CourtRepository(context);
            userRepository = new UserRepository(context);

            MainFrame.Content = new DashboardPage(bookingRepository);
        }

        private void ToAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DashboardPage(bookingRepository);
        }

        private void ToBookingWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new BookingPage(bookingRepository, userRepository, courtRepository);
        }

        private void ToAccountWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new AccountPage();
        }

        private void ToCourtWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new CourtsPage(courtRepository);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            this.Close();
            mainWindow.Show();
        }

        private void ToItemWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new ItemPage(itemRepository);
        }
    }
}
