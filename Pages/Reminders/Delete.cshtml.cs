using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Pages_Reminders
{
    public class DeleteModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public DeleteModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reminder Reminder { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminders.FirstOrDefaultAsync(m => m.Id == id);

            if (reminder is not null)
            {
                Reminder = reminder;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder != null)
            {
                Reminder = reminder;
                _context.Reminders.Remove(Reminder);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
