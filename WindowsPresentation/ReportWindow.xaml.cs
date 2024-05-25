using System;
using System.Linq;
using System.Windows;
using DataAccess.Models;

namespace WindowsPresentation;

public partial class ReportWindow : Window
{
        public ReportWindow()
        {
            InitializeComponent();
        }

        private async void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;

            if (startDate == null || endDate == null)
            {
                MessageBox.Show("Please select both start and end dates.");
                return;
            }

            using (var context = new FUMiniHotelManagementContext())
            {
                var reportData =  context.BookingDetails
                    .Where(b => b.StartDate >= startDate && b.EndDate <= endDate)
                    .OrderByDescending(b => b.StartDate)
                    .Select(b => new
                    {
                        b.BookingReservationId,
                        b.RoomId,
                        b.StartDate,
                        b.EndDate,
                        b.ActualPrice,
                        CustomerName = b.BookingReservation.Customer.CustomerFullName,
                        RoomNumber = b.Room.RoomNumber
                    })
                    .ToList();

                ReportDataGrid.ItemsSource = reportData;
            }
        }
}