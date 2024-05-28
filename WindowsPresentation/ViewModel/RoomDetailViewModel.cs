using System;

namespace WindowsPresentation.LoginViewModel;

public class RoomDetailViewModel
{
    public string RoomNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? ActualPrice { get; set; }
}