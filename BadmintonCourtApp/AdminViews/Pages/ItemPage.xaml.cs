using BadmintonCourtApp.AdminViews.Popup;
using Repository.Models;
using Repository.repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ItemPage.xaml
    /// </summary>
    public partial class ItemPage : Page
    {
        private ItemRepository ItemRepository;
        private readonly DBContext dbContext;
        private readonly ItemTypeRepository itemTypeRepository;
        public ItemPage(ItemRepository i)
        {
            InitializeComponent();
            itemTypeRepository = new ItemTypeRepository(dbContext);
            ItemRepository = i;
            LoadItemTypes();
            LoadItemList();
        }
        public void LoadItemList()
        {
            ItemList.ItemsSource = ItemRepository.GetAllItems();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(Price.Text, out int newPrice))
            {
                if (ItemTypeComboBox.SelectedValue is int selectedTypeID)
                {
                    var newItem = new Item
                    {
                        Name = Name.Text,
                        Price = newPrice,
                        ItemTypeId = selectedTypeID
                    };
                    

                    ItemRepository.Add(newItem);
                    MessageBox.Show("Item added successfully!", "Add Success", MessageBoxButton.OK);
                    clear();
                    LoadItemList();
                }
                else
                {
                    MessageBox.Show("Please select a Type.", "Input Error", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Invalid input for price!", "Input Error", MessageBoxButton.OK);
            };
        }
        public void clear()
        {
            Name.Text = "";
            Price.Text = "";
            ItemTypeComboBox.SelectedIndex = -1;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Item item = (Item)ItemList.SelectedItem;
            var result = System.Windows.MessageBox.Show("Are you sure you want to delete item " + item.Name, "Delete success", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ItemRepository.Remove(item);
                System.Windows.MessageBox.Show("Item deleted successfully.", "Delete Success", MessageBoxButton.OK);
                LoadItemList();
            }
        }
        public class TypeViewModel
        {
            public int TypeId { get; set; }
            public string Type { get; set; }
        }

        public void LoadItemTypes()
        {
            // Fetch item types from the repository and map to view model
            var itemTypes = ItemRepository.GetAllItemTypes()
                .Select(c => new TypeViewModel
                {
                    TypeId = c.ItemTypeId,
                    Type = c.Type
                }).ToList();

            // Set the ItemsSource property
            ItemTypeComboBox_Copy.ItemsSource = itemTypes;
            ItemTypeComboBox_Copy.DisplayMemberPath = "Type";
            ItemTypeComboBox_Copy.SelectedValuePath = "TypeId";
            ItemTypeComboBox.ItemsSource = itemTypes;
            ItemTypeComboBox.DisplayMemberPath = "Type";
            ItemTypeComboBox.SelectedValuePath = "TypeId";
        }
        

        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            Price_Copy.Text = Price_Copy.Text.Trim();
            Name_Copy.Text = Name_Copy.Text.Trim();
            if (int.TryParse(Price_Copy.Text, out int newPrice))
            {
                if (ItemTypeComboBox_Copy.SelectedValue is int selectedTypeID)
                {
                    Item item = (Item)ItemList.SelectedItem;
                    item.Name = Name_Copy.Text;
                    item.Price = newPrice;
                    item.ItemTypeId = selectedTypeID;
                    ItemRepository.Update(item);
                    MessageBox.Show("Item updated successfully!", "Update Success", MessageBoxButton.OK);
                    LoadItemList();
                }
                else
                {
                    MessageBox.Show("Please select a Type.", "Input Error", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Invalid input for price!", "Input Error", MessageBoxButton.OK);
            };
        }

        

        private void ItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemList.SelectedItem != null)
            {
                Item item = (Item)ItemList.SelectedItem;
                Name_Copy.Text = item.Name;
                Price_Copy.Text = item.Price.ToString();
                ItemTypeComboBox_Copy.SelectedValue = item.ItemType.ItemTypeId;
            }
            else
            {
                Name_Copy.Text = "";
                Price_Copy.Text = "";
                ItemTypeComboBox_Copy.SelectedIndex = -1;
            }
        }
    }
}
