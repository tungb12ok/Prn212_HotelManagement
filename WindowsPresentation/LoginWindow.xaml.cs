using System.IO;
using System.Windows;
using BusineessLogic.Repository;
using DataAccess.Models;
using Newtonsoft.Json;
using WindowsPresentation.LoginViewModel;

namespace WindowsPresentation;

public partial class LoginWindow : Window
{
    private readonly AdminCredentials _adminCredentials;
    private readonly CustomerRepository _repository;

    public LoginWindow()
    {
        InitializeComponent();

        // Đọc thông tin từ appsettings.json
        var json = File.ReadAllText("appsettings.json");
        var appSettings = JsonConvert.DeserializeObject<AppSettings>(json);
        _adminCredentials = appSettings.AdminCredentials;
        _repository = new CustomerRepository();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        string email = EmailTextBox.Text;
        string password = PasswordBox.Password;
        Customer c = _repository.login(email, password);
        if (email == _adminCredentials.Email && password == _adminCredentials.Password)
        {
            MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            AdminWindow mainWindow = new AdminWindow();
            mainWindow.Show();
            this.Close();
        }else if (c != null)
        {
            CustomerWindow cw = new CustomerWindow(c.CustomerId);
            cw.Show();
            this.Close();
        }
        else
        {
            MessageBox.Show("Invalid email or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}