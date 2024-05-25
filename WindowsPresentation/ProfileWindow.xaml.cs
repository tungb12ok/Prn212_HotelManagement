using System.Linq;
using System.Windows;
using DataAccess.Models;

namespace WindowsPresentation;

public partial class ProfileWindow : Window
{
    private FUMiniHotelManagementContext _context;
    private Customer _currentUser;

    public ProfileWindow(int customerId)
    {
        InitializeComponent();
        _context = new FUMiniHotelManagementContext();
        LoadUserProfile(customerId);
    }

    private void LoadUserProfile(int customerId)
    {
        _currentUser = _context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        if (_currentUser != null)
        {
            FullNameTextBox.Text = _currentUser.CustomerFullName;
            TelephoneTextBox.Text = _currentUser.Telephone;
            EmailTextBox.Text = _currentUser.EmailAddress;
            PasswordBox.Password = _currentUser.Password;
            BirthdayDatePicker.SelectedDate = _currentUser.CustomerBirthday;
        }
        else
        {
            MessageBox.Show("User not found.");
            Close();
        }
    }

    private void SaveChanges_Click(object sender, RoutedEventArgs e)
    {
        if (_currentUser != null)
        {
            _currentUser.CustomerFullName = FullNameTextBox.Text;
            _currentUser.Telephone = TelephoneTextBox.Text;
            _currentUser.EmailAddress = EmailTextBox.Text;
            _currentUser.Password = PasswordBox.Password;
            _currentUser.CustomerBirthday = BirthdayDatePicker.SelectedDate;

            _context.SaveChanges();
            MessageBox.Show("Profile updated successfully.");
        }
    }
}