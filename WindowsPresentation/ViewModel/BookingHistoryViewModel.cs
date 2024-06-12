using System;
using System.Collections.Generic;
using DataAccess.Models;

namespace WindowsPresentation.LoginViewModel;

public class BookingHistoryViewModel
{
    public int BookingReservationId { get; set; }
    public Customer Customer { get; set; }
    public DateTime? BookingDate { get; set; }
    public decimal? TotalPrice { get; set; }
    public byte? BookingStatus { get; set; }
    public List<RoomDetailViewModel> Rooms { get; set; }
}


