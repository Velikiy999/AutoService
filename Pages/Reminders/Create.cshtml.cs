using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Pages_Reminders
{
    public class CreateModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public CreateModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Reminder Reminder { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reminders.Add(Reminder);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
