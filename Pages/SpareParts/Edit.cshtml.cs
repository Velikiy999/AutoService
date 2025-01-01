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

namespace AutoService.Pages_SpareParts
{
    public class EditModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public EditModel(AutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SparePart SparePart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparepart =  await _context.SpareParts.FirstOrDefaultAsync(m => m.Id == id);
            if (sparepart == null)
            {
                return NotFound();
            }
            SparePart = sparepart;
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

            _context.Attach(SparePart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SparePartExists(SparePart.Id))
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

        private bool SparePartExists(int id)
        {
            return _context.SpareParts.Any(e => e.Id == id);
        }
    }
}
