using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusineessLogic.Repository;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Page.ViewModel;

namespace Page.Pages.Rooms
{
    public class EditModel : PageModel
    {

        private readonly FUMiniHotelManagementContext _context;
        private readonly RoomRepository _repository;

        public EditModel(FUMiniHotelManagementContext context)
        {
            _context = context;
            _repository = new RoomRepository();
        }

        [BindProperty]
        public int RoomId { get; set; }

        [BindProperty]
        public string RoomNumber { get; set; }

        [BindProperty]
        public string RoomDescription { get; set; }

        [BindProperty]
        public int MaxCapacity { get; set; }

        [BindProperty]
        public int RoomTypeId { get; set; }

        [BindProperty]
        public int RoomStatus { get; set; }

        [BindProperty]
        public decimal PricePerDay { get; set; }
        
        public List<RoomType> RoomTypes { get; set; }
        public List<Status> RoomStatuses { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.RoomInformations.FirstOrDefaultAsync(m => m.RoomId == id);

            if (room == null)
            {
                return NotFound();
            }

            RoomId = room.RoomId;
            RoomNumber = room.RoomNumber;
            RoomDescription = room.RoomDetailDescription;
            MaxCapacity = room.RoomMaxCapacity ?? 1;
            RoomTypeId = room.RoomTypeId;
            RoomStatus = int.Parse(room.RoomStatus.ToString());
            PricePerDay = room.RoomPricePerDay ?? 1;

            // Populate dropdown lists
            RoomTypes = await _context.RoomTypes.ToListAsync();
            RoomStatuses = new List<Status>
            {
                new Status { Id = 1, Name = "Active" },
                new Status { Id = 0, Name = "Deleted" }
            };

            return Page();
        }
        public async Task<IActionResult> OnPostEditRoomAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var room = await _context.RoomInformations.FindAsync(RoomId);

            if (room == null)
            {
                return NotFound();
            }

            // Update room details
            room.RoomNumber = RoomNumber;
            room.RoomDetailDescription = RoomDescription;
            room.RoomMaxCapacity = MaxCapacity;
            room.RoomTypeId = RoomTypeId;
            room.RoomStatus = (byte)(RoomStatus == 0 ? 1 : 0);
            room.RoomPricePerDay = PricePerDay;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
