using System.Windows;

namespace WindowsPresentation;

public partial class CustomerWindow : Window
{
    private readonly int _customerId;
    public CustomerWindow(int customerId)
    {
        InitializeComponent();
        _customerId = customerId;
    }

    private void ManageProfile_Click(object sender, RoutedEventArgs e)
    {
       ProfileWindow profileWindow = new ProfileWindow(_customerId);
       profileWindow.Show();
    }

    private void ViewBookingHistory_Click(object sender, RoutedEventArgs e)
    {
       BookingHistoryWindow bookingHistoryWindow = new  BookingHistoryWindow(_customerId);
       bookingHistoryWindow.Show();
    }

    private void BookingRoom_Click(object sender, RoutedEventArgs e)
    {
        BookingWindow bookingWindow = new BookingWindow(_customerId);
        bookingWindow.Show();
    }
}