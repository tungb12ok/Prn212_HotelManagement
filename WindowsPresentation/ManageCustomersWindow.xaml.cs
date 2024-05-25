using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Models;
using WindowsPresentation.LoginViewModel;

namespace WindowsPresentation
{
    public partial class ManageCustomersWindow : Window
    {
        private readonly FUMiniHotelManagementContext _context;
        private List<Customer> _customers;
        private List<Status> _statuses;

        public ManageCustomersWindow()
        {
            _context = new FUMiniHotelManagementContext();
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _customers = _context.Customers.ToList();
            CustomersDataGrid.ItemsSource = _customers;

            _statuses = new List<Status>
            {
                new Status { Id = 1, Name = "Active" },
                new Status { Id = 0, Name = "Deleted" }
            };

            StatusComboBox.ItemsSource = _statuses;
            StatusComboBox.SelectedValuePath = "Id";
            StatusComboBox.DisplayMemberPath = "Name";
        }

        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            var newCustomer = new Customer
            {
                CustomerFullName = FullNameTextBox.Text,
                Telephone = TelephoneTextBox.Text,
                EmailAddress = EmailTextBox.Text,
                Password = PasswordBox.Password,
                CustomerBirthday = BirthdayDatePicker.SelectedDate,
                CustomerStatus = (byte)StatusComboBox.SelectedValue
            };

            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            _customers.Add(newCustomer);
            RefreshDataGrid();
            ClearForm();
        }

        private void EditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem is Customer selectedCustomer)
            {
                selectedCustomer.CustomerFullName = FullNameTextBox.Text;
                selectedCustomer.Telephone = TelephoneTextBox.Text;
                selectedCustomer.EmailAddress = EmailTextBox.Text;
                selectedCustomer.Password = PasswordBox.Password;
                selectedCustomer.CustomerBirthday = BirthdayDatePicker.SelectedDate;
                selectedCustomer.CustomerStatus = (byte)StatusComboBox.SelectedValue;

                _context.SaveChanges();
                RefreshDataGrid();
                ClearForm();
            }
        }

        private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem is Customer selectedCustomer)
            {
                _context.Customers.Remove(selectedCustomer);
                _context.SaveChanges();
                _customers.Remove(selectedCustomer);
                RefreshDataGrid();
                ClearForm();
            }
        }

        private void SearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();
            var filteredCustomers = _customers.Where(c => c.CustomerFullName.ToLower().Contains(searchText) || c.EmailAddress.ToLower().Contains(searchText)).ToList();
            CustomersDataGrid.ItemsSource = filteredCustomers;
        }

        private void CustomersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomersDataGrid.SelectedItem is Customer selectedCustomer)
            {
                IdTextBox.Text = selectedCustomer.CustomerId.ToString();
                FullNameTextBox.Text = selectedCustomer.CustomerFullName;
                TelephoneTextBox.Text = selectedCustomer.Telephone;
                EmailTextBox.Text = selectedCustomer.EmailAddress;
                PasswordBox.Password = selectedCustomer.Password;
                BirthdayDatePicker.SelectedDate = selectedCustomer.CustomerBirthday;
                StatusComboBox.SelectedValue = selectedCustomer.CustomerStatus;
            }
        }

        private void ClearForm()
        {
            IdTextBox.Text = string.Empty;
            FullNameTextBox.Text = string.Empty;
            TelephoneTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            BirthdayDatePicker.SelectedDate = null;
            StatusComboBox.SelectedIndex = -1;
        }

        private void RefreshDataGrid()
        {
            CustomersDataGrid.ItemsSource = null;
            CustomersDataGrid.ItemsSource = _customers;
        }
    }
    
}
