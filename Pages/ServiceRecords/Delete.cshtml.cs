using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Pages_ServiceRecords
{
    public class DeleteModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public DeleteModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServiceRecord ServiceRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicerecord = await _context.ServiceRecords.FirstOrDefaultAsync(m => m.Id == id);

            if (servicerecord is not null)
            {
                ServiceRecord = servicerecord;

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

            var servicerecord = await _context.ServiceRecords.FindAsync(id);
            if (servicerecord != null)
            {
                ServiceRecord = servicerecord;
                _context.ServiceRecords.Remove(ServiceRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
