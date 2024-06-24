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

namespace BadmintonCourtApp
{
    /// <summary>
    /// Interaction logic for registerWindow.xaml
    /// </summary>
    public partial class registerWindow : Window
    {
        private readonly UserRepository _userRepository;

        public registerWindow(UserRepository userRepository)
        {
            InitializeComponent();
            _userRepository = userRepository;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string name = NameTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            string password = PasswordBox.Text;

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var existingUser = _userRepository.Find(u => u.Username == username || u.Gmail == email);
            if (existingUser.Any())
            {
                MessageBox.Show("Username or email already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newUser = new User
            {
                Username = username,
                Gmail = email,
                Name = name,
                PhoneNumber = phoneNumber,
                Password = password,
                Role = "Customer"
            };

            try
            {
                 _userRepository.Add(newUser);
                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, object e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
