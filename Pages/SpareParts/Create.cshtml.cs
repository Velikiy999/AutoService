using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoService.Data;
using AutoService.Models;

namespace AutoService.Pages_SpareParts
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
            return Page();
        }

        [BindProperty]
        public SparePart SparePart { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SpareParts.Add(SparePart);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
