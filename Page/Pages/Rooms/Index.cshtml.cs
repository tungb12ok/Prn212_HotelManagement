 using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Page.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Page.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly FUMiniHotelManagementContext _context;

        public IndexModel(FUMiniHotelManagementContext context)
        {
            _context = context;
        }

        [BindProperty] public string RoomNumber { get; set; }
        [BindProperty] public string RoomDescription { get; set; }
        [BindProperty] public int MaxCapacity { get; set; }
        [BindProperty] public int RoomTypeId { get; set; }
        [BindProperty] public byte RoomStatus { get; set; }
        [BindProperty] public decimal PricePerDay { get; set; }
        [BindProperty] public int EditRoomId { get; set; }
        [BindProperty] public int DeleteRoomId { get; set; }
        [BindProperty] public List<RoomType> RoomTypes { get; set; }
        [BindProperty] public List<Status> RoomStatuses { get; set; }

        public IList<RoomInformation> Rooms { get; private set; }

        public void OnGet()
        {
            Rooms = _context.RoomInformations.ToList();
            RoomTypes = _context.RoomTypes.ToList();
            RoomStatuses = new List<Status>
            {
                new Status { Id = 1, Name = "Active" },
                new Status { Id = 0, Name = "Deleted" }
            };
        }

        public IActionResult OnPostAddRoom()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newRoom = new RoomInformation
            {
                RoomNumber = RoomNumber,
                RoomDetailDescription = RoomDescription,
                RoomMaxCapacity = MaxCapacity,
                RoomTypeId = RoomTypeId,
                RoomStatus = RoomStatus,
                RoomPricePerDay = PricePerDay
            };

            _context.RoomInformations.Add(newRoom);
            _context.SaveChanges();

            return RedirectToPage("/Rooms/Index");
        }

        public IActionResult OnPostEditRoom()
        {
            var roomToUpdate = _context.RoomInformations.FirstOrDefault(r => r.RoomId == EditRoomId);

            if (roomToUpdate == null)
            {
                return NotFound();
            }

            // Update room details
            roomToUpdate.RoomNumber = RoomNumber;
            roomToUpdate.RoomDetailDescription = RoomDescription;
            roomToUpdate.RoomMaxCapacity = MaxCapacity;
            roomToUpdate.RoomTypeId = RoomTypeId;
            roomToUpdate.RoomStatus = RoomStatus;
            roomToUpdate.RoomPricePerDay = PricePerDay;

            _context.SaveChanges();

            return RedirectToPage("/Rooms/Index");
        }

        public IActionResult OnPostDeleteRoom()
        {
            var roomToDelete = _context.RoomInformations.FirstOrDefault(r => r.RoomId == DeleteRoomId);

            if (roomToDelete == null)
            {
                return NotFound();
            }

            _context.RoomInformations.Remove(roomToDelete);
            _context.SaveChanges();

            return RedirectToPage("/Rooms/Index");
        }
    }
}
