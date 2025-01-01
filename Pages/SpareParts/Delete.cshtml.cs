using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Pages_SpareParts
{
    public class DeleteModel : PageModel
    {
        private readonly AutoService.Data.ApplicationDbContext _context;

        public DeleteModel(AutoService.Data.ApplicationDbContext context)
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

            var sparepart = await _context.SpareParts.FirstOrDefaultAsync(m => m.Id == id);

            if (sparepart is not null)
            {
                SparePart = sparepart;

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

            var sparepart = await _context.SpareParts.FindAsync(id);
            if (sparepart != null)
            {
                SparePart = sparepart;
                _context.SpareParts.Remove(SparePart);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}