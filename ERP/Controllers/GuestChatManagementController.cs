using ERP.Data;
using ERP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class GuestChatManagementController : Controller
    {
        private readonly ERPContext _context;

        public GuestChatManagementController(ERPContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveGuests()
        {
            var guests = await _context.GuestUsers
                .Where(g => g.IsActive && g.ExpiryDate > DateTime.Now)
                .Select(g => new {
                    id = g.Id,
                    phoneNumber = g.PhoneNumber,
                    fullName = (g.FirstName + " " + g.LastName) ?? "مهمان",
                    createdDate = g.CreatedDate,
                    expiryDate = g.ExpiryDate,
                    lastActivity = g.LastActivity,
                    allowedUsersCount = _context.GuestChatAccesses.Count(a => a.GuestUserId == g.Id)
                })
                .OrderByDescending(g => g.createdDate)
                .ToListAsync();

            return Json(guests);
        }

        [HttpGet]
        public async Task<IActionResult> GetGuestAccess(int guestId)
        {
            var allowedUserIds = await _context.GuestChatAccesses
                .Where(a => a.GuestUserId == guestId)
                .Select(a => a.AllowedUserId)
                .ToListAsync();

            var allowed = await _context.Users
                .Where(u => allowedUserIds.Contains(u.Id))
                .Select(u => new {
                    id = u.Id,
                    name = u.FirstName + " " + u.LastName,
                    image = string.IsNullOrEmpty(u.Image) ? "/UserImage/Male.png" : "/UserImage/" + u.Image
                })
                .ToListAsync();

            var available = await _context.Users
                .Where(u => !allowedUserIds.Contains(u.Id))
                .Select(u => new {
                    id = u.Id,
                    name = u.FirstName + " " + u.LastName,
                    image = string.IsNullOrEmpty(u.Image) ? "/UserImage/Male.png" : "/UserImage/" + u.Image
                })
                .ToListAsync();

            return Json(new { allowed, available });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGuestAccess(int guestId, string userId)
        {
            var existing = await _context.GuestChatAccesses
                .FirstOrDefaultAsync(a => a.GuestUserId == guestId && a.AllowedUserId == userId);

            if (existing == null)
            {
                _context.GuestChatAccesses.Add(new GuestChatAccess
                {
                    GuestUserId = guestId,
                    AllowedUserId = userId,
                    GrantedDate = DateTime.Now
                });
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveGuestAccess(int guestId, string userId)
        {
            var access = await _context.GuestChatAccesses
                .FirstOrDefaultAsync(a => a.GuestUserId == guestId && a.AllowedUserId == userId);

            if (access != null)
            {
                _context.GuestChatAccesses.Remove(access);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateGuest(int guestId)
        {
            var guest = await _context.GuestUsers.FindAsync(guestId);
            if (guest != null)
            {
                guest.IsActive = false;
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExtendGuestAccess(int guestId, int hours)
        {
            var guest = await _context.GuestUsers.FindAsync(guestId);
            if (guest != null)
            {
                guest.ExpiryDate = guest.ExpiryDate.AddHours(hours);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true });
        }
    }
}
