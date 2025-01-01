using AutoService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoService.Pages_Reminders
{
    public class IndexModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(AutoService.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Reminder> Reminders { get; set; }

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            var isClient = User.IsInRole("Client");

            if (!isClient)
            {
                Reminders = await _context.Reminders
                    .Include(r => r.Client)
                    .OrderBy(r => r.ReminderDate)
                    .ToListAsync();
            }
            else if (userId != null)
            {
                Reminders = await _context.Reminders
                    .Include(r => r.Client)
                    .Where(r => r.Client.UserId == userId)
                    .OrderBy(r => r.ReminderDate)
                    .ToListAsync();
            }
            else
            {
                Reminders = new List<Reminder>();
            }
        }
    }
}
