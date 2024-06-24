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
    /// Interaction logic for forgotPasswordWindow.xaml
    /// </summary>
    public partial class forgotPasswordWindow : Window
    {
        private readonly UserRepository userRepository;
        public forgotPasswordWindow(UserRepository user)
        {
            InitializeComponent();
            userRepository = user;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Placeholder for password reset logic
            string emailAddress = EmailTextBox.Text.Trim();

            if (string.IsNullOrEmpty(emailAddress))
            {
                MessageBox.Show( "Please enter your email address.");
                return;
            }
            User u = userRepository.FindByEmail(emailAddress);
            string password = NewPasswordBox.Text.Trim();
            u.Password = password;
            userRepository.Update(u);
            MessageBox.Show($"Password reset successfully"); // Replace with actual logic

            Close(); // Close the window after password reset operation
        }
    }
}
