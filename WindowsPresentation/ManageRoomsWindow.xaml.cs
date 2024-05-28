using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using WindowsPresentation.LoginViewModel;

namespace WindowsPresentation
{
    public partial class ManageRoomsWindow : Window
    {
        private readonly FUMiniHotelManagementContext _context;

        public ManageRoomsWindow()
        {
            InitializeComponent();
            _context = new FUMiniHotelManagementContext();
            LoadRoomTypes();
            LoadRoomStatuses();
            LoadRoomsDataGrid();
        }

        private void LoadRoomTypes()
        {
            var listRoomType = _context.RoomTypes.ToList();
            RoomTypeComboBox.ItemsSource = listRoomType;
        }

        private void LoadRoomStatuses()
        {
            RoomStatusComboBox.ItemsSource = new List<Status>
            {
                new Status { Id = 1, Name = "Active" },
                new Status { Id = 0, Name = "Deleted" }
            };
            
        }

        private void LoadRoomsDataGrid()
        {
            RoomsDataGrid.ItemsSource = _context.RoomInformations
                .Include(r => r.RoomType)
                .Select(x =>  new
                {
                    x.RoomId,
                    x.RoomNumber,
                    x.RoomStatus,
                    x.RoomType.RoomTypeName,
                    x.RoomMaxCapacity,
                    x.RoomDetailDescription,
                    x.RoomPricePerDay,
                })
                .ToList();
        }

        private void SearchRoom_Click(object sender, RoutedEventArgs e)
        {
            // Implement search functionality
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            var newRoom = new RoomInformation
            {
                RoomNumber = RoomNumberTextBox.Text,
                RoomDetailDescription = RoomDescriptionTextBox.Text,
                RoomMaxCapacity = int.Parse(MaxCapacityTextBox.Text),
                RoomTypeId = (int)RoomTypeComboBox.SelectedValue,
                RoomStatus = (byte)RoomStatusComboBox.SelectedValue,
                RoomPricePerDay = decimal.Parse(PricePerDayTextBox.Text)
            };

            _context.RoomInformations.Add(newRoom);
            _context.SaveChanges();
            LoadRoomsDataGrid();
        }

        private void EditRoom_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem is RoomInformation selectedRoom)
            {
                selectedRoom.RoomNumber = RoomNumberTextBox.Text;
                selectedRoom.RoomDetailDescription = RoomDescriptionTextBox.Text;
                selectedRoom.RoomMaxCapacity = int.Parse(MaxCapacityTextBox.Text);
                selectedRoom.RoomTypeId = (int)RoomTypeComboBox.SelectedValue;
                selectedRoom.RoomStatus = (byte)RoomStatusComboBox.SelectedValue;
                selectedRoom.RoomPricePerDay = decimal.Parse(PricePerDayTextBox.Text);

                _context.SaveChanges();
                LoadRoomsDataGrid();
            }
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem is RoomInformation selectedRoom)
            {
                _context.RoomInformations.Remove(selectedRoom);
                _context.SaveChanges();
                LoadRoomsDataGrid();
            }
        }

        private void RoomsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem is RoomInformation selectedRoom)
            {
                RoomIDTextBox.Text = selectedRoom.RoomId.ToString();
                RoomNumberTextBox.Text = selectedRoom.RoomNumber;
                RoomDescriptionTextBox.Text = selectedRoom.RoomDetailDescription;
                MaxCapacityTextBox.Text = selectedRoom.RoomMaxCapacity.ToString();
                RoomTypeComboBox.SelectedValue = selectedRoom.RoomTypeId;
                RoomStatusComboBox.SelectedValue = selectedRoom.RoomStatus;
                PricePerDayTextBox.Text = selectedRoom.RoomPricePerDay.ToString();
            }
        }
    }
}
