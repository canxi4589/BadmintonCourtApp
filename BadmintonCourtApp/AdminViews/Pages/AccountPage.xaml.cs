using Microsoft.EntityFrameworkCore;
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
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        private readonly UserRepository userRepository;
        private readonly DBContext dBContext;
        public AccountPage()
        {
            InitializeComponent();
            dBContext = new DBContext();
            userRepository = new UserRepository(dBContext);
            LoadUsers();
        }
        private void LoadUsers()
        {
            UserListBox.ItemsSource = userRepository.GetAll().Where(c => c.Role!="Admin").ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new User
            {
                Name = NameTextBox.Text,
                Gmail = GmailTextBox.Text,
                Username = UsernameTextBox.Text,
                Password = PasswordBox.Password,
                PhoneNumber = PhoneNumberTextBox.Text
            };

            userRepository.Add(newUser);
            LoadUsers();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedItem is User selectedUser)
            {
                selectedUser.Name = NameTextBox.Text;
                selectedUser.Gmail = GmailTextBox.Text;
                selectedUser.Username = UsernameTextBox.Text;
                selectedUser.Password = PasswordBox.Password;
                selectedUser.PhoneNumber = PhoneNumberTextBox.Text;

                userRepository.Update(selectedUser);
                LoadUsers();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedItem is User selectedUser)
            {
                userRepository.Remove(selectedUser);
                LoadUsers();
            }
        }

        private void UserListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserListBox.SelectedItem is User selectedUser)
            {
                NameTextBox.Text = selectedUser.Name;
                GmailTextBox.Text = selectedUser.Gmail;
                UsernameTextBox.Text = selectedUser.Username;
                PasswordBox.Password = selectedUser.Password;
                PhoneNumberTextBox.Text = selectedUser.PhoneNumber;
            }
        }
    }
}
