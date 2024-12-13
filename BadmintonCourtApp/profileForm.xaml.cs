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
    /// Interaction logic for profileForm.xaml
    /// </summary>
    public partial class profileForm : Window
    {
        private readonly UserRepository userRepository;
        private readonly User _currentUser;
        public profileForm(UserRepository u,int uid)
        {
            InitializeComponent();
            userRepository = u;
            _currentUser = userRepository.GetById(uid);
            LoadUserData();
        }
        private void LoadUserData()
        {
            NameTextBox.Text = _currentUser.Name;
            GmailTextBox.Text = _currentUser.Gmail;
            PhoneNumberTextBox.Text = _currentUser.PhoneNumber;
            UsernameTextBox.Text = _currentUser.Username;
            PasswordBox.Password = _currentUser.Password;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            _currentUser.Name = NameTextBox.Text;
            _currentUser.Gmail = GmailTextBox.Text;
            _currentUser.PhoneNumber = PhoneNumberTextBox.Text;
            _currentUser.Username = UsernameTextBox.Text;
            _currentUser.Password = PasswordBox.Password;

            userRepository.Update(_currentUser);
            MessageBox.Show("Profile updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
