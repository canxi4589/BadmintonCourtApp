using BadmintonCourtApp.AdminViews.Popup;
using MaterialDesignThemes.Wpf;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Xaml.Behaviors;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BadmintonCourtApp.AdminViews.Pages
{
    /// <summary>
    /// Interaction logic for CourtsPage.xaml
    /// </summary>

    public partial class CourtsPage : Page
    {
        private CourtRepository courtRepository;
        public CourtsPage(CourtRepository courtRepository)
        {
            InitializeComponent();
            this.courtRepository = courtRepository;
            LoadBadmintonCourts();
            clear();
        }
        public void LoadBadmintonCourts()
        {
            CourtList.ItemsSource = courtRepository.getAll();
        }
        public void clear()
        {
            IDSearchTextBox.Text = string.Empty;
            LocationSearchTextBox.Text = string.Empty;
            NameSearchTextBox.Text = string.Empty;
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IDSearchTextBox.Text.IsNullOrEmpty())
            {
                CourtList.ItemsSource = courtRepository.getByFilter(0, LocationSearchTextBox.Text, NameSearchTextBox.Text);
            }
            else if (int.TryParse(IDSearchTextBox.Text, out int id))
            {
                CourtList.ItemsSource = courtRepository.getByFilter(int.Parse(IDSearchTextBox.Text), LocationSearchTextBox.Text, NameSearchTextBox.Text);
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid ID input!");
            }
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            IDSearchTextBox.Text = string.Empty;
            LocationSearchTextBox.Text = string.Empty;
            NameSearchTextBox.Text = string.Empty;
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CourtList.SelectedItem is BadmintonCourt badmintonCourt)
            {
                var result = System.Windows.MessageBox.Show("Are you sure you want to delete court " + badmintonCourt.CourtName, "Delete success", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        courtRepository.Remove(badmintonCourt);
                        System.Windows.MessageBox.Show("Court deleted successfully.", "Delete Success", MessageBoxButton.OK);
                        LoadBadmintonCourts();
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show($"Error deleting court: {ex.Message}", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a court to update.", "No Selection", MessageBoxButton.OK);
            }
        }


        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (CourtList.SelectedItem is BadmintonCourt badmintonCourt)
            {
                UpdateCourtWindow updateCourtWindow = new UpdateCourtWindow(badmintonCourt, courtRepository);
                updateCourtWindow.ShowDialog();
                LoadBadmintonCourts();
            }
            else
            {
                System.Windows.MessageBox.Show("Please select a court to update.", "No Selection", MessageBoxButton.OK);
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddCourtWindow addCourtWindow = new AddCourtWindow(courtRepository);
            addCourtWindow.ShowDialog();
            LoadBadmintonCourts();
        }
    }
}
