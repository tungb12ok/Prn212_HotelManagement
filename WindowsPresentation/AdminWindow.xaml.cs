using System.Windows;

namespace WindowsPresentation;

public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
    }

    private void ManageCustomers_Click(object sender, RoutedEventArgs e)
    {
        ManageCustomersWindow manageCustomersWindow = new ManageCustomersWindow();
        manageCustomersWindow.Show();
    }

    private void ManageRooms_Click(object sender, RoutedEventArgs e)
    {
        ManageRoomsWindow manageRoomsWindow = new ManageRoomsWindow();
        manageRoomsWindow.Show();
    }

    private void CreateReport_Click(object sender, RoutedEventArgs e)
    {
        ReportWindow reportWindow = new ReportWindow();
        reportWindow.Show();
    }
}