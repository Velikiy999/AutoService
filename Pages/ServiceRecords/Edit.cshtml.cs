using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Pages_ServiceRecords
{
    public class EditModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public EditModel(AutoService.Data.ApplicationDbContext context)
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

            var servicerecord =  await _context.ServiceRecords.FirstOrDefaultAsync(m => m.Id == id);
            if (servicerecord == null)
            {
                return NotFound();
            }
            ServiceRecord = servicerecord;
           ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Make");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ServiceRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceRecordExists(ServiceRecord.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ServiceRecordExists(int id)
        {
            return _context.ServiceRecords.Any(e => e.Id == id);
        }
    }
}
