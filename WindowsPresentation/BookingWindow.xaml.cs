using System;
using System.Linq;
using System.Windows;
using BusineessLogic.Repository;
using DataAccess.Models;

namespace WindowsPresentation
{
    public partial class BookingWindow : Window
    {
        private readonly FUMiniHotelManagementContext _context;
        private int customerId;
        private CustomerRepository _repository;
        private Customer customer;

        public BookingWindow(int customerId)
        {
            _context = new FUMiniHotelManagementContext();
            this.customerId = customerId;
            InitializeComponent();
            _repository = new CustomerRepository();
            customer = _repository.CustomerById(customerId);
            LoadData();
        }

        private void LoadData()
        {
            cbRoom.ItemsSource = _context.RoomInformations.ToList();
            cbRoom.DisplayMemberPath = "RoomNumber";
            cbRoom.SelectedValuePath = "RoomID";
            cbRoom.SelectedIndex = 0;
            tbCustomer.Text = customer.CustomerFullName;
        }

        private void BookButton_Click(object sender, RoutedEventArgs e)
        {
            if (cbRoom.SelectionBoxItem == null || StartDatePicker.SelectedDate == null ||
                EndDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please fill all fields", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedRoom = (RoomInformation)cbRoom.SelectedItem;
            

            if (selectedRoom == null)
            {
                MessageBox.Show("Selected room is not valid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var startDate = StartDatePicker.SelectedDate.Value;
            var endDate = EndDatePicker.SelectedDate.Value;
            var numberOfDays = (endDate - startDate).Days;

            if (numberOfDays <= 0)
            {
                MessageBox.Show("End date must be after start date", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }

            var totalPrice = numberOfDays * selectedRoom.RoomPricePerDay;

            var bookingReservation = new BookingReservation
            {
                CustomerId = customerId,
                BookingDate = DateTime.Now,
                BookingStatus = 1,
                TotalPrice = totalPrice
            };

            _context.BookingReservations.Add(bookingReservation);
            _context.SaveChanges();

            var bookingDetail = new BookingDetail
            {
                BookingReservationId = bookingReservation.BookingReservationId,
                RoomId = selectedRoom.RoomId,
                StartDate = startDate,
                EndDate = endDate,
                ActualPrice = totalPrice
            };

            _context.BookingDetails.Add(bookingDetail);
            _context.SaveChanges();

            MessageBox.Show($"Booking successful! Total: {totalPrice}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}